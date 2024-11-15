using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using SwCommands;
using SwConst;
using HydroSolidworksLibrary;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace HydroTaskpane2_AddIn.SWEventHandlerStrategy.AttributeTemplates
{
    public static class AttributeTemplate
    {
        public static bool migAttributePresent = locateAttribute("HYDRO_MIG_SAP").Item1;

        // List with all SW Template attributes
        public static List<string> template = new List<string>
        {
           SldWorksConstants.sldworks_attr_generaltolerances,
           SldWorksConstants.sldworks_attr_thermalcut,
           SldWorksConstants.sldworks_attr_semifinished,
           SldWorksConstants.sldworks_attr_description_en,
           SldWorksConstants.sldworks_attr_description_de,
           SldWorksConstants.sldworks_attr_description_complete_en,
           SldWorksConstants.sldworks_attr_description_complete_de,
           SldWorksConstants.sldworks_attr_additionaltext_en,
           SldWorksConstants.sldworks_attr_additionaltext_de,
           SldWorksConstants.sldworks_attr_caddescription,
           SldWorksConstants.sldworks_attr_material,
           SldWorksConstants.sldworks_attr_material_number,
           SldWorksConstants.sldworks_attr_material_en,
           SldWorksConstants.sldworks_attr_material_de,
           SldWorksConstants.sldworks_attr_materialalt,
           SldWorksConstants.sldworks_attr_materialalt_number,
           SldWorksConstants.sldworks_attr_materialalt_en,
           SldWorksConstants.sldworks_attr_materialalt_de,
           SldWorksConstants.sldworks_attr_weight,
           SldWorksConstants.sldworks_attr_heattreat_en,
           SldWorksConstants.sldworks_attr_heattreat_de,
           SldWorksConstants.sldworks_attr_preconditioning_en,
           SldWorksConstants.sldworks_attr_preconditioning_de,
           SldWorksConstants.sldworks_attr_surface_en,
           SldWorksConstants.sldworks_attr_surface_de,
           SldWorksConstants.sldworks_attr_additionalinfo_en,
           SldWorksConstants.sldworks_attr_additionalinfo_de,
           SldWorksConstants.sldworks_attr_components,
           SldWorksConstants.sldworks_attr_assemblymode_en,
           SldWorksConstants.sldworks_attr_assemblymode_de,
           SldWorksConstants.sldworks_attr_sapmaterial,
           SldWorksConstants.sldworks_attr_weldment,
           SldWorksConstants.sldworks_attr_weldment_dyepenetration_en,
           SldWorksConstants.sldworks_attr_weldment_dyepenetration_de,
           SldWorksConstants.sldworks_attr_weldment_visualinspection_en,
           SldWorksConstants.sldworks_attr_weldment_visualinspection_de,
           SldWorksConstants.sldworks_attr_weldment_qualityrequirement,
           SldWorksConstants.sldworks_attr_weldment_preparation,
           SldWorksConstants.sldworks_attr_weldment_qualityimperfections,
           SldWorksConstants.sldworks_attr_weldment_generaltolerance,
           SldWorksConstants.sldworks_attr_surface_norm,
           SldWorksConstants.sldworks_attr_edges_norm,
           SldWorksConstants.sldworks_attr_units,
           SldWorksConstants.sldworks_attr_dimension1,
           SldWorksConstants.sldworks_attr_dimension2,
           SldWorksConstants.sldworks_attr_dimension3,
           SldWorksConstants.sldworks_attr_drawnon,
           SldWorksConstants.sldworks_attr_drawnby,
           SldWorksConstants.sldworks_attr_drawnby_firstrelease,
           SldWorksConstants.sldworks_attr_drawnon_firstrelease,
           SldWorksConstants.sldworks_attr_checkedby,
           SldWorksConstants.sldworks_attr_checkedby_firstrelease,
           SldWorksConstants.sldworks_attr_checkedon,
           SldWorksConstants.sldworks_attr_checkedon_firstrelease,
           SldWorksConstants.sldworks_attr_changeoccurs,
           SldWorksConstants.sldworks_attr_changedescription,
           SldWorksConstants.sldworks_attr_changenumber,
           SldWorksConstants.sldworks_attr_basedon,
           SldWorksConstants.sldworks_attr_basedon_sldworks,
           SldWorksConstants.sldworks_attr_standardcolor,
           SldWorksConstants.sldworks_attr_standardcolor_en,
           SldWorksConstants.sldworks_attr_standardcolor_de,
           SldWorksConstants.sldworks_attr_lacquering,
           SldWorksConstants.sldworks_attr_lacquering_en,
           SldWorksConstants.sldworks_attr_lacquering_de,
           "DESCRIPTION"
        };

        public static List<string> migAttributes = new List<string>
        {
            SldWorksConstants.sldworks_attr_description_complete_en,
            SldWorksConstants.sldworks_attr_description_complete_de,
            SldWorksConstants.sldworks_attr_material,
            SldWorksConstants.sldworks_attr_material_number,
            SldWorksConstants.sldworks_attr_materialalt,
            SldWorksConstants.sldworks_attr_materialalt_number,
            SldWorksConstants.sldworks_attr_materialadd,
            SldWorksConstants.sldworks_attr_standardcolor,
            SldWorksConstants.sldworks_attr_lacquering,
            SldWorksConstants.sldworks_attr_sapmaterial,
            "HYDRO_MIG_SAP"
        };

        // additional attributes to the override
        #region override add.

        public static Dictionary<string, List<string>> migAttributesLines = new Dictionary<string, List<string>>
        {
            {SldWorksConstants.sldworks_attr_material, new List<string>{SldWorksConstants.sldworks_attr_material, SldWorksConstants.sldworks_attr_material_number, SldWorksConstants.sldworks_attr_material_en, SldWorksConstants.sldworks_attr_material_de }},
            {SldWorksConstants.sldworks_attr_materialalt, new List<string>{SldWorksConstants.sldworks_attr_materialalt, SldWorksConstants.sldworks_attr_materialalt_number, SldWorksConstants.sldworks_attr_materialalt_en, SldWorksConstants.sldworks_attr_materialalt_de }},
            {SldWorksConstants.sldworks_attr_standardcolor, new List<string>{"HYDRO_CAD_STANDARDCOLOR", "HYDRO_CAD_STANDARDCOLOR_EN", "HYDRO_CAD_STANDARDCOLOR_DE"}},
            {SldWorksConstants.sldworks_attr_lacquering, new List<string>{"HYDRO_CAD_LACQUERING", "HYDRO_CAD_LACQUERING_EN", "HYDRO_CAD_LACQUERING_DE"}}
        };

        public static Dictionary<string, Dictionary<int, string>> taskpaneMaterialDict;
        public static Dictionary<string, Dictionary<int, string>> taskpaneColorDict;

        #endregion

        public static Dictionary<string, Dictionary<string, string>> swAttributes = new Dictionary<string, Dictionary<string, string>>();
        public static Dictionary<string, string> migAttributePairs;

        public static void CodeMarker(ref SldWorks.SldWorks swApp, string msg)
        {
            int icon = (int)swMessageBoxIcon_e.swMbQuestion;
            int button = (int)swMessageBoxBtn_e.swMbOkCancel;

            swApp.SendMsgToUser2(msg, icon, button);

        }

        public static void createTemplate(SldWorks.SldWorks swApp)
        {
            bool PLMLoaded = SldWorksStandards.AddInIsLoaded();

            if (!PLMLoaded) { return; }

            ModelDoc2 swModel = swApp.ActiveDoc;

            // loop and fill template through Custom
            CustomPropertyManager swCustPropMgr = (CustomPropertyManager)swModel.Extension.get_CustomPropertyManager("");
            string[] customAttr = swCustPropMgr.GetNames();

            foreach (string attr in template)
            {

                if (customAttr == null)
                {
                    swCustPropMgr.Add3(attr, (int)swCustomInfoType_e.swCustomInfoText, "", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                }
                else if (!customAttr.Contains(attr))
                {
                    swCustPropMgr.Add3(attr, (int)swCustomInfoType_e.swCustomInfoText, "", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                }
            }

            swCustPropMgr = null;

            if (!(swModel.GetType() == (int)swDocumentTypes_e.swDocDRAWING))
            {

                // loop and fill template through Configurations

                string[] configNames = swModel.GetConfigurationNames();
                Configuration swConfig = default(Configuration);

                foreach (string config in configNames)
                {
                    swConfig = swModel.GetConfigurationByName(config);
                    CustomPropertyManager swConfigPropMgr = swConfig.CustomPropertyManager;
                    string[] configAttr = swConfigPropMgr.GetNames();

                    foreach (string attr in template)
                    {
                        if (configAttr == null)
                        {
                            swConfigPropMgr.Add3(attr, (int)swCustomInfoType_e.swCustomInfoText, "", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                        }
                        else if (!configAttr.Contains(attr))
                        {
                            swConfigPropMgr.Add3(attr, (int)swCustomInfoType_e.swCustomInfoText, "", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                        }
                    }
                }

            }

            swModel = null;
        }

        public static void GetAttributeLists()
        {
            SldWorks.SldWorks swApp = (SldWorks.SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            migAttributePairs = new Dictionary<string, string>();
            swAttributes = new Dictionary<string, Dictionary<string, string>>();

            // Retrieve migration Attributes and configuration attributes - ok

            if (!(swModel.GetType() == (int)swDocumentTypes_e.swDocDRAWING))
            {
                Configuration swConfig = default(Configuration);
                string[] configNames = swModel.GetConfigurationNames();

                foreach (string config in configNames)
                {
                    swConfig = swModel.GetConfigurationByName(config);
                    CustomPropertyManager swConfigPropMgr = (CustomPropertyManager)swConfig.CustomPropertyManager;
                    Dictionary<string, string> swConfigAttributes = new Dictionary<string, string>();

                    string[] configAttributes = swConfigPropMgr.GetNames();

                    try
                    {
                        string migVal = swConfigPropMgr.Get("HYDRO_MIG_SAP");
                        Debug.Print($"HYDRO_MIG_SAP : {migVal}");
                    }
                    catch { }

                    if (configAttributes.Contains("HYDRO_MIG_SAP"))
                    {
                        foreach (string attr in migAttributes)
                        {
                            try
                            {
                                string attrVal = swConfigPropMgr.Get(attr);
                                migAttributePairs.Add(attr, attrVal);
                                Debug.Print($"::: AttributesTemplate ::: GetAttributesList ::: Attribute - {attr}; Value - {attrVal} :::");
                            }
                            catch { }
                        }

                    }

                    foreach (string configAttr in configAttributes)
                    {
                        string configAttrVal = swConfigPropMgr.Get(configAttr);
                        swConfigAttributes.Add(configAttr, configAttrVal);
                    }

                    swAttributes.Add(config, swConfigAttributes);

                    swConfig = null;
                    swConfigPropMgr = null;
                }

                // Retrieve custom attributes

                CustomPropertyManager SwCustPropMgr = (CustomPropertyManager)swModel.Extension.get_CustomPropertyManager("");
                Dictionary<string, string> swCustomAttributes = new Dictionary<string, string>();

                string[] customAttributes = SwCustPropMgr.GetNames();

                foreach (string customAttr in customAttributes)
                {
                    string customAttrVal = SwCustPropMgr.Get(customAttr);
                    swCustomAttributes.Add(customAttr, customAttrVal);
                }

                swAttributes.Add("Custom", swCustomAttributes);

                SwCustPropMgr = null;
                swModel = null;
                swApp = null;

            }
            else
            {
                // Retrieve custom attributes from drawing

                CustomPropertyManager SwCustPropMgr = (CustomPropertyManager)swModel.Extension.get_CustomPropertyManager("");
                Dictionary<string, string> swCustomAttributes = new Dictionary<string, string>();

                string[] customAttributes = SwCustPropMgr.GetNames();

                foreach (string customAttr in customAttributes)
                {

                    try
                    {
                        string migVal = SwCustPropMgr.Get("HYDRO_MIG_SAP");
                        Debug.Print($"HYDRO_MIG_SAP : {migVal}");
                    }
                    catch { }

                    if (customAttributes.Contains("HYDRO_MIG_SAP"))
                    {
                        foreach (string attr in migAttributes)
                        {
                            try
                            {
                                string attrVal = SwCustPropMgr.Get(attr);
                                migAttributePairs.Add(attr, attrVal);
                                Debug.Print($"::: AttributesTemplate ::: GetAttributesList ::: Attribute - {attr}; Value - {attrVal} :::");
                            }
                            catch { }
                        }

                    }

                    string customAttrVal = SwCustPropMgr.Get(customAttr);
                    swCustomAttributes.Add(customAttr, customAttrVal);
                }

                swAttributes.Add("Custom", swCustomAttributes);

                SwCustPropMgr = null;
                swModel = null;
                swApp = null;
            }
        }

        public static void getTaskpaneInfo()
        {
            string materialPath = @"\\CAD_DE_SW\D_sw-pool\Hydro\System-Optionen\Macros\HydroTaskpane\Data\part_material.dat"; // changed to UNC 09.02.2024 + back to F:\ 12.03.2024
            string colorPath = @"\\CAD_DE_SW\D_sw-pool\Hydro\System-Optionen\Macros\HydroTaskpane\Data\mix_standardcolor.dat"; // changed to UNC 09.02.2024

            List<string> materialList = new List<string>(File.ReadAllLines(materialPath));
            List<string> colorList = new List<string>(File.ReadAllLines(colorPath));

            // get material info
            taskpaneMaterialDict = new Dictionary<string, Dictionary<int, string>>();

            foreach (string material in materialList)
            {
                Dictionary<int, string> keyValuePairs = new Dictionary<int, string>
                {
                    {0, material.Split(';')[0]},
                    {1, material.Split(';')[1]},
                    {2, material.Split(';')[2]},
                    {3, material.Split(';')[3]}
                };

                taskpaneMaterialDict.Add(material.Split(';')[0], keyValuePairs);
            }

            // get color info
            taskpaneColorDict = new Dictionary<string, Dictionary<int, string>>();

            foreach (string color in colorList)
            {
                Dictionary<int, string> keyValuePairs = new Dictionary<int, string>
                {
                    {0, color.Split(';')[0]},
                    {1, color.Split(';')[1]},
                    {2, color.Split(';')[2]},
                };

                taskpaneColorDict.Add(color.Split(';')[0], keyValuePairs);
            }

            // Add Attributes that can be added
            foreach (string attrKey in migAttributesLines.Keys)
            {
                if (migAttributePairs[attrKey] != "")
                {
                    string keyValue = migAttributePairs[attrKey];

                    for (int i = 0; i < migAttributesLines[attrKey].Count(); i++)
                    {
                        if (attrKey.Contains("MATERIAL"))
                        {
                            if (!migAttributePairs.Keys.Contains(migAttributesLines[attrKey][i]))
                            {
                                migAttributePairs.Add(migAttributesLines[attrKey][i], taskpaneMaterialDict[keyValue][i]);
                            }
                        }
                        else if (attrKey.Contains("COLOR") || attrKey.Contains("LACQUERING"))
                        {
                            if (!migAttributePairs.Keys.Contains(migAttributesLines[attrKey][i]))
                            {
                                migAttributePairs.Add(migAttributesLines[attrKey][i], taskpaneColorDict[keyValue][i]);
                            }
                        }
                    }
                }
            }

        }

        public static Tuple<bool, string> locateAttribute(string attributeName)
        {
            Tuple<bool, string> output = null;
            SldWorks.SldWorks swApp = (SldWorks.SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            // look for attribute in Config
            CustomPropertyManager swCustPropMgr = (CustomPropertyManager)swModel.Extension.get_CustomPropertyManager("");

            if (!(swModel.GetType() == (int)swDocumentTypes_e.swDocDRAWING))
            {
                string[] configNames = swModel.GetConfigurationNames();
                Configuration swConfig = default(Configuration);

                foreach (string config in configNames)
                {
                    swConfig = swModel.GetConfigurationByName(config);
                    swCustPropMgr = (CustomPropertyManager)swConfig.CustomPropertyManager;
                    string[] configAttrNames = swCustPropMgr.GetNames();

                    if (configAttrNames != null)
                    {
                        if (configAttrNames.Contains(attributeName))
                        {
                            string attrValue = swCustPropMgr.Get(attributeName);
                            output = new Tuple<bool, string>(true, attrValue);

                            return output;
                        }
                    }
                }
            }

            // look for attribute in custom
            swCustPropMgr = (CustomPropertyManager)swModel.Extension.get_CustomPropertyManager("");

            string[] customAttrNames = swCustPropMgr.GetNames();

            if (customAttrNames != null)
            {
                if (customAttrNames.Contains(attributeName))
                {
                    string attrValue = swCustPropMgr.Get(attributeName);
                    output = new Tuple<bool, string>(true, attrValue);

                    return output;
                }
            }

            swModel = null;
            swApp = null;

            output = new Tuple<bool, string>(false, "");

            return output;
        }

        public static void AttributionMethod(SldWorks.SldWorks swApp, ModelDoc2 swModel)
        {
            bool PLMLoaded = SldWorksStandards.AddInIsLoaded();

            if (!PLMLoaded) { return; }

            Tuple<bool, string> block = locateAttribute("HYDRO_MIG_CONVERTED");
            bool blockAttribute = block.Item1;
            string blockValue = block.Item2;

            if (blockAttribute)
            {
                return;
            }

            Tuple<bool, string> mig = locateAttribute("HYDRO_MIG_SAP");

            bool MigAttribute = mig.Item1;
            string MigValue = mig.Item2;

            if (PLMLoaded && MigAttribute && (MigValue == "Ja" || MigValue == "Yes" || MigValue == "TRUE"))
            {
                try
                {
                    // make sure that template exists
                    createTemplate(swApp);

                    // generate required Dicts
                    Debug.Print("::: Attributes Templates | AttributionMethod ::: generate required Dicts");
                    GetAttributeLists();
                    getTaskpaneInfo();

                    // Manipulate Dict to create reference Attribute list
                    Debug.Print("::: Attributes Templates | AttributionMethod ::: Manipulate Dict to create reference Attribute list");
                    ReferenceAttributeGenerator attrGenerator = new ReferenceAttributeGenerator(swAttributes, attrOver: migAttributePairs);
                    attrGenerator.attachAttributes(swModel);

                    Debug.Print("::: Attributes Templates | AttributionMethod ::: Set converted attribute to JA");
                    SldWorksStandards.setAttribute(ref swModel, "HYDRO_MIG_CONVERTED", "Ja");

                    Debug.Print("::: Attributes Templates | AttributionMethod ::: clear Dicts");
                    attrGenerator.clearDicts();
                    attrGenerator = null;

                    swAttributes = null;
                    migAttributePairs = null;
                }
                catch (Exception e)
                {
                    Debug.Print(e.ToString());
                    return;
                }
            }
            else
            {
                return;
            }

        }
    }
}
