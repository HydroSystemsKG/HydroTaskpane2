using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroTaskpane2.Decorators.Reference;
using HydroSolidworksLibrary;
using SwConst;

namespace HydroTaskpane2.References
{
    public class FieldList
    {
        // string name, int controlType, string standardValue, int height, string filePath = null
        public static List<string> keys = new List<string>
        {
            "name", "controlType", "standardValue", "height", "filePath", "tab", "decorator"
        };

        public static int height = 30;
        public static int controlHeight = 25;

        public static List<object[]> controlArrays = new List<object[]>
        {
            // part
            // description
            new object[] { "part_description_separator", (int)ControlTypes.separator, "Descriptions | Beschreibungen", height, null, PageNames.standard, null},
            new object[] { "part_description_label", (int)ControlTypes.label, "Description | Beschreibung", height, null, PageNames.standard, null},
            new object[] { "part_description_dropdown", (int)ControlTypes.comboBox, "Please select  |  Bitte auswählen", controlHeight, SldWorksConstants.filename_mix_description,  PageNames.standard, (int)Decorator_e.description_change},
            new object[] { "part_additionaltext_label", (int)ControlTypes.label, "Additional Description | Zusatztext", height, null,  PageNames.standard, null},
            new object[] { "part_additionaltext_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_part_additionaltext, PageNames.standard, (int)Decorator_e.description_change},
            new object[] { "part_caddescription_label", (int)ControlTypes.label, "CAD Description | CAD Beschreibung", height, null, PageNames.standard, null},
            new object[] { "part_caddescription_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "part_basedon_label", (int)ControlTypes.label, "Based On | Basierend Auf", height, null, PageNames.standard, null},
            new object[] { "part_basedon_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "part_description_separatorend", (int)ControlTypes.separator, "", height, null, PageNames.standard, null},
            //new object[] { "part_description_spacer", (int)ControlTypes.label, "", height, null, PageNames.standard, null}, // SPACER

            // material original

            new object[] { "part_materialorig_separator", (int)ControlTypes.separator, "Original Material | Originalwerkstoff", height, null, PageNames.standard, null},
            new object[] { "part_materialorig_label", (int)ControlTypes.label, "Material Original | Original Werkstoff", height, null, PageNames.standard, null},
            new object[] { "part_materialorig_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_part_material, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "part_materialorigadd_label", (int)ControlTypes.label, "Material Original Additional Text | Original Werkstoff Zusatzmerkmal", height, null, PageNames.standard, null},
            new object[] { "part_materialorigadd_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "part_materialorig_separatorend", (int)ControlTypes.separator, "", height, null, PageNames.standard, null},
            //new object[] { "part_materialorig_spacer", (int)ControlTypes.label, "", height, null, PageNames.standard, null}, // SPACER

            // material + strength

            new object[] { "part_material_separator", (int)ControlTypes.separator, "Material | Werkstoff", height, null, PageNames.standard, null},
            new object[] { "part_material_label", (int)ControlTypes.label, "Material Hydro Variant 1 | Werkstoff Hydro Variante 1", height, null, PageNames.standard, null},
            new object[] { "part_material_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_part_material, PageNames.standard, (int)Decorator_e.material}, // modified
            new object[] { "part_yieldstrength_label", (int)ControlTypes.label, "Yield Strength | Streckgrenze [MPa]", height, null, PageNames.standard, null},
            new object[] { "part_yieldstrength_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.strength},
            new object[] { "part_tensilestrength_label", (int)ControlTypes.label, "Tensile Strength | Zugfestigkeit [MPa]", height, null, PageNames.standard, null},
            new object[] { "part_tensilestrength_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.strength},
            new object[] { "part_elongationatbreak_label", (int)ControlTypes.label, "Elongation at break | Bruchdehnung [%]", height, null, PageNames.standard, null},
            new object[] { "part_elongationatbreak_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.strength},
            new object[] { "part_materialalt_label", (int)ControlTypes.label, "Material Hydro Variant 2 | Werkstoff Hydro Variante 2", height, null, PageNames.standard, null},
            new object[] { "part_materialalt_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_part_material, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "part_materialadd_label", (int)ControlTypes.label, "Material Hydro Additional Text | Werkstoff Hydro Zusatzmerkmal", height, null, PageNames.standard, null},
            new object[] { "part_materialadd_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "part_specialselection_label", (int)ControlTypes.label, "Special Selection | Sonderauswahl", height, null, PageNames.standard, null}, // sonderauswahl
            new object[] { "part_specialselection_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_mix_specialselection, PageNames.standard, (int)Decorator_e.standard}, // sonderauswahl
            new object[] { "part_material_separatorend", (int)ControlTypes.separator, "", height, null, PageNames.standard, null},
            //new object[] { "part_material_spacer", (int)ControlTypes.label, "", height, null, PageNames.standard, null}, // SPACER

            // treatments

            new object[] { "part_treatments_separator", (int)ControlTypes.separator, "Treatments | Behandlungen", height, null, PageNames.standard, null},
            new object[] { "part_heattreat_label", (int)ControlTypes.label, "Heat Treatment | Wärmebehandlung", height, null, PageNames.standard, null},
            new object[] { "part_heattreat_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_mix_heattreat, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "part_preconditioning_label", (int)ControlTypes.label, "Preconditioning | Vorbehandlung", height, null, PageNames.standard, null},
            new object[] { "part_preconditioning_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_mix_preconditioning, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "part_surface_label", (int)ControlTypes.label, "Surface Finish | Oberflächenbehandlung", height, null, PageNames.standard, null},
            new object[] { "part_surface_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_mix_surface, PageNames.standard, (int)Decorator_e.color}, //COLOR HANDLER
            new object[] { "part_color_label", (int)ControlTypes.label, "Standard Color | Standardfarbe", height, null, PageNames.standard, null},
            new object[] { "part_color_dropdown", (int)ControlTypes.comboBox, "not necessary | not necessary | nicht erforderlich", controlHeight, SldWorksConstants.filename_mix_standardcolor, PageNames.standard, (int)Decorator_e.cv},
            new object[] { "part_varnishing_label", (int)ControlTypes.label, "Lacquering | Lackierung", height, null, PageNames.standard, null},
            new object[] { "part_varnishing_dropdown", (int)ControlTypes.comboBox, "not necessary | not necessary | nicht erforderlich", controlHeight, SldWorksConstants.filename_mix_standardcolor, PageNames.standard, (int)Decorator_e.cv},
            new object[] { "part_additionalinfo_label", (int)ControlTypes.label, "Additional Information | Zusatzinformationen", height, null, PageNames.standard, null},
            new object[] { "part_additionalinfo_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_mix_additionalinformation, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "part_treatments_separatorend", (int)ControlTypes.separator, "", height, null, PageNames.standard, null},
            //new object[] { "part_treatments_spacer", (int)ControlTypes.label, "", height, null, PageNames.standard, null}, // SPACER

            // norms

            new object[] { "part_norms_separator", (int)ControlTypes.separator, "Norms | Normen", height, null, PageNames.standard, null},
            new object[] { "part_generaltolerance_label", (int)ControlTypes.label, "DIN ISO 2768 | General Tolerances | Allgemeintoleranzen", height, null, PageNames.standard, null},
            new object[] { "part_generaltolerance_dropdown", (int)ControlTypes.comboBox, "DIN ISO 2768-mK", controlHeight, SldWorksConstants.filename_part_generaltolerance, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "part_thermalcut_label", (int)ControlTypes.label, "ISO 9013 | Separation Methods Precision | Trennverfahren Toleranz", height, null, PageNames.standard, null},
            new object[] { "part_thermalcut_dropdown", (int)ControlTypes.comboBox, "n/a", controlHeight, SldWorksConstants.filename_mix_thermalcut, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "part_semifinished_label" , (int)ControlTypes.label, "Semifinished Standard | Halbzeug Norm", height, null, PageNames.standard, null},
            new object[] { "part_semifinished_dropdown", (int)ControlTypes.comboBox, "n/a", controlHeight, SldWorksConstants.filename_mix_semifinished, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "part_norms_separatorend", (int)ControlTypes.separator, "", height, null, PageNames.standard, null},
            //new object[] { "part_norms_spacer", (int)ControlTypes.label, "", height, null, PageNames.standard, null}, // SPACER
            
            // welding

            new object[] { "part_welding_separator", (int)ControlTypes.separator, "Welding | Schweißteil", height, null, PageNames.standard, null},
            new object[] { "part_welding_checkbox", (int)ControlTypes.checkBox, "Weldment | Schweißteil", controlHeight, null, PageNames.standard, (int)Decorator_e.weldment}, // part welding checkbox
            new object[] { "part_dyepenetration_label", (int)ControlTypes.label, "Dye Penetration Inspection acc. ISO 3452-1 | Farbeindringprüfung n. ISO 3452-1", height, null, PageNames.standard, null},
            new object[] { "part_dyepenetration_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_assembly_dyepentrationinspection, PageNames.standard, (int)Decorator_e.weld_init},
            new object[] { "part_visualinspection_label", (int)ControlTypes.label, "ISO 17637 | Visual Inspection | Sichtprüfung", height, null, PageNames.standard, null},
            new object[] { "part_visualinspection_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_mix_visualinspection, PageNames.standard, (int)Decorator_e.weld_init},
            new object[] { "part_qualityimperfections_label", (int)ControlTypes.label, "DIN EN ISO 5817 | Quality Level for Imperfection | Qualitätsanforderung für Unregelmäßigkeiten", height, null, PageNames.standard, null},
            new object[] { "part_qualityimperfections_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_assembly_qualityimperfections, PageNames.standard, (int)Decorator_e.weld_init},
            new object[] { "part_welding_separatorend", (int)ControlTypes.separator, "", height, null, PageNames.standard, null},
            //new object[] { "part_welding_spacer", (int)ControlTypes.label, "", height, null, PageNames.standard, null}, // SPACER
            
            // inch/metric mode + dimensions + weight

            new object[] { "part_dimensionsweight_separator", (int)ControlTypes.separator, "Dimensions & Weight | Dimensionen & Gewicht", height, null, PageNames.standard, null},
            new object[] { "part_metricmode_checkbox", (int)ControlTypes.checkBox, "Metric", controlHeight, null, PageNames.standard, (int)Decorator_e.unit_mode},
            new object[] { "part_imperialmode_checkbox", (int)ControlTypes.checkBox, "Imperial", controlHeight, null, PageNames.standard, (int)Decorator_e.unit_mode},
            new object[] { "part_dimensions_stack", (int)ControlTypes.stack, "Dimensions | Abmessungen [mm]", controlHeight, null, PageNames.standard, (int)Decorator_e.dimensions},
            new object[] { "part_imperialdimensions_stack", (int)ControlTypes.stack, "Dimensions | Abmessungen [inch]", controlHeight, null, PageNames.standard, (int)Decorator_e.dimensions},
            new object[] { "part_weight_stack", (int)ControlTypes.stack, "Weight | Gewicht [kg]", height, null, PageNames.standard, (int)Decorator_e.weight},
            new object[] { "part_imperialweight_stack", (int)ControlTypes.stack, "Weight | Gewicht [lbs]", controlHeight, null, PageNames.standard, (int)Decorator_e.weight},
            new object[] { "part_dimensionsweight_separatorend", (int)ControlTypes.separator, "", height, null, PageNames.standard, null},

            // ################

            // assembly

            // description

            new object[] { "assembly_description_separator", (int)ControlTypes.separator, "Descriptions | Beschreibungen", height, null, PageNames.standard, null},
            new object[] { "assembly_description_label", (int)ControlTypes.label, "Description | Beschreibung", height, null, PageNames.standard, null},
            new object[] { "assembly_description_dropdown", (int)ControlTypes.comboBox, "Please select | Bitte auswählen", controlHeight, SldWorksConstants.filename_mix_description, PageNames.standard, (int)Decorator_e.description_change},
            new object[] { "assembly_additionaltext_label", (int)ControlTypes.label, "Additional Text | Zusatztext", height, null, PageNames.standard, null},
            new object[] { "assembly_additionaltext_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_assembly_additionaltext, PageNames.standard, (int)Decorator_e.description_change},
            new object[] { "assembly_caddescription_label", (int)ControlTypes.label, "CAD Description | CAD Beschreibung", height, null, PageNames.standard, null},
            new object[] { "assembly_caddescription_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "assembly_basedon_label", (int)ControlTypes.label, "Based On | Basierend Auf", height, null, PageNames.standard, null},
            new object[] { "assembly_basedon_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "assembly_description_separatorend", (int)ControlTypes.separator, "", height, null, PageNames.standard, null},

            // treatments

            //new object[] { "assembly_specialselection_label", (int)ControlTypes.label, "Special Selection | Sonderauswahl", height, null, PageNames.standard, null}, // sonderauswahl
            //new object[] { "assembly_specialselection_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_mix_specialselection, PageNames.standard, (int)Decorator_e.standard}, // sonderauswahl
            new object[] { "assembly_treatments_separator", (int)ControlTypes.separator, "Treatments | Behandlungen", height, null, PageNames.standard, null},
            new object[] { "assembly_heattreat_label", (int)ControlTypes.label, "Heat Treatment | Wärmebehandlung", height, null, PageNames.standard, null},
            new object[] { "assembly_heattreat_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_mix_heattreat, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "assembly_preconditioning_label", (int)ControlTypes.label, "Preconditioning | Vorbehandlung", height, null, PageNames.standard, null},
            new object[] { "assembly_preconditioning_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_mix_preconditioning, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "assembly_surface_label", (int)ControlTypes.label, "Surface Finish | Oberflächenbehandlung", height, null, PageNames.standard, null},
            new object[] { "assembly_surface_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_mix_surface, PageNames.standard, (int)Decorator_e.color},
            new object[] { "assembly_color_label", (int)ControlTypes.label, "Standard Color | Standardfarbe", height, null, PageNames.standard, null},
            new object[] { "assembly_color_dropdown", (int)ControlTypes.comboBox, "not necessary | not necessary | nicht erforderlich", controlHeight, SldWorksConstants.filename_mix_standardcolor, PageNames.standard, (int)Decorator_e.cv},
            new object[] { "assembly_varnishing_label", (int)ControlTypes.label, "Lacquering | Lackierung", height, null, PageNames.standard, null},
            new object[] { "assembly_varnishing_dropdown", (int)ControlTypes.comboBox, "not necessary | not necessary | nicht erforderlich", controlHeight, SldWorksConstants.filename_mix_standardcolor, PageNames.standard, (int)Decorator_e.cv},
            new object[] { "assembly_additionalinfo_label", (int)ControlTypes.label, "Additional Information | Zusatzinformationen", height, null, PageNames.standard, null},
            new object[] { "assembly_additionalinfo_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_mix_additionalinformation, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "assembly_treatments_separatorend", (int)ControlTypes.separator, "", height, null, PageNames.standard, null},

            // norms

            new object[] { "assembly_norms_separator", (int)ControlTypes.separator, "Norms | Normen", height, null, PageNames.standard, null},
            new object[] { "assembly_generaltolerance_label", (int)ControlTypes.label, "DIN ISO 2768 | General Tolerances | Allgemeintoleranzen", height, null, PageNames.standard, null},
            new object[] { "assembly_generaltolerance_dropdown", (int)ControlTypes.comboBox, "DIN ISO 2768-mK", controlHeight, SldWorksConstants.filename_assembly_generaltolerance, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "assembly_thermalcut_label", (int)ControlTypes.label, "ISO 9013 | Separation Methods Precision | Trennverfahren Toleranz", height, null, PageNames.standard, null},
            new object[] { "assembly_thermalcut_dropdown", (int)ControlTypes.comboBox, "n/a", controlHeight, SldWorksConstants.filename_mix_thermalcut, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "assembly_semifinished_label", (int)ControlTypes.label, "Semifinished Standard | Halbzeug Norm", height, null, PageNames.standard, null},
            new object[] { "assembly_semifinished_dropdown", (int)ControlTypes.comboBox, "n/a", controlHeight, SldWorksConstants.filename_mix_semifinished, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "assembly_norms_separatorend", (int)ControlTypes.separator, "", height, null, PageNames.standard, null},

            // welding

            new object[] { "assembly_welding_separator", (int)ControlTypes.separator, "Welding | Schweißbaugruppe", height, null, PageNames.standard, null},
            new object[] { "assembly_welding_checkbox", (int)ControlTypes.checkBox, "Welding Assembly | Schweißbaugruppe", controlHeight, null, PageNames.standard, (int)Decorator_e.weldment}, // assembly welding checkbox
            new object[] { "assembly_dyepenetration_label", (int)ControlTypes.label, "Dye Penentration Inspection acc. ISO 3452-1 | Farbeindringprüfung n. ISO 3452-1", height, null, PageNames.standard, null},
            new object[] { "assembly_dyepenetration_dropdown", (int)ControlTypes.comboBox, "",  controlHeight, SldWorksConstants.filename_assembly_dyepentrationinspection, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "assembly_visualinspection_label", (int)ControlTypes.label, "ISO 17637 | Visual Inspection | Sichtprüfung", height, null, PageNames.standard, null},
            new object[] { "assembly_visualinspection_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_mix_visualinspection, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "assembly_qualityimperfections_label", (int)ControlTypes.label, "DIN EN ISO 5817 | Quality Level for Imperfection | Qualitätsanforderung für Uneregelmäßigkeiten", height, null, PageNames.standard, null},
            new object[] { "assembly_qualityimperfections_dropdown", (int)ControlTypes.comboBox, "", controlHeight, SldWorksConstants.filename_assembly_qualityimperfections, PageNames.standard, (int)Decorator_e.standard},
            new object[] { "assembly_welding_separatorend", (int)ControlTypes.separator, "", height, null, PageNames.standard, null},
            
            // ################

            // drawing

            // header 

            new object[] { "drawing_header_separator", (int)ControlTypes.separator, "Header | Zeichnungskopf", height, null, PageNames.standard, null},
            new object[] { "drawing_changeno_label", (int)ControlTypes.label, "Change-No. | Änderungsnummer", height, null, PageNames.standard, null},
            new object[] { "drawing_changeno_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard},
            new object[] { "drawing_changedescription_label", (int)ControlTypes.label, "Change Short Description | Änderungskurztext", height, null, PageNames.standard, null},
            new object[] { "drawing_changedescription_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard},
            new object[] { "drawing_occurs_label", (int)ControlTypes.label, "Occurs | Vorkommen", height, null, PageNames.standard, null},
            new object[] { "drawing_occurs_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard},
            new object[] { "drawing_additionalinfo_label", (int)ControlTypes.label, "Additional Information | Zusatzinformationen", height, null, PageNames.standard, null},
            new object[] { "drawing_additionalinfo_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard},
            new object[] { "drawing_basedon_label", (int)ControlTypes.label, "Based On | Basierend Auf", height, null, PageNames.standard, null},
            new object[] { "drawing_basedon_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard},
            new object[] { "drawing_header_separatorend", (int)ControlTypes.separator, "", height, null, PageNames.standard, null},

            //new object[] { "drawing_specialselection_label", (int)ControlTypes.label, "Special Selection | Sonderauswahl", height, null, PageNames.standard, null}, // sonderauswahl
            //new object[] { "drawing_specialselection_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard}, // sonderauswahl

            // treatments

            new object[] { "drawing_treatments_separator", (int)ControlTypes.separator, "Treatments | Behandlungen", height, null, PageNames.standard, null},
            new object[] { "drawing_heattreat_label", (int)ControlTypes.label, "Heat Treatment | Wärmebehandlung", height, null, PageNames.standard, null},
            new object[] { "drawing_heattreat_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard},
            new object[] { "drawing_preconditioning_label", (int)ControlTypes.label, "Preconditioning | Vorbehandlung", height, null, PageNames.standard, null},
            new object[] { "drawing_preconditioning_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard},
            new object[] { "drawing_surface_label", (int)ControlTypes.label, "Surface Finish | Oberflächenbehandlung", height, null, PageNames.standard, null},
            new object[] { "drawing_surface_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard},
            new object[] { "drawing_color_label", (int)ControlTypes.label, "Standard Color | Standardfarbe", height, null, PageNames.standard, null},
            new object[] { "drawing_color_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard},
            new object[] { "drawing_varnishing_label", (int)ControlTypes.label, "Lacquering | Lackierung", height, null, PageNames.standard, null},
            new object[] { "drawing_varnishing_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard},
            new object[] { "drawing_treatments_separatorend", (int)ControlTypes.separator, "", height, null, PageNames.standard, null},

            // norms

            new object[] { "drawing_norms_separator", (int)ControlTypes.separator, "Norms | Normen", height, null, PageNames.standard, null},
            new object[] { "drawing_generaltolerance_label", (int)ControlTypes.label, "DIN ISO 2768 | General Tolerances | Allgemeintoleranzen", height, null, PageNames.standard, null},
            new object[] { "drawing_generaltolerance_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard},
            new object[] { "drawing_thermalcut_label", (int)ControlTypes.label, "ISO 9013 | Separation Methods Precision | Trennverfahren Toleranz", height, null, PageNames.standard, null},
            new object[] { "drawing_thermalcut_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard},
            new object[] { "drawing_semifinished_label", (int)ControlTypes.label, "Semifinished Standard | Halbzeug Norm", height, null, PageNames.standard, null},
            new object[] { "drawing_semifinished_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard},
            new object[] { "drawing_norms_separatorend", (int)ControlTypes.separator, "", height, null, PageNames.standard, null},

            // welding

            new object[] { "drawing_welding_separator", (int)ControlTypes.separator, "Welding | Schweißbaugruppe", height, null, PageNames.standard, null},
            new object[] { "drawing_welding_checkbox", (int)ControlTypes.checkBox, "Welding Assembly | Schweißbaugruppe", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard}, // assembly welding checkbox
            new object[] { "drawing_dyepenetration_label", (int)ControlTypes.label, "ISO 3452-1 | Dye Penetration Inspection | Farbeindringprüfung", height, null, PageNames.standard, null},
            new object[] { "drawing_dyepenetration_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard},
            new object[] { "drawing_visualinspection_label", (int)ControlTypes.label, "ISO 17637 | Visual Inspection | Sichtprüfung", height, null, PageNames.standard, null},
            new object[] { "drawing_visualinspection_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard, (int)Decorator_e.drawing_standard},
            new object[] { "drawing_qualityimperfections_label", (int)ControlTypes.label, "DIN EN ISO 5817 | Quality Level for Imperfection | Qualitätsanforderung für Unregelmäßigkeiten", height, null, PageNames.standard, null},
            new object[] { "drawing_qualityimperfections_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.standard , (int)Decorator_e.drawing_standard},
            new object[] { "drawing_welding_separatorend", (int)ControlTypes.separator, "", height, null, PageNames.standard, null},

            // Additions (must be controlled throughout the whole project)

            // drawing - Angebotsblatt_A4_Cobra

            new object[] { "drawing_angCobra_mode_metric_checkbox", (int)ControlTypes.checkBox, "Metric", controlHeight, null, PageNames.angCobra, (int)Decorator_e.mode_switch}, // ADD NEW DECORATOR!
            new object[] { "drawing_angCobra_mode_imperial_checkbox", (int)ControlTypes.checkBox, "Imperial", controlHeight, null, PageNames.angCobra, (int)Decorator_e.mode_switch}, // ADD NEW DECORATOR!
            new object[] { "drawing_angCobra_mode_separator", (int)ControlTypes.separator, "", height, null, PageNames.angCobra, null},

            new object[] { "drawing_angCobra_Model_label", (int)ControlTypes.label, "Model | Modell | Reference", height, null, PageNames.angCobra, null},
            new object[] { "drawing_angCobra_Model_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angCobra, (int)Decorator_e.standard},
            new object[] { "drawing_angCobra_revision_label", (int)ControlTypes.label, "Revision Status | Revision Stand", height, null, PageNames.angCobra, null},
            new object[] { "drawing_angCobra_revision_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angCobra, (int)Decorator_e.standard},
            new object[] { "drawing_angCobra_capacity_t_label", (int)ControlTypes.label, "Capacity | Hubkraft | Carge Nom. [t]", height, null, PageNames.angCobra, null},
            new object[] { "drawing_angCobra_capacity_t_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angCobra, (int)Decorator_e.metric},
            new object[] { "drawing_angCobra_capacity_lbs_label", (int)ControlTypes.label, "Capacity | Hubkraft | Carge Nom. [lbs]", height, null, PageNames.angCobra, null},
            new object[] { "drawing_angCobra_capacity_lbs_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angCobra, (int)Decorator_e.imperial},
            new object[] { "drawing_angCobra_closedheightA_mm_label", (int)ControlTypes.label, "Closed Height A | Bauhöhe min. A | Hauter Fermee A [mm]", height, null, PageNames.angCobra, null},
            new object[] { "drawing_angCobra_closedheightA_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angCobra, (int)Decorator_e.metric},
            new object[] { "drawing_angCobra_closedheightA_inch_label", (int)ControlTypes.label, "Closed Height A | Bauhöhe min. A | Hauter Fermee A [inch]", height, null, PageNames.angCobra, null},
            new object[] { "drawing_angCobra_closedheightA_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angCobra, (int)Decorator_e.imperial},
            new object[] { "drawing_angCobra_liftstrokeB_mm_label", (int)ControlTypes.label, "Lift Stroke B | Hub B | Course B [mm]", height, null, PageNames.angCobra, null},
            new object[] { "drawing_angCobra_liftstrokeB_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angCobra, (int)Decorator_e.metric},
            new object[] { "drawing_angCobra_liftstrokeB_inch_label", (int)ControlTypes.label, "Lift Stroke B | Hub B | Course B [inch]", height, null, PageNames.angCobra, null},
            new object[] { "drawing_angCobra_liftstrokeB_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angCobra, (int)Decorator_e.imperial},
            new object[] { "drawing_angCobra_closedheightC_mm_label", (int)ControlTypes.label, "Closed Height C | Bauhöhe Min. C | Hauteur Fermee C [mm]", height, null, PageNames.angCobra, null},
            new object[] { "drawing_angCobra_closedheightC_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angCobra, (int)Decorator_e.metric},
            new object[] { "drawing_angCobra_closedheightC_inch_label", (int)ControlTypes.label, "Closed Height C | Bauhöhe Min. C | Hauteur Fermee C [inch]", height, null, PageNames.angCobra, null},
            new object[] { "drawing_angCobra_closedheightC_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angCobra, (int)Decorator_e.imperial},
            new object[] { "drawing_angCobra_liftstrokeD_mm_label", (int)ControlTypes.label, "Lift Stroke D | Hub D | Course D [mm]", height, null, PageNames.angCobra, null},
            new object[] { "drawing_angCobra_liftstrokeD_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angCobra, (int)Decorator_e.metric},
            new object[] { "drawing_angCobra_liftstrokeD_inch_label", (int)ControlTypes.label, "Lift Stroke D | Hub D | Course D [inch]", height, null, PageNames.angCobra, null},
            new object[] { "drawing_angCobra_liftstrokeD_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angCobra, (int)Decorator_e.imperial},

            // drawing - Angebotsblatt_A4_D-Heber
            
            new object[] { "drawing_angDHeber_mode_metric_checkbox", (int)ControlTypes.checkBox, "Metric", controlHeight, null, PageNames.angDHeber, (int)Decorator_e.mode_switch}, // ADD NEW DECORATOR!
            new object[] { "drawing_angDHeber_mode_imperial_checkbox", (int)ControlTypes.checkBox, "Imperial", controlHeight, null, PageNames.angDHeber , (int)Decorator_e.mode_switch}, // ADD NEW DECORATOR!
            new object[] { "drawing_angDHeber_mode_separator", (int)ControlTypes.separator, "", height, null, PageNames.angDHeber, null},

            new object[] { "drawing_angDHeber_model_label", (int)ControlTypes.label, "Model | Modell | Reference", height, null, PageNames.angDHeber, null},
            new object[] { "drawing_angDHeber_model_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeber, (int)Decorator_e.standard},
            new object[] { "drawing_angDHeber_capacity_t_label", (int)ControlTypes.label, "Capacity | Hubkraft | Charge Nom. [t]", height, null, PageNames.angDHeber, null},
            new object[] { "drawing_angDHeber_capacity_t_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeber, (int)Decorator_e.metric},
            new object[] { "drawing_angDHeber_capacity_ton_label", (int)ControlTypes.label, "Capacity | Hubkraft | Charge Nom. [ton]", height, null, PageNames.angDHeber, null},
            new object[] { "drawing_angDHeber_capacity_ton_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeber, (int)Decorator_e.imperial},
            new object[] { "drawing_angDHeber_closedheight_mm_label", (int)ControlTypes.label, "Closed Height | Bauhöhe Min. | Hauteur Fermee [mm]", height, null, PageNames.angDHeber, null},
            new object[] { "drawing_angDHeber_closedheight_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeber, (int)Decorator_e.metric},
            new object[] { "drawing_angDHeber_closedheight_inch_label", (int)ControlTypes.label, "Closed Height | Bauhöhe Min. | Hauteur Fermee [inch]", height, null, PageNames.angDHeber, null},
            new object[] { "drawing_angDHeber_closedheight_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeber, (int)Decorator_e.imperial},
            new object[] { "drawing_angDHeber_hydrauliclift_mm_label", (int)ControlTypes.label, "Hydraulic Lift | Hydraulische Hub | Course Hydraulique [mm]", height, null, PageNames.angDHeber, null},
            new object[] { "drawing_angDHeber_hydrauliclift_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeber, (int)Decorator_e.metric},
            new object[] { "drawing_angDHeber_hydrauliclift_inch_label", (int)ControlTypes.label, "Hydraulic Lift | Hydraulische Hub | Course Hydraulique [inch]", height, null, PageNames.angDHeber, null},
            new object[] { "drawing_angDHeber_hydrauliclift_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeber, (int)Decorator_e.imperial},
            new object[] { "drawing_angDHeber_screwextension_mm_label", (int)ControlTypes.label, "Screw Extension | Spindelhub | Course d'Appr. [mm]", height, null, PageNames.angDHeber, null},
            new object[] { "drawing_angDHeber_screwextension_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeber, (int)Decorator_e.metric},
            new object[] { "drawing_angDHeber_screwextension_inch_label", (int)ControlTypes.label, "Screw Extension | Spindelhub | Course d'Appr. [inch]", height, null, PageNames.angDHeber, null},
            new object[] { "drawing_angDHeber_screwextension_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeber, (int)Decorator_e.imperial},
            new object[] { "drawing_angDHeber_extendedheight_mm_label", (int)ControlTypes.label, "Extended Height | Bauhöhe Max. | Hauteur Totale [mm]", height, null, PageNames.angDHeber, null},
            new object[] { "drawing_angDHeber_extendedheight_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeber, (int)Decorator_e.metric},
            new object[] { "drawing_angDHeber_extendedheight_inch_label", (int)ControlTypes.label, "Extended Height | Bauhöhe Max. | Hauteur Totale [inch]", height, null, PageNames.angDHeber, null},
            new object[] { "drawing_angDHeber_extendedheight_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeber, (int)Decorator_e.imperial},

            // drawing - Angebotsblatt_A4_D-Heber_Zweizeilig
            
            new object[] { "drawing_angDHeberZwei_mode_metric_checkbox", (int)ControlTypes.checkBox, "Metric", controlHeight, null, PageNames.angDHeberZwei, (int)Decorator_e.mode_switch}, // ADD NEW DECORATOR!
            new object[] { "drawing_angDHeberZwei_mode_imperial_checkbox", (int)ControlTypes.checkBox, "Imperial", controlHeight, null, PageNames.angDHeberZwei, (int)Decorator_e.mode_switch}, // ADD NEW DECORATOR!
            new object[] { "drawing_angDHeberZwei_mode_separator", (int)ControlTypes.separator, "", height, null, PageNames.angDHeberZwei, null},

            new object[] { "drawing_angDHeberZwei_model_label", (int)ControlTypes.label, "Model | Modell | Reference", height, null, PageNames.angDHeberZwei, null},
            new object[] { "drawing_angDHeberZwei_model_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeberZwei, (int)Decorator_e.standard},
            new object[] { "drawing_angDHeberZwei_capacity_t_label", (int)ControlTypes.label, "Capacity | Hubkraft | Charge Nom. [t]", height, null, PageNames.angDHeberZwei, null},
            new object[] { "drawing_angDHeberZwei_capacity_t_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeberZwei, (int)Decorator_e.metric},
            new object[] { "drawing_angDHeberZwei_capacity_ton_label", (int)ControlTypes.label, "Capacity | Hubkraft | Charge Nom. [ton]", height, null, PageNames.angDHeberZwei, null},
            new object[] { "drawing_angDHeberZwei_capacity_ton_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeberZwei, (int)Decorator_e.imperial},
            new object[] { "drawing_angDHeberZwei_closedheight_mm_label", (int)ControlTypes.label, "Closed Height | Bauhöhe Min. | Hauteur Fermee [mm]", height, null, PageNames.angDHeberZwei, null},
            new object[] { "drawing_angDHeberZwei_closedheight_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeberZwei, (int)Decorator_e.metric},
            new object[] { "drawing_angDHeberZwei_closedheight_inch_label", (int)ControlTypes.label, "Closed Height | Bauhöhe Min. | Hauteur Fermee [inch]", height, null, PageNames.angDHeberZwei, null},
            new object[] { "drawing_angDHeberZwei_closedheight_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeberZwei, (int)Decorator_e.imperial},
            new object[] { "drawing_angDHeberZwei_hydrauliclift_mm_label", (int)ControlTypes.label, "Hydraulic Lift | Hydraulische Hub | Course Hydraulique [mm]", height, null, PageNames.angDHeberZwei, null},
            new object[] { "drawing_angDHeberZwei_hydrauliclift_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeberZwei, (int)Decorator_e.metric},
            new object[] { "drawing_angDHeberZwei_hydrauliclift_inch_label", (int)ControlTypes.label, "Hydraulic Lift | Hydraulische Hub | Course Hydraulique [inch]", height, null, PageNames.angDHeberZwei, null},
            new object[] { "drawing_angDHeberZwei_hydrauliclift_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeberZwei, (int)Decorator_e.imperial},
            new object[] { "drawing_angDHeberZwei_screwextension_mm_label", (int)ControlTypes.label, "Screw Extension | Spindelhub | Course d'Appr. [mm]", height, null, PageNames.angDHeberZwei, null},
            new object[] { "drawing_angDHeberZwei_screwextension_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeberZwei, (int)Decorator_e.metric},
            new object[] { "drawing_angDHeberZwei_screwextension_inch_label", (int)ControlTypes.label, "Screw Extension | Spindelhub | Course d'Appr. [inch]", height, null, PageNames.angDHeberZwei, null},
            new object[] { "drawing_angDHeberZwei_screwextension_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeberZwei, (int)Decorator_e.imperial},
            new object[] { "drawing_angDHeberZwei_extendedheight_mm_label", (int)ControlTypes.label, "Extended Height | Bauhöhe Max. | Hauteur Totale [mm]", height, null, PageNames.angDHeberZwei, null},
            new object[] { "drawing_angDHeberZwei_extendedheight_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeberZwei, (int)Decorator_e.metric},
            new object[] { "drawing_angDHeberZwei_extendedheight_inch_label", (int)ControlTypes.label, "Extended Height | Bauhöhe Max. | Hauteur Totale [inch]", height, null, PageNames.angDHeberZwei, null},
            new object[] { "drawing_angDHeberZwei_extendedheight_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angDHeberZwei, (int)Decorator_e.imperial},
            
            // drawing - Angebotsblatt_A4_Hydr_Stuetze

            new object[] { "drawing_angHydrStuetze_mode_metric_checkbox", (int)ControlTypes.checkBox, "Metric", controlHeight, null, PageNames.angHydrStuetze, (int)Decorator_e.mode_switch}, // ADD NEW DECORATOR!
            new object[] { "drawing_angHydrStuetze_mode_imperial_checkbox", (int)ControlTypes.checkBox, "Imperial", controlHeight, null, PageNames.angHydrStuetze , (int)Decorator_e.mode_switch}, // ADD NEW DECORATOR!
            new object[] { "drawing_angHydrStuetze_mode_separator", (int)ControlTypes.separator, "", height, null, PageNames.angHydrStuetze, null},

            new object[] { "drawing_angHydrStuetze_model_label", (int)ControlTypes.label, "Model | Modell | Reference", height, null, PageNames.angHydrStuetze, null},
            new object[] { "drawing_angHydrStuetze_model_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angHydrStuetze, (int)Decorator_e.standard},
            new object[] { "drawing_angHydrStuetze_capacity_t_label", (int)ControlTypes.label, "Capacity | Hubkraft | Charge Nom. [t]", height, null, PageNames.angHydrStuetze, null},
            new object[] { "drawing_angHydrStuetze_capacity_t_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angHydrStuetze, (int)Decorator_e.metric},
            new object[] { "drawing_angHydrStuetze_capacity_ton_label", (int)ControlTypes.label, "Capacity | Hubkraft | Charge Nom. [ton]", height, null, PageNames.angHydrStuetze, null},
            new object[] { "drawing_angHydrStuetze_capacity_ton_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angHydrStuetze, (int)Decorator_e.imperial},
            new object[] { "drawing_angHydrStuetze_closedheight_mm_label", (int)ControlTypes.label, "Closed Height | Bauhöhe Min. | Hauteur Fermee [mm]", height, null, PageNames.angHydrStuetze, null},
            new object[] { "drawing_angHydrStuetze_closedheight_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angHydrStuetze, (int)Decorator_e.metric},
            new object[] { "drawing_angHydrStuetze_closedheight_inch_label", (int)ControlTypes.label, "Closed Height | Bauhöhe Min. | Hauteur Fermee [inch]", height, null, PageNames.angHydrStuetze, null},
            new object[] { "drawing_angHydrStuetze_closedheight_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angHydrStuetze, (int)Decorator_e.imperial},
            new object[] { "drawing_angHydrStuetze_hydrauliclift_mm_label", (int)ControlTypes.label, "Hydraulic Lift | Hydraulische Hub | Course Hydraulique [mm]", height, null, PageNames.angHydrStuetze, null},
            new object[] { "drawing_angHydrStuetze_hydrauliclift_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angHydrStuetze, (int)Decorator_e.metric},
            new object[] { "drawing_angHydrStuetze_hydrauliclift_inch_label", (int)ControlTypes.label, "Hydraulic Lift | Hydraulische Hub | Course Hydraulique [inch]", height, null, PageNames.angHydrStuetze, null},
            new object[] { "drawing_angHydrStuetze_hydrauliclift_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angHydrStuetze, (int)Decorator_e.imperial},
            new object[] { "drawing_angHydrStuetze_screwextension_mm_label", (int)ControlTypes.label, "Screw Extension | Spindelhub | Course d'Appr. [mm]", height, null, PageNames.angHydrStuetze, null},
            new object[] { "drawing_angHydrStuetze_screwextension_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angHydrStuetze, (int)Decorator_e.metric},
            new object[] { "drawing_angHydrStuetze_screwextension_inch_label", (int)ControlTypes.label, "Screw Extension | Spindelhub | Course d'Appr. [inch]", height, null, PageNames.angHydrStuetze, null},
            new object[] { "drawing_angHydrStuetze_screwextension_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angHydrStuetze, (int)Decorator_e.imperial},
            new object[] { "drawing_angHydrStuetze_extendedheight_mm_label", (int)ControlTypes.label, "Extended Height | Bauhöhe Max. | Hauteur Totale [mm]", height, null, PageNames.angHydrStuetze, null},
            new object[] { "drawing_angHydrStuetze_extendedheight_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angHydrStuetze, (int)Decorator_e.metric},
            new object[] { "drawing_angHydrStuetze_extendedheight_inch_label", (int)ControlTypes.label, "Extended Height | Bauhöhe Max. | Hauteur Totale [inch]", height, null, PageNames.angHydrStuetze, null},
            new object[] { "drawing_angHydrStuetze_extendedheight_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angHydrStuetze, (int)Decorator_e.imperial},

            // drawing - Angebotsblatt_A4_IDGR

            new object[] { "drawing_angIDGR_mode_metric_checkbox", (int)ControlTypes.checkBox, "Metric", controlHeight, null, PageNames.angIDGR, (int)Decorator_e.mode_switch}, // ADD NEW DECORATOR!
            new object[] { "drawing_angIDGR_mode_imperial_checkbox", (int)ControlTypes.checkBox, "Imperial", controlHeight, null, PageNames.angIDGR , (int)Decorator_e.mode_switch}, // ADD NEW DECORATOR!
            new object[] { "drawing_angIDGR_mode_separator", (int)ControlTypes.separator, "", height, null, PageNames.angIDGR, null},

            new object[] { "drawing_angIDGR_model_label", (int)ControlTypes.label, "Model | Modell | Reference", height, null, PageNames.angIDGR, null},
            new object[] { "drawing_angIDGR_model_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angIDGR, (int)Decorator_e.standard},
            new object[] { "drawing_angIDGR_revision_label", (int)ControlTypes.label, "Revision Status | Revision Stand", height, null, PageNames.angIDGR, null},
            new object[] { "drawing_angIDGR_revision_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angIDGR, (int)Decorator_e.standard},
            new object[] { "drawing_angIDGR_capacity_kg_label", (int)ControlTypes.label, "Capacity | Hubkraft | Charge Nom. [kg]", height, null, PageNames.angIDGR, null},
            new object[] { "drawing_angIDGR_capacity_kg_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angIDGR, (int)Decorator_e.metric},
            new object[] { "drawing_angIDGR_capacity_lb_label", (int)ControlTypes.label, "Capacity | Hubkraft | Charge Nom. [lb]", height, null, PageNames.angIDGR, null},
            new object[] { "drawing_angIDGR_capacity_lb_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angIDGR, (int)Decorator_e.imperial},
            new object[] { "drawing_angIDGR_closedheight_mm_label", (int)ControlTypes.label, "Closed Height | Bauhöhe Min. | Hauteur Fermee [mm]", height, null, PageNames.angIDGR, null},
            new object[] { "drawing_angIDGR_closedheight_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angIDGR, (int)Decorator_e.metric},
            new object[] { "drawing_angIDGR_closedheight_inch_label", (int)ControlTypes.label, "Closed Height | Bauhöhe Min. | Hauteur Fermee [inch]", height, null, PageNames.angIDGR, null},
            new object[] { "drawing_angIDGR_closedheight_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angIDGR, (int)Decorator_e.imperial},
            new object[] { "drawing_angIDGR_hydrauliclift_mm_label", (int)ControlTypes.label, "Hydraulic Lift | Hydraulische Hub | Course Hydraulique [mm]", height, null, PageNames.angIDGR, null},
            new object[] { "drawing_angIDGR_hydrauliclift_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angIDGR, (int)Decorator_e.metric},
            new object[] { "drawing_angIDGR_hydrauliclift_inch_label", (int)ControlTypes.label, "Hydraulic Lift | Hydraulische Hub | Course Hydraulique [mm]", height, null, PageNames.angIDGR, null},
            new object[] { "drawing_angIDGR_hydrauliclift_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angIDGR, (int)Decorator_e.imperial},
            new object[] { "drawing_angIDGR_extendedheight_mm_label", (int)ControlTypes.label, "Extended Height | Bauhöhe Max. | Hauteur Totale [mm]", height, null, PageNames.angIDGR, null},
            new object[] { "drawing_angIDGR_extendedheight_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angIDGR, (int)Decorator_e.metric},
            new object[] { "drawing_angIDGR_extendedheight_inch_label", (int)ControlTypes.label, "Extended Height | Bauhöhe Max. | Hauteur Totale [inch]", height, null, PageNames.angIDGR, null},
            new object[] { "drawing_angIDGR_extendedheight_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angIDGR, (int)Decorator_e.imperial},
            new object[] { "drawing_angIDGR_ac_label", (int)ControlTypes.label, "Aircraft (AC)", height, null, PageNames.angIDGR, null},
            new object[] { "drawing_angIDGR_ac_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angIDGR, (int)Decorator_e.standard},
            new object[] { "drawing_angIDGR_typeofengine_label", (int)ControlTypes.label, "Type of Engine", height, null, PageNames.angIDGR, null},
            new object[] { "drawing_angIDGR_typeofengine_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angIDGR, (int)Decorator_e.standard},

            // drawing - Angebotsblatt_A4_Mech_Stuetze

            new object[] { "drawing_angMechStuetze_mode_metric_checkbox", (int)ControlTypes.checkBox, "Metric", controlHeight, null, PageNames.angMechStuetze, (int)Decorator_e.mode_switch}, // ADD NEW DECORATOR!
            new object[] { "drawing_angMechStuetze_mode_imperial_checkbox", (int)ControlTypes.checkBox, "Imperial", controlHeight, null, PageNames.angMechStuetze , (int)Decorator_e.mode_switch}, // ADD NEW DECORATOR!
            new object[] { "drawing_angMechStuetze_mode_separator", (int)ControlTypes.separator, "", height, null, PageNames.angMechStuetze, null},

            new object[] { "drawing_angMechStuetze_model_label", (int)ControlTypes.label, "Model | Modell | Reference", height, null, PageNames.angMechStuetze, null},
            new object[] { "drawing_angMechStuetze_model_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angMechStuetze, (int)Decorator_e.standard},
            new object[] { "drawing_angMechStuetze_revision_label", (int)ControlTypes.label, "Revision Status | Revision Stand", height, null, PageNames.angMechStuetze, null},
            new object[] { "drawing_angMechStuetze_revision_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angMechStuetze, (int)Decorator_e.standard},
            new object[] { "drawing_angMechStuetze_capacity_t_label", (int)ControlTypes.label, "Capacity | Hubkraft | Charge Nom. [t]", height, null, PageNames.angMechStuetze, null},
            new object[] { "drawing_angMechStuetze_capacity_t_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angMechStuetze, (int)Decorator_e.metric},
            new object[] { "drawing_angMechStuetze_capacity_ton_label", (int)ControlTypes.label, "Capacity | Hubkraft | Charge Nom. [ton]", height, null, PageNames.angMechStuetze, null},
            new object[] { "drawing_angMechStuetze_capacity_ton_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angMechStuetze, (int)Decorator_e.imperial},
            new object[] { "drawing_angMechStuetze_closedheight_mm_label", (int)ControlTypes.label, "Closed Height | Bauhöhe Min. | Hauteur Fermee [mm]", height, null, PageNames.angMechStuetze, null},
            new object[] { "drawing_angMechStuetze_closedheight_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angMechStuetze, (int)Decorator_e.metric},
            new object[] { "drawing_angMechStuetze_closedheight_inch_label", (int)ControlTypes.label, "Closed Height | Bauhöhe Min. | Hauteur Fermee [inch]", height, null, PageNames.angMechStuetze, null},
            new object[] { "drawing_angMechStuetze_closedheight_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angMechStuetze, (int)Decorator_e.imperial},
            new object[] { "drawing_angMechStuetze_screwextension_mm_label", (int)ControlTypes.label, "Screw Extension | Sindelhub | Course d'Appr. [mm]", height, null, PageNames.angMechStuetze, null},
            new object[] { "drawing_angMechStuetze_screwextension_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angMechStuetze, (int)Decorator_e.metric},
            new object[] { "drawing_angMechStuetze_screwextension_inch_label", (int)ControlTypes.label, "Screw Extension | Sindelhub | Course d'Appr. [inch]", height, null, PageNames.angMechStuetze, null},
            new object[] { "drawing_angMechStuetze_screwextension_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angMechStuetze, (int)Decorator_e.imperial},
            new object[] { "drawing_angMechStuetze_extendedheight_mm_label", (int)ControlTypes.label, "Extended Height | Bauhöhe Max. | Hauteur Totale [mm]", height, null, PageNames.angMechStuetze, null},
            new object[] { "drawing_angMechStuetze_extendedheight_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angMechStuetze, (int)Decorator_e.metric},
            new object[] { "drawing_angMechStuetze_extendedheight_inch_label", (int)ControlTypes.label, "Extended Height | Bauhöhe Max. | Hauteur Totale [inch]", height, null, PageNames.angMechStuetze, null},
            new object[] { "drawing_angMechStuetze_extendedheight_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angMechStuetze, (int)Decorator_e.imperial},

            // drawing -  Angebotsblatt_A4_Pruefvorrichtung

            new object[] { "drawing_angPruefvorr_model_label", (int)ControlTypes.label, "Model | Modell", height, null, PageNames.angPruefvorr, null},
            new object[] { "drawing_angPruefvorr_model_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angPruefvorr, (int)Decorator_e.standard},
            new object[] { "drawing_angPruefvorr_revision_label", (int)ControlTypes.label, "Revision Status | Revision Stand", height, null, PageNames.angPruefvorr, null},
            new object[] { "drawing_angPruefvorr_revision_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angPruefvorr, (int)Decorator_e.standard},

            // drawing - Angebotsblatt_A4_R-Heber

            new object[] { "drawing_angRHeber_mode_metric_checkbox", (int)ControlTypes.checkBox, "Metric", controlHeight, null, PageNames.angRHeber, (int)Decorator_e.mode_switch}, // ADD NEW DECORATOR!
            new object[] { "drawing_angRHeber_mode_imperial_checkbox", (int)ControlTypes.checkBox, "Imperial", controlHeight, null, PageNames.angRHeber, (int)Decorator_e.mode_switch}, // ADD NEW DECORATOR!
            new object[] { "drawing_angRHeber_mode_separator", (int)ControlTypes.separator, "", height, null, PageNames.angRHeber, null},

            new object[] { "drawing_angRHeber_model_label", (int)ControlTypes.label, "Model | Modell | Reference", height, null, PageNames.angRHeber, null},
            new object[] { "drawing_angRHeber_model_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angRHeber, (int)Decorator_e.standard},
            new object[] { "drawing_angRHeber_revision_label", (int)ControlTypes.label, "Revision Status | Revision Stand", height, null, PageNames.angRHeber, null},
            new object[] { "drawing_angRHeber_revision_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angRHeber, (int)Decorator_e.standard},
            new object[] { "drawing_angRHeber_capacity_t_label", (int)ControlTypes.label, "Capacity | Hubkraft | Charge Nom. [t]", height, null, PageNames.angRHeber, null},
            new object[] { "drawing_angRHeber_capacity_t_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angRHeber, (int)Decorator_e.metric},
            new object[] { "drawing_angRHeber_capacity_ton_label", (int)ControlTypes.label, "Capacity | Hubkraft | Charge Nom. [ton]", height, null, PageNames.angRHeber, null},
            new object[] { "drawing_angRHeber_capacity_ton_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angRHeber, (int)Decorator_e.imperial},
            new object[] { "drawing_angRHeber_closedheight_mm_label", (int)ControlTypes.label, "Closed Height | Bauhöhe Min. | Hauteur Fermee [mm]", height, null, PageNames.angRHeber, null},
            new object[] { "drawing_angRHeber_closedheight_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angRHeber, (int)Decorator_e.metric},
            new object[] { "drawing_angRHeber_closedheight_inch_label", (int)ControlTypes.label, "Closed Height | Bauhöhe Min. | Hauteur Fermee [inch]", height, null, PageNames.angRHeber, null},
            new object[] { "drawing_angRHeber_closedheight_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angRHeber, (int)Decorator_e.imperial},
            new object[] { "drawing_angRHeber_hydrauliclift_mm_label", (int)ControlTypes.label, "Hydraulic Lift | Hydraulische Hub | Course Hydraulique [mm]", height, null, PageNames.angRHeber, null},
            new object[] { "drawing_angRHeber_hydrauliclift_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angRHeber, (int)Decorator_e.metric},
            new object[] { "drawing_angRHeber_hydrauliclift_inch_label", (int)ControlTypes.label, "Hydraulic Lift | Hydraulische Hub | Course Hydraulique [inch]", height, null, PageNames.angRHeber, null},
            new object[] { "drawing_angRHeber_hydrauliclift_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angRHeber, (int)Decorator_e.imperial},
            new object[] { "drawing_angRHeber_screwextension_mm_label", (int)ControlTypes.label, "Screw Extension | Spindelhub | Course d'Appr. [mm]", height, null, PageNames.angRHeber, null},
            new object[] { "drawing_angRHeber_screwextension_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angRHeber, (int)Decorator_e.metric},
            new object[] { "drawing_angRHeber_screwextension_inch_label", (int)ControlTypes.label, "Screw Extension | Spindelhub | Course d'Appr. [inch]", height, null, PageNames.angRHeber, null},
            new object[] { "drawing_angRHeber_screwextension_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angRHeber, (int)Decorator_e.imperial},
            new object[] { "drawing_angRHeber_usableheight_mm_label", (int)ControlTypes.label, "Usable Height | Nutzbare Höhe | Hauteur Utilisable [mm]", height, null, PageNames.angRHeber, null},
            new object[] { "drawing_angRHeber_usableheight_mm_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angRHeber, (int)Decorator_e.metric},
            new object[] { "drawing_angRHeber_usableheight_inch_label", (int)ControlTypes.label, "Usable Height | Nutzbare Höhe | Hauteur Utilisable [inch]", height, null, PageNames.angRHeber, null},
            new object[] { "drawing_angRHeber_usableheight_inch_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angRHeber, (int)Decorator_e.imperial},

            // drawing - Angebotsblatt_A4_Steuerpult

            new object[] { "drawing_angSteuerp_model_label", (int)ControlTypes.label, "Model | Modell", height, null, PageNames.angSteuerp, null},
            new object[] { "drawing_angSteuerp_model_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angSteuerp, (int)Decorator_e.standard},
            new object[] { "drawing_angSteuerp_revision_label", (int)ControlTypes.label, "Revision Status | Revision Stand", height, null, PageNames.angSteuerp, null},
            new object[] { "drawing_angSteuerp_revision_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angSteuerp, (int)Decorator_e.standard},

            // drawing - Angebotsblatt_A4_TOW

            new object[] { "drawing_angTOW_model_label", (int)ControlTypes.label, "Model | Modell", height, null, PageNames.angTOW, null},
            new object[] { "drawing_angTOW_model_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angTOW, (int)Decorator_e.standard},
            new object[] { "drawing_angTOW_revision_label", (int)ControlTypes.label, "Revision Status | Revision Stand", height, null, PageNames.angTOW, null},
            new object[] { "drawing_angTOW_revision_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angTOW, (int)Decorator_e.standard},
            new object[] { "drawing_angTOW_ac_label", (int)ControlTypes.label, "Aircraft (AC)", height, null, PageNames.angTOW, null},
            new object[] { "drawing_angTOW_ac_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angTOW, (int)Decorator_e.standard},

            // drawing - Angebotsblatt_A4_Wiegeeinrichtung

            new object[] { "drawing_angWiegeeinr_model_label", (int)ControlTypes.label, "Model | Modell", height, null, PageNames.angWiegeeinr, null},
            new object[] { "drawing_angWiegeeinr_model_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angWiegeeinr, (int)Decorator_e.standard},
            new object[] { "drawing_angWiegeeinr_revision_label", (int)ControlTypes.label, "Revision Status | Revision Stand", height, null, PageNames.angWiegeeinr, null},
            new object[] { "drawing_angWiegeeinr_revision_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angWiegeeinr, (int)Decorator_e.standard},
            new object[] { "drawing_angWiegeeinr_capacity_KN_label", (int)ControlTypes.label, "Capacity [KN]", height, null, PageNames.angWiegeeinr, null},
            new object[] { "drawing_angWiegeeinr_capacity_KN_textbox", (int)ControlTypes.textBox, "", controlHeight, null, PageNames.angWiegeeinr, (int)Decorator_e.standard}

        };

        public static Dictionary<string, int> controlAttributeClassesPairs = new Dictionary<string, int>
        {
            { "part_description_dropdown" , (int)AttributeClass_e.description},
            { "part_additionaltext_dropdown" , (int)AttributeClass_e.addtext},
            { "part_caddescription_textbox" , (int)AttributeClass_e.caddescription},
            { "part_material_dropdown" , (int)AttributeClass_e.materialhydro1},
            { "part_yieldstrength_textbox" , (int)AttributeClass_e.yieldstrength}, // Yield strength
            { "part_tensilestrength_textbox" , (int)AttributeClass_e.tensilestrength}, // Tensile strength
            { "part_elongationatbreak_textbox", (int)AttributeClass_e.elongation}, // elongation at break
            { "part_materialalt_dropdown" , (int)AttributeClass_e.materialhydro2},
            { "part_materialadd_textbox" , (int)AttributeClass_e.materialhydroadd},
            { "part_materialorig_dropdown" , (int)AttributeClass_e.materialoriginal},
            { "part_materialorigadd_textbox" , (int)AttributeClass_e.materialoriginaladd},
            { "part_specialselection_dropdown" , (int)AttributeClass_e.specialselection}, // add special selection
            { "part_heattreat_dropdown" , (int)AttributeClass_e.heattreatment},
            { "part_preconditioning_dropdown" , (int)AttributeClass_e.preconditioning},
            { "part_surface_dropdown" , (int)AttributeClass_e.surfacefinish},
            { "part_color_dropdown" , (int)AttributeClass_e.standardcolor},
            { "part_varnishing_dropdown" , (int)AttributeClass_e.lacquering},
            { "part_additionalinfo_dropdown" , (int)AttributeClass_e.addinformation},
            { "part_basedon_textbox" , (int)AttributeClass_e.basedon},
            { "part_generaltolerance_dropdown" , (int)AttributeClass_e.generaltolerances},
            { "part_thermalcut_dropdown" , (int)AttributeClass_e.sepmethodsprecision},
            { "part_semifinished_dropdown" , (int)AttributeClass_e.semifinishedstandard},
            { "part_welding_checkbox" , (int)AttributeClass_e.weldment},
            { "part_dyepenetration_dropdown" , (int)AttributeClass_e.dyepenetrationinsp},
            { "part_visualinspection_dropdown" , (int)AttributeClass_e.visualinsp},
            { "part_qualityimperfections_dropdown" , (int)AttributeClass_e.qualitylevelimp},
            // add measurements + weight
            { "part_metricmode_checkbox", (int)AttributeClass_e.NONE},
            { "part_imperialmode_checkbox", (int)AttributeClass_e.NONE},
            { "part_dimensions_stack", (int)AttributeClass_e.measurements},
            { "part_imperialdimensions_stack", (int)AttributeClass_e.measurements_inch},
            { "part_weight_stack", (int)AttributeClass_e.weight_kg},
            { "part_imperialweight_stack", (int)AttributeClass_e.weight_lbs},
            // ############
            { "assembly_description_dropdown" , (int)AttributeClass_e.description},
            { "assembly_additionaltext_dropdown" , (int)AttributeClass_e.addtext},
            { "assembly_caddescription_textbox" , (int)AttributeClass_e.caddescription},
            { "assembly_specialselection_dropdown" , (int)AttributeClass_e.specialselection}, // add special selection
            { "assembly_heattreat_dropdown" , (int)AttributeClass_e.heattreatment},
            { "assembly_preconditioning_dropdown" , (int)AttributeClass_e.preconditioning},
            { "assembly_surface_dropdown" , (int)AttributeClass_e.surfacefinish},
            { "assembly_color_dropdown" , (int)AttributeClass_e.standardcolor},
            { "assembly_varnishing_dropdown" , (int)AttributeClass_e.lacquering},
            { "assembly_additionalinfo_dropdown" , (int)AttributeClass_e.addinformation},
            { "assembly_basedon_textbox" , (int)AttributeClass_e.basedon},
            { "assembly_generaltolerance_dropdown" , (int)AttributeClass_e.generaltolerances},
            { "assembly_thermalcut_dropdown" , (int)AttributeClass_e.sepmethodsprecision},
            { "assembly_semifinished_dropdown" , (int)AttributeClass_e.semifinishedstandard},
            { "assembly_welding_checkbox" , (int)AttributeClass_e.weldassembly},
            { "assembly_dyepenetration_dropdown" , (int)AttributeClass_e.dyepenetrationinsp},
            { "assembly_visualinspection_dropdown" , (int)AttributeClass_e.visualinsp},
            { "assembly_qualityimperfections_dropdown" , (int)AttributeClass_e.qualitylevelimp},
            { "drawing_changeno_textbox" , (int)AttributeClass_e.changeno},
            { "drawing_changedescription_textbox" , (int)AttributeClass_e.changeshortdescription},
            { "drawing_occurs_textbox" , (int)AttributeClass_e.occurs},
            { "drawing_additionalinfo_textbox" , (int)AttributeClass_e.addinformation},
            { "drawing_specialselection_textbox" , (int)AttributeClass_e.specialselection}, // add special selection
            { "drawing_heattreat_textbox" , (int)AttributeClass_e.heattreatment},
            { "drawing_preconditioning_textbox" , (int)AttributeClass_e.preconditioning},
            { "drawing_surface_textbox" , (int)AttributeClass_e.surfacefinish},
            { "drawing_color_textbox" , (int)AttributeClass_e.standardcolor},
            { "drawing_varnishing_textbox" , (int)AttributeClass_e.lacquering},
            { "drawing_basedon_textbox" , (int)AttributeClass_e.basedon},
            { "drawing_generaltolerance_textbox" , (int)AttributeClass_e.generaltolerances},
            { "drawing_thermalcut_textbox" , (int)AttributeClass_e.sepmethodsprecision},
            { "drawing_semifinished_textbox" , (int)AttributeClass_e.semifinishedstandard},
            { "drawing_welding_checkbox" , (int)AttributeClass_e.weldassembly},
            { "drawing_dyepenetration_textbox" , (int)AttributeClass_e.dyepenetrationinsp},
            { "drawing_visualinspection_textbox" , (int)AttributeClass_e.visualinsp},
            { "drawing_qualityimperfections_textbox" , (int)AttributeClass_e.qualitylevelimp},
            { "drawing_angCobra_revision_textbox" , (int)AttributeClass_e.revisionstatus},
            { "drawing_angCobra_capacity_t_textbox" , (int)AttributeClass_e.capacity_t},
            { "drawing_angCobra_capacity_lbs_textbox" , (int)AttributeClass_e.capacity_lbs},
            { "drawing_angCobra_closedheightA_mm_textbox" , (int)AttributeClass_e.closedheightA_mm},
            { "drawing_angCobra_closedheightA_inch_textbox" , (int)AttributeClass_e.closedheightA_inch},
            { "drawing_angCobra_liftstrokeB_mm_textbox" , (int)AttributeClass_e.liftstrokeB_mm},
            { "drawing_angCobra_liftstrokeB_inch_textbox" , (int)AttributeClass_e.liftstrokeB_inch},
            { "drawing_angCobra_closedheightC_mm_textbox" , (int)AttributeClass_e.closedheightC_mm},
            { "drawing_angCobra_closedheightC_inch_textbox" , (int)AttributeClass_e.closedheightC_inch},
            { "drawing_angCobra_liftstrokeD_mm_textbox" , (int)AttributeClass_e.liftstrokeD_mm},
            { "drawing_angCobra_liftstrokeD_inch_textbox" , (int)AttributeClass_e.liftstrokeD_inch},
            { "drawing_angCobra_Model_textbox" , (int)AttributeClass_e.Model},
            { "drawing_angCobra_mode_metric_checkbox" , (int)AttributeClass_e.NONE},
            { "drawing_angCobra_mode_imperial_checkbox" , (int)AttributeClass_e.NONE},
            { "drawing_angDHeber_capacity_t_textbox" , (int)AttributeClass_e.capacity_t},
            { "drawing_angDHeber_capacity_ton_textbox" , (int)AttributeClass_e.capacity_ton},
            { "drawing_angDHeber_closedheight_mm_textbox" , (int)AttributeClass_e.closedheight_mm},
            { "drawing_angDHeber_closedheight_inch_textbox" , (int)AttributeClass_e.closedheight_inch},
            { "drawing_angDHeber_hydrauliclift_mm_textbox" , (int)AttributeClass_e.hydrauliclift_mm},
            { "drawing_angDHeber_hydrauliclift_inch_textbox" , (int)AttributeClass_e.hydrauliclift_inch},
            { "drawing_angDHeber_screwextension_mm_textbox" , (int)AttributeClass_e.screwextension_mm},
            { "drawing_angDHeber_screwextension_inch_textbox" , (int)AttributeClass_e.screwextension_inch},
            { "drawing_angDHeber_extendedheight_mm_textbox" , (int)AttributeClass_e.extendedheight_mm},
            { "drawing_angDHeber_extendedheight_inch_textbox" , (int)AttributeClass_e.extendedheight_inch},
            { "drawing_angDHeber_model_textbox" , (int)AttributeClass_e.Model},
            { "drawing_angDHeber_mode_metric_checkbox" , (int)AttributeClass_e.NONE},
            { "drawing_angDHeber_mode_imperial_checkbox" , (int)AttributeClass_e.NONE},
            { "drawing_angDHeberZwei_capacity_t_textbox" , (int)AttributeClass_e.capacity_t},
            { "drawing_angDHeberZwei_capacity_ton_textbox" , (int)AttributeClass_e.capacity_ton},
            { "drawing_angDHeberZwei_closedheight_mm_textbox" , (int)AttributeClass_e.closedheight_mm},
            { "drawing_angDHeberZwei_closedheight_inch_textbox" , (int)AttributeClass_e.closedheight_inch},
            { "drawing_angDHeberZwei_hydrauliclift_mm_textbox" , (int)AttributeClass_e.hydrauliclift_mm},
            { "drawing_angDHeberZwei_hydrauliclift_inch_textbox" , (int)AttributeClass_e.hydrauliclift_inch},
            { "drawing_angDHeberZwei_screwextension_mm_textbox" , (int)AttributeClass_e.screwextension_mm},
            { "drawing_angDHeberZwei_screwextension_inch_textbox" , (int)AttributeClass_e.screwextension_inch},
            { "drawing_angDHeberZwei_extendedheight_mm_textbox" , (int)AttributeClass_e.extendedheight_mm},
            { "drawing_angDHeberZwei_extendedheight_inch_textbox" , (int)AttributeClass_e.extendedheight_inch},
            { "drawing_angDHeberZwei_model_textbox" , (int)AttributeClass_e.Model},
            { "drawing_angDHeberZwei_mode_metric_checkbox" , (int)AttributeClass_e.NONE},
            { "drawing_angDHeberZwei_mode_imperial_checkbox" , (int)AttributeClass_e.NONE},
            { "drawing_angHydrStuetze_capacity_t_textbox" , (int)AttributeClass_e.capacity_t},
            { "drawing_angHydrStuetze_capacity_ton_textbox" , (int)AttributeClass_e.capacity_ton},
            { "drawing_angHydrStuetze_closedheight_mm_textbox" , (int)AttributeClass_e.closedheight_mm},
            { "drawing_angHydrStuetze_closedheight_inch_textbox" , (int)AttributeClass_e.closedheight_inch},
            { "drawing_angHydrStuetze_hydrauliclift_mm_textbox" , (int)AttributeClass_e.hydrauliclift_mm},
            { "drawing_angHydrStuetze_hydrauliclift_inch_textbox" , (int)AttributeClass_e.hydrauliclift_inch},
            { "drawing_angHydrStuetze_screwextension_mm_textbox" , (int)AttributeClass_e.screwextension_mm},
            { "drawing_angHydrStuetze_screwextension_inch_textbox" , (int)AttributeClass_e.screwextension_inch},
            { "drawing_angHydrStuetze_extendedheight_mm_textbox" , (int)AttributeClass_e.extendedheight_mm},
            { "drawing_angHydrStuetze_extendedheight_inch_textbox" , (int)AttributeClass_e.extendedheight_inch},
            { "drawing_angHydrStuetze_model_textbox" , (int)AttributeClass_e.Model},
            { "drawing_angHydrStuetze_mode_metric_checkbox" , (int)AttributeClass_e.NONE},
            { "drawing_angHydrStuetze_mode_imperial_checkbox" , (int)AttributeClass_e.NONE},
            { "drawing_angIDGR_revision_textbox" , (int)AttributeClass_e.revisionstatus},
            { "drawing_angIDGR_capacity_kg_textbox" , (int)AttributeClass_e.capacity_kg},
            { "drawing_angIDGR_capacity_lb_textbox" , (int)AttributeClass_e.capacity_lbs},
            { "drawing_angIDGR_closedheight_mm_textbox" , (int)AttributeClass_e.closedheight_mm},
            { "drawing_angIDGR_closedheight_inch_textbox" , (int)AttributeClass_e.closedheight_inch},
            { "drawing_angIDGR_hydrauliclift_mm_textbox" , (int)AttributeClass_e.hydrauliclift_mm},
            { "drawing_angIDGR_hydrauliclift_inch_textbox" , (int)AttributeClass_e.hydrauliclift_inch},
            { "drawing_angIDGR_extendedheight_mm_textbox" , (int)AttributeClass_e.extendedheight_mm},
            { "drawing_angIDGR_extendedheight_inch_textbox" , (int)AttributeClass_e.extendedheight_inch},
            { "drawing_angIDGR_ac_textbox" , (int)AttributeClass_e.ac},
            { "drawing_angIDGR_typeofengine_textbox" , (int)AttributeClass_e.typeofengine},
            { "drawing_angIDGR_model_textbox" , (int)AttributeClass_e.Model},
            { "drawing_angIDGR_mode_metric_checkbox" , (int)AttributeClass_e.NONE},
            { "drawing_angIDGR_mode_imperial_checkbox" , (int)AttributeClass_e.NONE},
            { "drawing_angMechStuetze_revision_textbox" , (int)AttributeClass_e.revisionstatus},
            { "drawing_angMechStuetze_capacity_t_textbox" , (int)AttributeClass_e.capacity_t},
            { "drawing_angMechStuetze_capacity_ton_textbox" , (int)AttributeClass_e.capacity_ton},
            { "drawing_angMechStuetze_closedheight_mm_textbox" , (int)AttributeClass_e.closedheight_mm},
            { "drawing_angMechStuetze_closedheight_inch_textbox" , (int)AttributeClass_e.closedheight_inch},
            { "drawing_angMechStuetze_screwextension_mm_textbox" , (int)AttributeClass_e.screwextension_mm},
            { "drawing_angMechStuetze_screwextension_inch_textbox" , (int)AttributeClass_e.screwextension_inch},
            { "drawing_angMechStuetze_extendedheight_mm_textbox" , (int)AttributeClass_e.extendedheight_mm},
            { "drawing_angMechStuetze_extendedheight_inch_textbox" , (int)AttributeClass_e.extendedheight_inch},
            { "drawing_angMechStuetze_model_textbox" , (int)AttributeClass_e.Model},
            { "drawing_angMechStuetze_mode_metric_checkbox" , (int)AttributeClass_e.NONE},
            { "drawing_angMechStuetze_mode_imperial_checkbox" , (int)AttributeClass_e.NONE},
            { "drawing_angPruefvorr_revision_textbox" , (int)AttributeClass_e.revisionstatus},
            { "drawing_angPruefvorr_model_textbox" , (int)AttributeClass_e.Model},
            { "drawing_angRHeber_revision_textbox" , (int)AttributeClass_e.revisionstatus},
            { "drawing_angRHeber_capacity_t_textbox" , (int)AttributeClass_e.capacity_t},
            { "drawing_angRHeber_capacity_ton_textbox" , (int)AttributeClass_e.capacity_ton},
            { "drawing_angRHeber_closedheight_mm_textbox" , (int)AttributeClass_e.closedheight_mm},
            { "drawing_angRHeber_closedheight_inch_textbox" , (int)AttributeClass_e.closedheight_inch},
            { "drawing_angRHeber_hydrauliclift_mm_textbox" , (int)AttributeClass_e.hydrauliclift_mm},
            { "drawing_angRHeber_hydrauliclift_inch_textbox" , (int)AttributeClass_e.hydrauliclift_inch},
            { "drawing_angRHeber_screwextension_mm_textbox" , (int)AttributeClass_e.screwextension_mm},
            { "drawing_angRHeber_screwextension_inch_textbox" , (int)AttributeClass_e.screwextension_inch},
            { "drawing_angRHeber_usableheight_mm_textbox" , (int)AttributeClass_e.usableheight_mm},
            { "drawing_angRHeber_usableheight_inch_textbox" , (int)AttributeClass_e.usableheight_inch},
            { "drawing_angRHeber_model_textbox" , (int)AttributeClass_e.Model},
            { "drawing_angRHeber_mode_metric_checkbox" , (int)AttributeClass_e.NONE},
            { "drawing_angRHeber_mode_imperial_checkbox" , (int)AttributeClass_e.NONE},
            { "drawing_angSteuerp_revision_textbox" , (int)AttributeClass_e.revisionstatus},
            { "drawing_angSteuerp_model_textbox" , (int)AttributeClass_e.Model},
            { "drawing_angTOW_revision_textbox" , (int)AttributeClass_e.revisionstatus},
            { "drawing_angTOW_ac_textbox" , (int)AttributeClass_e.ac},
            { "drawing_angTOW_model_textbox" , (int)AttributeClass_e.Model},
            { "drawing_angWiegeeinr_revision_textbox" , (int)AttributeClass_e.revisionstatus},
            { "drawing_angWiegeeinr_capacity_KN_textbox" , (int)AttributeClass_e.capacity_KN},
            { "drawing_angWiegeeinr_model_textbox" , (int)AttributeClass_e.Model}
        };

    }
}
