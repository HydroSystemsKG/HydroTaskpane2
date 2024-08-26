using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using SwCommands;
using SwConst;
using HydroSolidworksLibrary;

namespace HydroTaskpane2_AddIn.SWEventHandlerStrategy.AttributeTemplates
{
    public class DescriptionGenerator
    {
        public static ModelDoc2 swModel;

        public static Dictionary<string, string> descriptionAttributes = new Dictionary<string, string>();

        public static Dictionary<int, List<string>> addDescriptions = new Dictionary<int, List<string>>
        {
            {0, new List<string>{"assy.","vollst."}},
            {1, new List<string>{"assy. (CE-standard)","vollst. (CE-Standard)"}},
            {2, new List<string>{"assy. (DE)","vollst. (DE)"}},
            {3, new List<string>{"assy. (DE, EN)","vollst. (DE, EN)"}},
            {4, new List<string>{"assy. (DE, EN, FR)","vollst. (DE, EN, FR)"}},
            {5, new List<string>{"assy. (EN)","vollst. (EN)"}},
            {6, new List<string>{"assy. (EN-CE)","vollst. (EN-CE)"}},
            {7, new List<string>{"assy. (EN-UL)","vollst. (EN-UL)"}},
            {8, new List<string>{"assy. (ES)","vollst. (ES)"}},
            {9, new List<string>{"assy. (FR)","vollst. (FR)"}},
            {10, new List<string>{"assy. (language-independent)","vollst. (sprachunabhaengig)"}},
            {11, new List<string>{"assy. (PL)","vollst. (PL)"}},
            {12, new List<string>{"assy. (PT)","vollst. (PT)"}},
            {13, new List<string>{"assy. (RU)","vollst. (RU)"}},
            {14, new List<string>{"assy. (TR)","vollst. (TR)"}},
            {15, new List<string>{"assy. (UL-standard)","vollst. (UL-Standard)"}},
            {16, new List<string>{"assy. (ZH)","vollst. (ZH)"}},
            {17, new List<string>{"assy., LH","vollst., LH"}},
            {18, new List<string>{"assy., RH","vollst., RH"}},
            {19, new List<string>{"compl.","kpl."}},
            {20, new List<string>{"compl., LH","kpl., LH"}},
            {21, new List<string>{"compl., raw part","kpl., Rohteil"}},
            {22, new List<string>{"compl., RH","kpl., RH"}},
            {23, new List<string>{"edited","bearbeitet"}},
            {24, new List<string>{"LH","LH"}},
            {25, new List<string>{"RH","RH"}},
            {26, new List<string>{"set","Satz"}}
        };

        public DescriptionGenerator(ref ModelDoc2 swModelInput)
        {
            swModel = swModelInput;

            generateDescriptionFromComplete();
            SetDescription();
        }

        private static void generateDescriptionFromComplete()
        {
            CustomPropertyManager swCustPropMgr = (CustomPropertyManager)swModel.Extension.get_CustomPropertyManager("");
            string[] attrNames = swCustPropMgr.GetNames();

            string descriptionCompleteEN = "HYDRO_CAD_DESCRIPTION_COMPLETE_EN";
            string descriptionCompleteDE = "HYDRO_CAD_DESCRIPTION_COMPLETE_DE";

            if (attrNames.Contains(descriptionCompleteEN) && attrNames.Contains(descriptionCompleteDE))
            {
                string descriptionCompleteENVal = swCustPropMgr.Get(descriptionCompleteEN);
                string descriptionCompleteDEVal = swCustPropMgr.Get(descriptionCompleteDE);

                string descriptionEN = descriptionCompleteENVal;
                string descriptionDE = descriptionCompleteDEVal;
                string descriptionAddEN = "";
                string descriptionAddDE = "";

                for (int i = 0; i < addDescriptions.Count; i++)
                {

                    descriptionAddEN = addDescriptions[i][0];
                    descriptionAddDE = addDescriptions[i][1];

                    if (descriptionCompleteEN.EndsWith(descriptionAddEN) && descriptionCompleteDE.EndsWith(descriptionAddDE))
                    {
                        descriptionEN = descriptionCompleteENVal.Replace(descriptionAddEN, "");
                        descriptionDE = descriptionCompleteDEVal.Replace(descriptionAddDE, "");
                        continue;
                    }
                    else
                    {
                        descriptionEN = descriptionCompleteENVal;
                        descriptionDE = descriptionCompleteDEVal;
                        descriptionAddEN = "";
                        descriptionAddDE = "";

                    }

                }

                descriptionAttributes.Add("HYDRO_CAD_DESCRIPTION_EN", descriptionEN);
                descriptionAttributes.Add("HYDRO_CAD_DESCRIPTION_DE", descriptionDE);
                descriptionAttributes.Add("HYDRO_CAD_DESCRIPTION_ADD_EN", descriptionAddEN);
                descriptionAttributes.Add("HYDRO_CAD_DESCRIPTION_ADD_DE", descriptionAddDE);

            }
        }

        private static void SetDescription()
        {
            foreach (string key in descriptionAttributes.Keys)
            {
                SldWorksStandards.setAttribute(ref swModel, key, descriptionAttributes[key]);
            }
        }

        public void clearDicts()
        {
            descriptionAttributes.Clear();
        }
    }
}
