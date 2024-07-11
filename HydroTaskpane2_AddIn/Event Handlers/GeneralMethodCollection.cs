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
using HydroTaskpane2_AddIn.Attribution;

namespace HydroTaskpane2_AddIn.Event_Handlers
{
    public class GeneralMethodCollection
    {

        #region public methods

        public static void setDescription(SldWorks.SldWorks swApp)
        {
            try
            {
                ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

                bool AddIn = HydroSolidworksLibrary.SldWorksStandards.AddInIsLoaded();
                bool checkIntegration = HydroSolidworksLibrary.SldWorksStandards.checkIntegrationCheckbox();

                if (AddIn && checkIntegration)
                {
                    // check for V_Name
                    setLoadAttributes(swApp, "V_Name", "SAP_MATNR", filterVName);

                    // search description attribute within current configuration

                    Configuration configuration = swModel.GetActiveConfiguration();
                    CustomPropertyManager swCustPropMgr = configuration.CustomPropertyManager;

                    string[] attributes = swCustPropMgr.GetNames();

                    foreach (string attr in attributes)
                    {
                        if (attr.ToLower() == "description")
                        {
                            setLoadAttributes(swApp, "HYDRO_CAD_DESCRIPTION_COMPLETE_EN", attr);
                            break;
                        }
                    }
                }

                swModel = null;
            }
            catch (Exception e)
            {
                Debug.Print(" :: Hydro Taskpane :: ERROR - " + e.ToString() + " :: ");
            }
        }

        public static void copyAttributeList(SldWorks.SldWorks swApp)
        {
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            if (!(swModel.GetType() == (int)swDocumentTypes_e.swDocDRAWING))
            {
                try
                {
                    Debug.Print("CREATE TEMPLATE");
                    AttributeTemplate.createTemplate(ref swApp);

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

        public static void startAttribution(SldWorks.SldWorks swApp)
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

        #region private methods

        private static void setLoadAttributes(SldWorks.SldWorks swApp, string attrSource, string attrTarget, Func<string, string> valFilter = null)
        {
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

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

        private static string filterVName(string attrValue)
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

    }
}
