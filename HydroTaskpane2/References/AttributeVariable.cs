using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroSolidworksLibrary;

namespace HydroTaskpane2.References
{
    public class AttributeVariable
    {
        public static Dictionary<int, List<string>> controlAttributePairs = new Dictionary<int, List<string>>
        {
            {(int)AttributeClass_e.description, new List<string>{ SldWorksConstants.sldworks_attr_description_en, SldWorksConstants.sldworks_attr_description_de} },
            {(int)AttributeClass_e.adddescription, new List<string>{ SldWorksConstants.sldworks_attr_additionaltext_en, SldWorksConstants.sldworks_attr_additionaltext_de } },
            {(int)AttributeClass_e.addtext, new List<string>{ SldWorksConstants.sldworks_attr_additionaltext_en, SldWorksConstants.sldworks_attr_additionaltext_de } },
            {(int)AttributeClass_e.caddescription, new List<string>{ SldWorksConstants.sldworks_attr_caddescription } },
            {(int)AttributeClass_e.materialhydro1, new List<string>{ SldWorksConstants.sldworks_attr_material, SldWorksConstants.sldworks_attr_material_number, SldWorksConstants.sldworks_attr_material_en, SldWorksConstants.sldworks_attr_material_de } },
            {(int)AttributeClass_e.materialhydro2, new List<string>{ SldWorksConstants.sldworks_attr_materialalt, SldWorksConstants.sldworks_attr_materialalt_number, SldWorksConstants.sldworks_attr_materialalt_en, SldWorksConstants.sldworks_attr_materialalt_de } },
            {(int)AttributeClass_e.materialhydroadd, new List<string>{ SldWorksConstants.sldworks_attr_materialadd } },
            {(int)AttributeClass_e.materialoriginal, new List<string>{ SldWorksConstants.sldworks_attr_materialorig, SldWorksConstants.sldworks_attr_materialorig_number, SldWorksConstants.sldworks_attr_materialorig_en, SldWorksConstants.sldworks_attr_materialorig_de } },
            {(int)AttributeClass_e.materialoriginaladd, new List<string>{ SldWorksConstants.sldworks_attr_materialorigadd } },
            {(int)AttributeClass_e.heattreatment, new List<string>{ SldWorksConstants.sldworks_attr_heattreat_en, SldWorksConstants.sldworks_attr_heattreat_de } },
            {(int)AttributeClass_e.preconditioning, new List<string>{ SldWorksConstants.sldworks_attr_preconditioning_en, SldWorksConstants.sldworks_attr_preconditioning_de } },
            {(int)AttributeClass_e.surfacefinish, new List<string>{ SldWorksConstants.sldworks_attr_surface_en, SldWorksConstants.sldworks_attr_surface_de } },
            {(int)AttributeClass_e.standardcolor, new List<string>{ SldWorksConstants.sldworks_attr_standardcolor, SldWorksConstants.sldworks_attr_standardcolor_en, SldWorksConstants.sldworks_attr_standardcolor_de } },
            {(int)AttributeClass_e.lacquering, new List<string>{ SldWorksConstants.sldworks_attr_lacquering, SldWorksConstants.sldworks_attr_lacquering_en, SldWorksConstants.sldworks_attr_lacquering_de } },
            {(int)AttributeClass_e.addinformation, new List<string>{ SldWorksConstants.sldworks_attr_additionalinfo_en, SldWorksConstants.sldworks_attr_additionalinfo_de } },
            {(int)AttributeClass_e.basedon, new List<string>{ SldWorksConstants.sldworks_attr_basedon } },
            {(int)AttributeClass_e.generaltolerances, new List<string>{ SldWorksConstants.sldworks_attr_generaltolerances } },
            {(int)AttributeClass_e.sepmethodsprecision, new List<string>{ SldWorksConstants.sldworks_attr_thermalcut } },
            {(int)AttributeClass_e.semifinishedstandard, new List<string>{ SldWorksConstants.sldworks_attr_semifinished } },
            {(int)AttributeClass_e.weldment, new List<string>{ SldWorksConstants.sldworks_attr_weldment } },
            {(int)AttributeClass_e.dyepenetrationinsp, new List<string>{ SldWorksConstants.sldworks_attr_weldment_dyepenetration_en, SldWorksConstants.sldworks_attr_weldment_dyepenetration_de } },
            {(int)AttributeClass_e.visualinsp, new List<string>{ SldWorksConstants.sldworks_attr_weldment_visualinspection_en, SldWorksConstants.sldworks_attr_weldment_visualinspection_de } },
            {(int)AttributeClass_e.qualitylevelimp, new List<string>{ SldWorksConstants.sldworks_attr_weldment_qualityimperfections } },
            {(int)AttributeClass_e.measurements, new List<string>{ } },
            {(int)AttributeClass_e.weight, new List<string>{ } },
            {(int)AttributeClass_e.weldassembly, new List<string>{ SldWorksConstants.sldworks_attr_weldment } },
            {(int)AttributeClass_e.components, new List<string>{ } },
            {(int)AttributeClass_e.changeno, new List<string>{ SldWorksConstants.sldworks_attr_changenumber } },
            {(int)AttributeClass_e.changeshortdescription, new List<string>{ SldWorksConstants.sldworks_attr_changedescription } },
            {(int)AttributeClass_e.occurs, new List<string>{ SldWorksConstants.sldworks_attr_changeoccurs } },
            {(int)AttributeClass_e.drafter, new List<string>{ } },
            {(int)AttributeClass_e.date, new List<string>{ } },
            {(int)AttributeClass_e.material, new List<string>{ } },
            // neue felder
            {(int)AttributeClass_e.yieldstrength, new List<string>{SldWorksConstants.sldworks_attr_yieldstrength}},
            {(int)AttributeClass_e.tensilestrength, new List<string>{SldWorksConstants.sldworks_attr_tensilestrength}},
            {(int)AttributeClass_e.elongation, new List<string>{ SldWorksConstants.sldworks_attr_elongation}},
            {(int)AttributeClass_e.specialselection, new List<string>{ SldWorksConstants.sldworks_attr_specialselection_number, SldWorksConstants.sldworks_attr_specialselection_en, SldWorksConstants.sldworks_attr_specialselection_de} },
            // Angebotsblaetter
            {(int)AttributeClass_e.revisionstatus, new List<string>{SldWorksConstants.sldworks_attr_revisionstatus}},
            {(int)AttributeClass_e.capacity_t, new List<string>{SldWorksConstants.sldworks_attr_capacity_t}},
            {(int)AttributeClass_e.capacity_ton, new List<string>{SldWorksConstants.sldworks_attr_capacity_ton}},
            {(int)AttributeClass_e.capacity_lbs, new List<string>{SldWorksConstants.sldworks_attr_capacity_lbs}},
            {(int)AttributeClass_e.capacity_kg, new List<string>{SldWorksConstants.sldworks_attr_capacity_kg}},
            {(int)AttributeClass_e.capacity_KN, new List<string>{SldWorksConstants.sldworks_attr_capacity_kn}},
            {(int)AttributeClass_e.ac, new List<string>{SldWorksConstants.sldworks_attr_ac}},
            {(int)AttributeClass_e.typeofengine, new List<string>{SldWorksConstants.sldworks_attr_typeofengine}},
            {(int)AttributeClass_e.closedheight_mm, new List<string>{SldWorksConstants.sldworks_attr_closedheight_mm}},
            {(int)AttributeClass_e.closedheight_inch, new List<string>{SldWorksConstants.sldworks_attr_closedheight_inch}},
            {(int)AttributeClass_e.closedheightA_mm, new List<string>{SldWorksConstants.sldworks_attr_closedheightA_mm}},
            {(int)AttributeClass_e.closedheightA_inch, new List<string>{SldWorksConstants.sldworks_attr_closedheightA_inch}},
            {(int)AttributeClass_e.liftstrokeB_mm, new List<string>{SldWorksConstants.sldworks_attr_liftstrokeB_mm}},
            {(int)AttributeClass_e.liftstrokeB_inch, new List<string>{SldWorksConstants.sldworks_attr_liftstrokeB_inch}},
            {(int)AttributeClass_e.closedheightC_mm, new List<string>{SldWorksConstants.sldworks_attr_closedheightC_mm}},
            {(int)AttributeClass_e.closedheightC_inch, new List<string>{SldWorksConstants.sldworks_attr_closedheightC_inch}},
            {(int)AttributeClass_e.liftstrokeD_mm, new List<string>{SldWorksConstants.sldworks_attr_liftstrokeD_mm}},
            {(int)AttributeClass_e.liftstrokeD_inch, new List<string>{SldWorksConstants.sldworks_attr_liftstrokeD_inch}},
            {(int)AttributeClass_e.Model, new List<string>{SldWorksConstants.sldworks_attr_model}},
            {(int)AttributeClass_e.hydrauliclift_mm, new List<string>{SldWorksConstants.sldworks_attr_hydrauliclift_mm}},
            {(int)AttributeClass_e.hydrauliclift_inch, new List<string>{SldWorksConstants.sldworks_attr_hydrauliclift_inch}},
            {(int)AttributeClass_e.screwextension_mm, new List<string>{SldWorksConstants.sldworks_attr_screwextension_mm}},
            {(int)AttributeClass_e.screwextension_inch, new List<string>{SldWorksConstants.sldworks_attr_screwextension_inch}},
            {(int)AttributeClass_e.extendedheight_mm, new List<string>{SldWorksConstants.sldworks_attr_extendedheight_mm}},
            {(int)AttributeClass_e.extendedheight_inch, new List<string>{SldWorksConstants.sldworks_attr_extendedheight_inch}},
            {(int)AttributeClass_e.usableheight_mm, new List<string>{SldWorksConstants.sldworks_attr_extendedheight_mm}},
            {(int)AttributeClass_e.usableheight_inch, new List<string>{SldWorksConstants.sldworks_attr_extendedheight_inch}}
        };

    }
}
