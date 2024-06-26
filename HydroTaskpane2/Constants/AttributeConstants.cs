using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroSolidworksLibrary;

namespace HydroTaskpane2.Constants
{
    public class AttributeConstants
    {
        public static Dictionary<string, List<string>> partDict = new Dictionary<string, List<string>>
        {
            {"Descriptions", new List<string>{ "Description", "Additional Description", "CAD Description"} },
            {"Material", new List<string>{ "Material Hydro Variant 1", "Material Hydro Variant 2", "Material Hydro Additional Text", "Material Original", "Material Original Additional Text" } },
            {"Treatments", new List<string>{ "Heat Treatment", "Preconditioning", "Surface Finish"} },
            {"Color", new List<string>{ "Standard Color", "Lacquering"} },
            {"Additional Information", new List<string>{ "Additional Information", "Based On", "DIN ISO 2768 | General Tolerances", "ISO 9013 | Separations Methods Precision", "Semifinished Standard"} },
            {"Welding", new List<string>{ "Weldment", "Dye Penetration Inspection acc. ISO 3452-1", "ISO 17637 | Visual Inspection", "DIN EN ISO 5817 | Quality Level for Imperfection"} },
            {"Dimensions", new List<string>{ "Measurements", "Weight"} }
        };

        public static Dictionary<string, List<string>> assemblyDict = new Dictionary<string, List<string>>
        {
            {"Descriptions", new List<string>{ "Description", "Additional Text", "CAD Description"} },
            {"Treatments", new List<string>{ "Heat Treatment", "Preconditioning", "Surface Finish"} },
            {"Color", new List<string>{ "Standard Color", "Lacquering"} },
            {"Additional Information", new List<string>{ "Additional Information", "Based On", "DIN ISO 2768 | General Tolerances", "ISO 9013 | Separations Methods Precision", "Semifinished Standard"} },
            {"Welding", new List<string>{ "Welding Assembly", "Dye Penetration Inspection acc. ISO 3452-1", "ISO 17637 | Visual Inspection", "DIN EN ISO 5817 | Quality Level for Imperfection"} },
            {"Dimensions", new List<string>{ "Measurements", "Weight", "Components"} }
        };

        public static Dictionary<string, List<string>> drawingDict = new Dictionary<string, List<string>>
        {
            {"Changes", new List<string>{ "Change-No.", "Change Short Description", "Occurs", "Additional Information"} },
            {"Treatments", new List<string>{ "Heat Treatment", "Preconditioning", "Surface Finish"} },
            {"Color", new List<string>{ "Standard Color", "Lacquering"} },
            {"Additional Information", new List<string>{ "Based On", "DIN ISO 2768 | General Tolerances", "ISO 9013 | Separations Methods Precision", "Semifinished Standard"} },
            {"Welding", new List<string>{ "Welding Assembly", "Dye Penetration Inspection acc. ISO 3452-1", "ISO 17637 | Visual Inspection", "DIN EN ISO 5817 | Quality Level for Imperfection"} },
            {"General", new List<string>{ "Drafter", "Date", "Weight", "Material"} }
        };

    }

    public class AttributeValueLists
    {
        public static Dictionary<string, string> partControlPathDict = new Dictionary<string, string>
        {
            {"Description", SldWorksConstants.filename_mix_description},
            {"Additional Description", SldWorksConstants.filename_part_additionaltext},
            {"CAD Description", ""}, // textBox
            {"Material Hydro Variant 1", SldWorksConstants.filename_part_material},
            {"Material Hydro Variant 2", SldWorksConstants.filename_part_material},
            {"Material Hydro Additional Text", ""}, // textBox
            {"Material Original", SldWorksConstants.filename_part_material},
            {"Material Original Additional Text", ""}, // textBox
            {"Heat Treatment", SldWorksConstants.filename_mix_heattreat},
            {"Preconditioning", SldWorksConstants.filename_mix_preconditioning},
            {"Surface Finish", SldWorksConstants.filename_mix_surface},
            {"Standard Color", SldWorksConstants.filename_mix_standardcolor},
            {"Lacquering", SldWorksConstants.filename_mix_lacquering},
            {"Additional Information", SldWorksConstants.filename_mix_additionalinformation},
            {"Based On", ""}, // textBox
            {"DIN ISO 2768 | General Tolerances", SldWorksConstants.filename_part_generaltolerance},
            {"ISO 9013 | Separations Methods Precision", SldWorksConstants.filename_mix_thermalcut},
            {"Semifinished Standard", SldWorksConstants.filename_mix_semifinished},
            {"Weldment", "*"}, // checkBox
            {"Dye Penetration Inspection acc. ISO 3452-1", SldWorksConstants.filename_assembly_dyepentrationinspection},
            {"ISO 17637 | Visual Inspection", SldWorksConstants.filename_mix_visualinspection},
            {"DIN EN ISO 5817 | Quality Level for Imperfection", SldWorksConstants.filename_assembly_qualityimperfections},
            {"Measurements", "-"}, // label
            {"Weight", "-"} // label
        };

        public static Dictionary<string, string> assemblyControlPathDict = new Dictionary<string, string>
        {
            {"Description", SldWorksConstants.filename_mix_description},
            {"Additional Text", SldWorksConstants.filename_assembly_additionaltext},
            {"CAD Description", ""}, // textBox
            {"Heat Treatment", SldWorksConstants.filename_mix_heattreat},
            {"Preconditioning", SldWorksConstants.filename_mix_preconditioning},
            {"Surface Finish", SldWorksConstants.filename_mix_surface},
            {"Standard Color", SldWorksConstants.filename_mix_standardcolor},
            {"Lacquering", SldWorksConstants.filename_mix_lacquering},
            {"Additional Information", SldWorksConstants.filename_mix_additionalinformation},
            {"Based On", ""}, // textBox
            {"DIN ISO 2768 | General Tolerances", SldWorksConstants.filename_assembly_generaltolerance},
            {"ISO 9013 | Separations Methods Precision", SldWorksConstants.filename_mix_thermalcut},
            {"Semifinished Standard", SldWorksConstants.filename_mix_semifinished},
            {"Welding Assembly", "*"}, // checkBox
            {"Dye Penetration Inspection acc. ISO 3452-1", SldWorksConstants.filename_assembly_dyepentrationinspection},
            {"ISO 17637 | Visual Inspection", SldWorksConstants.filename_mix_visualinspection},
            {"DIN EN ISO 5817 | Quality Level for Imperfection", SldWorksConstants.filename_assembly_qualityimperfections},
            {"Measurements", "-"}, // label
            {"Weight", "-"}, // label
            {"Components", "-"} // label
        };

        public static Dictionary<string, string> drawingControlPathDict = new Dictionary<string, string>
        {
            {"Change-No.", ""},
            {"Change Short Description", ""},
            {"Occurs", ""}, // textBox
            {"Additional Information", ""},
            {"Heat Treatment", SldWorksConstants.filename_mix_heattreat},
            {"Preconditioning", SldWorksConstants.filename_mix_preconditioning},
            {"Surface Finish", SldWorksConstants.filename_mix_surface},
            {"Standard Color", SldWorksConstants.filename_mix_standardcolor},
            {"Lacquering", SldWorksConstants.filename_mix_lacquering},
            {"Based On", ""}, // textBox
            {"DIN ISO 2768 | General Tolerances", SldWorksConstants.filename_assembly_generaltolerance},
            {"ISO 9013 | Separations Methods Precision", SldWorksConstants.filename_mix_thermalcut},
            {"Semifinished Standard", SldWorksConstants.filename_mix_semifinished},
            {"Welding Assembly", "*"}, // checkBox
            {"Dye Penetration Inspection acc. ISO 3452-1", SldWorksConstants.filename_assembly_dyepentrationinspection},
            {"ISO 17637 | Visual Inspection", SldWorksConstants.filename_mix_visualinspection},
            {"DIN EN ISO 5817 | Quality Level for Imperfection", SldWorksConstants.filename_assembly_qualityimperfections},
            {"Drafter", "-"}, // label
            {"Date", "-"}, // label
            {"Weight", "-"}, // label
            {"Material", "-"} // label
        };

    }
}
