using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
