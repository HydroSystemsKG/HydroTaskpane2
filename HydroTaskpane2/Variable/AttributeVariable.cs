using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroSolidworksLibrary;

namespace HydroTaskpane2.Variable
{
    public class AttributeVariable
    {
        public static Dictionary<string, List<string>> controlAttributes = new Dictionary<string, List<string>>
        {
            {"Description", new List<string>{ SldWorksConstants.sldworks_attr_description_en, SldWorksConstants.sldworks_attr_description_de} },
            {"Additional Description", new List<string>{ SldWorksConstants.sldworks_attr_additionaltext_en, SldWorksConstants.sldworks_attr_additionaltext_de } },
            {"Additional Text", new List<string>{ SldWorksConstants.sldworks_attr_additionaltext_en, SldWorksConstants.sldworks_attr_additionaltext_de } },
            {"CAD Description", new List<string>{ SldWorksConstants.sldworks_attr_caddescription } },
            {"Material Hydro Variant 1", new List<string>{ SldWorksConstants.sldworks_attr_material, SldWorksConstants.sldworks_attr_material_number, SldWorksConstants.sldworks_attr_material_en, SldWorksConstants.sldworks_attr_material_de } },
            {"Material Hydro Variant 2", new List<string>{ SldWorksConstants.sldworks_attr_materialalt, SldWorksConstants.sldworks_attr_materialalt_number, SldWorksConstants.sldworks_attr_materialalt_en, SldWorksConstants.sldworks_attr_materialalt_de } },
            {"Material Hydro Additional Text", new List<string>{ SldWorksConstants.sldworks_attr_materialadd } },
            {"Material Original", new List<string>{ SldWorksConstants.sldworks_attr_materialorig, SldWorksConstants.sldworks_attr_materialorig_number, SldWorksConstants.sldworks_attr_materialorig_en, SldWorksConstants.sldworks_attr_materialorig_de } },
            {"Material Original Additional Text", new List<string>{ SldWorksConstants.sldworks_attr_materialorigadd } },
            {"Heat Treatment", new List<string>{ SldWorksConstants.sldworks_attr_heattreat_en, SldWorksConstants.sldworks_attr_heattreat_de } },
            {"Preconditioning", new List<string>{ SldWorksConstants.sldworks_attr_preconditioning_en, SldWorksConstants.sldworks_attr_preconditioning_de } },
            {"Surface Finish", new List<string>{ SldWorksConstants.sldworks_attr_surface_en, SldWorksConstants.sldworks_attr_surface_de } },
            {"Standard Color", new List<string>{ SldWorksConstants.sldworks_attr_standardcolor, SldWorksConstants.sldworks_attr_standardcolor_en, SldWorksConstants.sldworks_attr_standardcolor_de } },
            {"Lacquering", new List<string>{ SldWorksConstants.sldworks_attr_lacquering, SldWorksConstants.sldworks_attr_lacquering_en, SldWorksConstants.sldworks_attr_lacquering_de } },
            {"Additional Information", new List<string>{ SldWorksConstants.sldworks_attr_additionalinfo_en, SldWorksConstants.sldworks_attr_additionalinfo_de } },
            {"Based On", new List<string>{ SldWorksConstants.sldworks_attr_basedon } },
            {"DIN ISO 2768 | General Tolerances", new List<string>{ SldWorksConstants.sldworks_attr_generaltolerances } },
            {"ISO 9013 | Separations Methods Precision", new List<string>{ SldWorksConstants.sldworks_attr_thermalcut } },
            {"Semifinished Standard", new List<string>{ SldWorksConstants.sldworks_attr_semifinished } },
            {"Weldment", new List<string>{ SldWorksConstants.sldworks_attr_weldment } },
            {"Dye Penetration Inspection acc. ISO 3452-1", new List<string>{ SldWorksConstants.sldworks_attr_weldment_dyepenetration_en, SldWorksConstants.sldworks_attr_weldment_dyepenetration_de } },
            {"ISO 17637 | Visual Inspection", new List<string>{ SldWorksConstants.sldworks_attr_weldment_visualinspection_en, SldWorksConstants.sldworks_attr_weldment_visualinspection_de } },
            {"DIN EN ISO 5817 | Quality Level for Imperfection", new List<string>{ SldWorksConstants.sldworks_attr_weldment_qualityimperfections } },
            {"Measurements", new List<string>{ } },
            {"Weight", new List<string>{ } },
            {"Welding Assembly", new List<string>{ SldWorksConstants.sldworks_attr_weldment } },
            {"Components", new List<string>{ } },
            {"Change-No.", new List<string>{ SldWorksConstants.sldworks_attr_changenumber } },
            {"Change Short Description", new List<string>{ SldWorksConstants.sldworks_attr_changedescription } },
            {"Occurs", new List<string>{ SldWorksConstants.sldworks_attr_changeoccurs } },
            {"Drafter", new List<string>{ } },
            {"Date", new List<string>{ } },
            {"Material", new List<string>{ } }
        };

    }
}
