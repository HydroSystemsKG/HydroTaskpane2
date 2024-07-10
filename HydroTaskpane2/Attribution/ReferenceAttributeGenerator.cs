using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using SwCommands;
using SwConst;
using HydroSolidworksLibrary;
using System.Diagnostics;

namespace HydroTaskpane2.Attribution
{
    public class ReferenceAttributeGenerator
    {
        // List with all SW Template attributes
        public static List<string> template = new List<string>
        {
           "HYDRO_CAD_GENERALTOLERANCE_NORM",
           "HYDRO_CAD_CUTTING_NORM",
           "HYDRO_CAD_SEMIFINISHED_NORM",
           "HYDRO_CAD_DESCRIPTION_EN",
           "HYDRO_CAD_DESCRIPTION_DE",
           "HYDRO_CAD_DESCRIPTION_COMPLETE_EN",
           "HYDRO_CAD_DESCRIPTION_COMPLETE_DE",
           "HYDRO_CAD_DESCRIPTION_ADD_EN",
           "HYDRO_CAD_DESCRIPTION_ADD_DE",
           "HYDRO_CAD_DESCRIPTION_CAD",
           "HYDRO_CAD_MATERIAL",
           "HYDRO_CAD_MATERIAL_ADDITIONAL",
           "HYDRO_CAD_MATERIAL_NUMBER",
           "HYDRO_CAD_MATERIAL_EN",
           "HYDRO_CAD_MATERIAL_DE",
           "HYDRO_CAD_MATERIAL_ALT",
           "HYDRO_CAD_MATERIAL_ALT_NUMBER",
           "HYDRO_CAD_MATERIAL_ALT_EN",
           "HYDRO_CAD_MATERIAL_ALT_DE",
           "HYDRO_CAD_WEIGHT_KG",
           "HYDRO_CAD_HEATTREATMENT_EN",
           "HYDRO_CAD_HEATTREATMENT_DE",
           "HYDRO_CAD_PRECONDITIONING_EN",
           "HYDRO_CAD_PRECONDITIONING_DE",
           "HYDRO_CAD_SURFACEFINISH_EN",
           "HYDRO_CAD_SURFACEFINISH_DE",
           "HYDRO_CAD_ADDITIONAL_INFORMATION_EN",
           "HYDRO_CAD_ADDITIONAL_INFORMATION_DE",
           "HYDRO_CAD_COMPONENTS",
           "HYDRO_CAD_ASSEMBLYMODE_EN",
           "HYDRO_CAD_ASSEMBLYMODE_DE",
           "SAP_MATNR",
           "HYDRO_CAD_WELDMENT",
           "HYDRO_CAD_WELDMENT_DYEPENETRATION_INSP_EN ",
           "HYDRO_CAD_WELDMENT_DYEPENETRATION_INSP_DE",
           "HYDRO_CAD_WELDMENT_VISUALINSPECTION_EN",
           "HYDRO_CAD_WELDMENT_VISUALINSPECTION_DE",
           "HYDRO_CAD_WELDMENT_QUALITYREQUIREMENT_NORM",
           "HYDRO_CAD_WELDMENT_PREPARATION_NORM",
           "HYDRO_CAD_WELDMENT_QUALITYLEVEL_NORM",
           "HYDRO_CAD_WELDMENT_GENERALTOLERANCE_NORM",
           "HYDRO_CAD_SURFACE_NORM",
           "HYDRO_CAD_EDGES_NORM",
           "HYDRO_CAD_UNITS",
           "HYDRO_CAD_DIM1",
           "HYDRO_CAD_DIM2",
           "HYDRO_CAD_DIM3",
           "HYDRO_CAD_DRAWN_ON",
           "HYDRO_CAD_DRAWN_BY",
           "HYDRO_CAD_DRAWN_BY_FIRSTRELEASE",
           "HYDRO_CAD_DRAWN_ON_FIRSTRELEASE",
           "HYDRO_CAD_CHECKEDBY",
           "HYDRO_CAD_CHECKEDBY_FIRSTRELEASE",
           "HYDRO_CAD_CHECKEDON",
           "HYDRO_CAD_CHECKEDON_FIRSTRELEASE",
           "HYDRO_CAD_CHANGEOCCURS",
           "HYDRO_CAD_CHANGEDESCRIPTION",
           "HYDRO_CAD_CHANGENUMBER",
           "HYDRO_CAD_BASEDON",
           "HYDRO_CAD_BASEDON_SLDWORKS",
           "HYDRO_CAD_STANDARDCOLOR",
           "HYDRO_CAD_STANDARDCOLOR_EN",
           "HYDRO_CAD_STANDARDCOLOR_DE",
           "HYDRO_CAD_LACQUERING",
           "HYDRO_CAD_LACQUERING_EN",
           "HYDRO_CAD_LACQUERING_DE",
           "DESCRIPTION"
        };

        // final attribute list
        public static Dictionary<string, string> swAttributes = new Dictionary<string, string>();

        // custom attributes
        public static Dictionary<string, string> swCustomAttributes;

        // configuration specific attributes
        public static Dictionary<string, string> swMainConfigAttributes = new Dictionary<string, string>();
        public static Dictionary<string, Dictionary<string, string>> swConfigAttributes;

        // attributes that can be overriden on the final list
        public static Dictionary<string, string> swAttributeOverride;

        public ReferenceAttributeGenerator(Dictionary<string, Dictionary<string, string>> swAttributeInput, Dictionary<string, string> attrOver = null)
        {
            // Assign variables
            swCustomAttributes = new Dictionary<string, string>(swAttributeInput["Custom"]);
            swAttributeOverride = attrOver;

            // use methods to generate attribute list
            if (swAttributeInput.Keys.Count() > 1) // ADD POSSIBILITY OF NULL CONFIG LIST
            {
                swConfigAttributes = new Dictionary<string, Dictionary<string, string>>(swAttributeInput);
                swConfigAttributes.Remove("Custom");

                // create main config dict 
                generateMainConfigDict();
            }

            // get final dict
            generateReferenceDict();

            // list all main attributes (Debug)
            // get main config
            foreach (string configAttr in swMainConfigAttributes.Keys)
            {
                Debug.Print("::: ReferenceAttributeGenerator ::: Constructor ::: (Main Config) " + $"Attribute: {configAttr}; Value: {swMainConfigAttributes[configAttr]}");
            }

            Debug.Print("################################");

            // get custom
            foreach (string customAttr in swCustomAttributes.Keys)
            {
                Debug.Print("::: ReferenceAttributeGenerator ::: Constructor ::: (Custom) " + $"Attribute: {customAttr}; Value: {swCustomAttributes[customAttr]}");
            }

            // get final dict
            foreach (string attr in swAttributes.Keys)
            {
                Debug.Print("::: ReferenceAttributeGenerator ::: Constructor ::: (Final) " + $"Attribute: {attr}; Value: {swAttributes[attr]}");
            }

            Debug.Print("################################");

            Debug.Print("::: ReferenceAttributeGenerator ::: Constructor ::: Commencing overrideAttributeReference...");
            overrideAttributeReference();
            Debug.Print("::: ReferenceAttributeGenerator ::: Constructor ::: ... overrideAttributeReference complete");

        }

        public void clearDicts()
        {
            swAttributes.Clear();
            swCustomAttributes.Clear();
            swMainConfigAttributes.Clear();
            swConfigAttributes.Clear();
            swAttributeOverride.Clear();
        }

        public void generateMainConfigDict()
        {
            foreach (string attr in template)
            {
                List<string> attrCompare = new List<string>();
                string finalAttrVal;

                foreach (string key in swConfigAttributes.Keys)
                {
                    string attrVal = null;

                    try
                    {
                        attrVal = swConfigAttributes[key][attr];
                    }
                    catch
                    {
                    }

                    attrCompare.Add(attrVal);
                }

                if (attrCompare.Any(o => o != attrCompare[0]))
                {
                    finalAttrVal = null;
                }
                else
                {
                    finalAttrVal = attrCompare[0];
                }

                swMainConfigAttributes.Add(attr, finalAttrVal);

            }
        }

        public void generateReferenceDict()
        {
            foreach (string attr in template)
            {
                string attrVal;
                string customVal;
                string configVal;

                try
                {
                    customVal = swCustomAttributes[attr];
                    if (customVal == "")
                    {
                        customVal = null;
                    }
                }
                catch
                {
                    customVal = null;
                }

                try
                {
                    configVal = swMainConfigAttributes[attr];
                    if (configVal == "")
                    {
                        configVal = null;
                    }
                }
                catch
                {
                    configVal = null;
                }

                if (customVal == configVal) { attrVal = customVal; }
                else if (customVal == null && configVal != null) { attrVal = configVal; }
                else if (customVal != null && configVal == null) { attrVal = customVal; }
                else { attrVal = customVal; }

                swAttributes.Add(attr, attrVal);
            }
        }

        public void overrideAttributeReference()
        {
            if (swAttributeOverride == null)
            {
                return;
            }

            List<string> overrideList = new List<string>(swAttributeOverride.Keys);
            List<string> keyList = new List<string>(swAttributes.Keys);

            foreach (string key in keyList)
            {
                if (overrideList.Contains(key))
                {
                    swAttributes[key] = swAttributeOverride[key];
                }
            }
        }

        public void attachAttributes(ModelDoc2 swModel)
        {
            foreach (string key in swAttributes.Keys)
            {
                SldWorksStandards.setAttribute(ref swModel, key, swAttributes[key]);
            }

            // modify Description attributes
            if (swAttributes["HYDRO_CAD_DESCRIPTION_COMPLETE_EN"] != null && swAttributes["HYDRO_CAD_DESCRIPTION_COMPLETE_DE"] != null)
            {
                DescriptionGenerator dGen = new DescriptionGenerator(ref swModel);
                dGen.clearDicts();
            }
        }
    }
}
