using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using System.Runtime.InteropServices;
using SwConst;
using SWPublished;
using SolidWorksTools;
using System.Diagnostics;
using SolidWorksTools.File;
using System.Reflection;
using System.IO;
using HydroSolidworksLibrary;
using HydroTaskpane2.Connectors;
using HydroTaskpane2.References;
using HydroTaskpane2_AddIn.SWEventHandlerStrategy.AttributeTemplates;

namespace HydroTaskpane2_AddIn.SWEventHandlerStrategy.TaskpaneEvents
{
    public class GeneralMethodCollection
    {
        public GeneralMethodCollection() : base()
        {

        }

        #region public methods

        public void setDescription()
        {
            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;

            try
            {
                bool AddIn = HydroSolidworksLibrary.SldWorksStandards.AddInIsLoaded();
                bool checkIntegration = HydroSolidworksLibrary.SldWorksStandards.checkIntegrationCheckbox();

                if (AddIn && checkIntegration)
                {
                    // check for V_Name
                    setLoadAttributes("V_Name", "SAP_MATNR", filterVName);

                    // search description attribute within current configuration

                    Configuration configuration = swModel.GetActiveConfiguration();
                    CustomPropertyManager swCustPropMgr = configuration.CustomPropertyManager;

                    string[] attributes = swCustPropMgr.GetNames();

                    foreach (string attr in attributes)
                    {
                        if (attr.ToLower() == "description")
                        {
                            setLoadAttributes( "HYDRO_CAD_DESCRIPTION_COMPLETE_EN", attr);
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Print(" :: Hydro Taskpane :: ERROR - " + e.ToString() + " :: ");
            }
        }

        public void UpdateDrawing(HydroTaskpane2.HydroTaskpane2_UI taskpane)
        {
            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;
            SldWorks.SldWorks swApp = SWModelConnector.GetInstance().swApp;

            DrawingDoc swDrawing = null;
            ModelDoc2 swRefModel = null;

            if (swModel.GetType() != (int)swDocumentTypes_e.swDocDRAWING || swModel == null) { return; }

            try
            {
                swDrawing = (DrawingDoc)swModel;
                swRefModel = (ModelDoc2)((View)swDrawing.GetFirstView()).GetNextView().ReferencedDocument;
            }
            catch(Exception e)
            {
                DebugBuilder.PrintError(e);
                return;
            }

            DebugBuilder.Print($"Run methods to update drawing (swRefModel is null (({(swRefModel == null).ToString()}))");

            if (swRefModel != null)
            {
                SldWorksStandards.CheckForOldAttributes(ref swModel, ref swRefModel);
                SldWorksStandards.CopyAttributesFromReferenceModel(ref swModel, ref swRefModel);

                // check drafter
                SldWorksStandards.CheckDrafter(ref swApp, ref swModel, ref swRefModel);

                string revision = SldWorksStandards.getAttribute(ref swApp, SldWorksConstants.sldworks_attr_revision);

                try
                {
                    taskpane.fillControls();
                }
                catch(Exception e)
                {
                    DebugBuilder.PrintError(e);
                }
            }
        }

        public void SynchDrawingToModel()
        {
            if (SWModelConnector.GetInstance().swModel.GetType() != (int)swDocumentTypes_e.swDocDRAWING) { return; }

            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;
            DrawingDoc swDrawing = (DrawingDoc)swModel;
            ModelDoc2 swRefModel = (ModelDoc2)((View)swDrawing.GetFirstView()).GetNextView().ReferencedDocument;

            // get custom property manager

            CustomPropertyManager swCustPropMgr = swModel.Extension.get_CustomPropertyManager("");

            string[] attributes = swCustPropMgr.GetNames();
            
            foreach (string attribute in attributes)
            {
                string value = swCustPropMgr.Get(attribute);

                try
                {
                    Debug.Print($"Set attribute to reference: ({attribute}, {value})");

                    SldWorksStandards.setAttribute(ref swRefModel, attribute, value);
                }
                catch(Exception e)
                {
                    Debug.Print($"Error setting attribute to Reference: {e.ToString()}");
                }
            }
        }

        public void SetRevisionAttribute()
        {
            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;
            string oldRevisionAttribute = "PTC_WM_REVISION";
            List<string> attrTables;

            if (swModel.GetType() != (int)swDocumentTypes_e.swDocDRAWING)
            {
                attrTables = new List<string>(swModel.GetConfigurationNames());
                attrTables.Add("");
            }
            else
            {
                attrTables = new List<string>();
                attrTables.Add("");
            }

            foreach (string table in attrTables)
            {
                CustomPropertyManager swCustPropMgr = swModel.Extension.get_CustomPropertyManager(table);
                string[] attributes = swCustPropMgr.GetNames();

                if (attributes.Contains(oldRevisionAttribute))
                {
                    string value = swCustPropMgr.Get(oldRevisionAttribute);

                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        swCustPropMgr.Add3(SldWorksConstants.sldworks_attr_revision, (int)swCustomInfoType_e.swCustomInfoText, value, (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                        swCustPropMgr.Delete2(oldRevisionAttribute);

                        string mainRevision = value.Substring(0, 1);
                        string intRevision = value.Substring(1, 1);

                        swCustPropMgr.Add3(SldWorksConstants.sldworks_attr_mainrevision, (int)swCustomInfoType_e.swCustomInfoText, mainRevision, (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                        swCustPropMgr.Add3(SldWorksConstants.sldworks_attr_intrevision, (int)swCustomInfoType_e.swCustomInfoText, intRevision, (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    }
                    else
                    {
                        swCustPropMgr.Add3(SldWorksConstants.sldworks_attr_revision, (int)swCustomInfoType_e.swCustomInfoText, "", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

                        swCustPropMgr.Delete2(oldRevisionAttribute);
                    }
                    
                }
            }
            
        }

        #endregion

        /*
        public void copyAttributeList()
        {
            ModelDoc2 swModel = swApp.ActiveDoc;

            if (!(swModel.GetType() == (int)swDocumentTypes_e.swDocDRAWING))
            {
                try
                {
                    Debug.Print("CREATE TEMPLATE");

                    AttributeTemplate.createTemplate(swApp);

                    if (!AttributeTemplate.migAttributePresent)
                    {
                        SldWorksStandards.CopyListOpenMethod(ref swModel);
                        Debug.Print("COPIED LIST 1");
                    }
                    else if (AttributeTemplate.locateAttribute("HYDRO_MIG_CONVERTED").Item1)
                    {
                        SldWorksStandards.CopyListOpenMethod(ref swModel);
                        Debug.Print("COPIED LIST 2");
                    }
                }
                catch (Exception e)
                {
                    Debug.Print(e.ToString());
                }
            }

            swModel = null;
            
        }

        public void startAttribution()
        {
            // Implement Attribution method
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            // check if SAP information was migrated
            if (swModel != null)
            {
                try
                {
                    Debug.Print("INITIATE ATTRIBUTION ################");
                    AttributeTemplate.AttributionMethod(swApp, swModel);
                }
                catch (Exception e)
                {
                    Debug.Print(e.ToString());
                }
            }

            swModel = null;
            
        }

        #endregion
        */

        #region private methods

        private void setLoadAttributes(string attrSource, string attrTarget, Func<string, string> valFilter = null)
        {
            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;

            string attrSourceValue = HydroSolidworksLibrary.SldWorksStandards.getConfigAttribute(ref swModel, attrSource);

            if (valFilter != null)
            {
                Configuration config = (Configuration)swModel.GetActiveConfiguration();
                string configName = config.Name;

                string customAttrValue = HydroSolidworksLibrary.SldWorksStandards.getAttributeByConfig(ref swModel, attrSource, configName);
                attrSourceValue = valFilter(customAttrValue);
            }

            if (attrSourceValue == "")
            {
                string attrValue = null;
                string attrResValue = null;
                bool resolved = true;

                Configuration swConfig = (Configuration)swModel.GetActiveConfiguration();
                CustomPropertyManager swCustPropMgr = swConfig.CustomPropertyManager;

                int exitcode = swCustPropMgr.Get5(attrSource, false, out attrValue, out attrResValue, out resolved);

                if (exitcode == (int)SwConst.swCustomInfoGetResult_e.swCustomInfoGetResult_ResolvedValue)
                {
                    Debug.Print(" :: Hydro Taskpane :: setLoadAttributes :: getAttribute (Config - " + swConfig.Name + ") :: attribute " + attrSource + " resolved to " + attrResValue.Trim() + "...");
                    attrSourceValue = attrResValue.Trim();
                }
                else
                {
                    Debug.Print(" :: Hydro Taskpane :: setLoadAttributes :: getAttribute (Config - " + swConfig.Name + ") :: attribute " + attrSource + " does not exist in file " + swModel.GetTitle() + " ...");
                }
            }

            Debug.Print($" :: Hydro Taskpane :: setLoadAttributes :: Set target |{attrTarget}| to source |{attrSource}| - Value: |{attrSourceValue}| :: ");
            HydroSolidworksLibrary.SldWorksStandards.setAttribute(ref swModel, attrTarget, attrSourceValue);
        }

        private string filterVName(string attrValue)
        {
            // check if string contains nested parentheses
            bool par = false;
            int count = 0;

            Debug.Print($"::: HydroTaskpane ::: filterVName ::: attrValue - {attrValue} :::");

            foreach (var c in attrValue)
            {
                switch (c)
                {
                    case '(':
                        count++;
                        break;
                    case ')':
                        count++;
                        break;
                }
            }

            Debug.Print($"::: HydroTaskpane ::: filterVName ::: counter - {count} :::");

            if (count == 2)
            {
                par = true;
            }

            // if contains parentheses, extract SAP number
            string result = "";

            if (par)
            {
                Debug.Print($"::: HydroTaskpane ::: filterVName ::: contains parentheses... :::");
                int index = attrValue.IndexOf("(");
                result = attrValue.Substring(0, index);
            }
            else
            {
                result = attrValue;
            }

            Debug.Print($"::: HydroTaskpane ::: filterVName ::: result value is |{result}| :::");

            return result;
        }

        #endregion

        #region BOM methods

        public void ProcessBOM()
        {
            Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: Start...");

            SldWorks.SldWorks swApp = SWModelConnector.GetInstance().swApp;
            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;

            if (swModel.GetType() != (int)swDocumentTypes_e.swDocASSEMBLY) { return; }

            string[] configNames = swModel.GetConfigurationNames();
            string activeConfig = ((Configuration)swModel.GetActiveConfiguration()).Name;

            foreach (string config in configNames)
            {
                Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: Check component references for configuration " + config + "...");
                // Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: Set component references...");
                SldWorksStandards.SetComponentReferences(ref swApp);
            }

            Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: reload active configurate " + activeConfig + "...");

            swModel.ShowConfiguration2(activeConfig);

            Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: create or update BOM...");
            UpdateBOM();

            Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: ...done.");
        }

        public void UpdateBOM()
        {
            Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: UpdateBOM :: Search for existing BOM...");

            // loop through features and check for BOMfeature
            SldWorks.SldWorks swApp = SWModelConnector.GetInstance().swApp;
            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;

            Feature swFeature = swModel.FirstFeature();
            bool createnew = true;

            while (swFeature != null)
            {
                Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: UpdateBOM :: swFeature = " + swFeature.Name + " " + swFeature.Description + " " + swFeature.GetTypeName().ToString() + " " + swFeature.GetTypeName2().ToString());

                if (swFeature.GetTypeName2() == "TableFolder")
                {
                    // Debug.Print(" :: Hydro Taskpane :: CheckCreateBOM :: TableFolder found...");

                    Feature subFeature = swFeature.GetFirstSubFeature();

                    while (subFeature != null)
                    {
                        Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: UpdateBOM :: subFeature = " + subFeature.Name + " " + subFeature.Description + " " + subFeature.GetTypeName().ToString() + " " + subFeature.GetTypeName2().ToString());

                        if (subFeature.GetTypeName2() == "BomFeat")
                        {
                            // BOM exists, do not create new one
                            Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: UpdateBOM :: BOM found...");
                            createnew = false;
                            // exit while loops
                            subFeature = null;
                            swFeature = null;
                        }

                        if (subFeature != null)
                            subFeature = swFeature.GetNextSubFeature();
                    }
                }

                if (swFeature != null)
                    swFeature = swFeature.GetNextFeature();
            }

            // if no BOM found, create new one
            if (createnew)
            {
                Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: UpdateBOM :: no BOM found...create new...");
                // try to get vName attribute, whenever the 3DX PLM Add-In is present - 10.01.2024

                string title = "";
                try
                {
                    bool AddIn = SldWorksStandards.AddInIsLoaded();

                    if (AddIn)
                    {
                        title = SldWorksStandards.getConfigAttribute(ref swModel, "V_Name");
                    }
                }
                catch { }

                // #########################

                // create BOM

                string[] configNames = (string[])swModel.GetConfigurationNames();
                int index = 0;
                for (int i = 0; i < configNames.Length; i++)
                {
                    if (configNames[i].Contains(SldWorksConstants.defaultconfiguration) || configNames[i].Contains(title))
                    {
                        Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: UpdateBOM :: default config found: " + configNames[i] + "...");
                        break;
                    }
                    index++;
                }

                if (index < configNames.Length)
                {
                    Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: UpdateBOM :: zoom to fit...");
                    swModel.ViewZoomtofit2();
                    Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: UpdateBOM :: create BOM table with new component references...");
                    BomTableAnnotation swBOMTable = swModel.Extension.InsertBomTable3(SldWorksConstants.filename_bom_template, 200, 100, (int)swBomType_e.swBomType_TopLevelOnly, configNames[index], false, (int)swNumberingType_e.swIndentedBOMNotSet, false);
                    Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: UpdateBOM :: create BOM feature...");
                    BomFeature swBomFeature = (BomFeature)swBOMTable.BomFeature;
                    Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: UpdateBOM :: set assembly order...");
                    swBomFeature.FollowAssemblyOrder2 = true;
                }
                else
                {
                    Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: UpdateBOM :: no config " + SldWorksConstants.defaultconfiguration + " or " + title + " found...");
                    swApp.SendMsgToUser2("BOM Warning: " + System.Environment.NewLine + "No configuration " + SldWorksConstants.defaultconfiguration + " or " + title + " found!" + System.Environment.NewLine + "Please create BOM manually...", (int)swMessageBoxIcon_e.swMbStop, (int)swMessageBoxBtn_e.swMbOk);
                }
            }
            // if BOM found update
            else
            {
                Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: UpdateBOM :: BOM found...update only...");
            }

            Debug.Print(" :: Hydro Taskpane :: ProcessBOM :: UpdateBOM :: Rebuild...");
            swModel.ForceRebuild3(true);
        }

        #endregion

    }
}
