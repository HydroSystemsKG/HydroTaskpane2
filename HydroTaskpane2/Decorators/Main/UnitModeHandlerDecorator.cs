using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using HydroTaskpane2.Connectors;
using HydroTaskpane2.Fabrication;
using HydroTaskpane2.References;
using SldWorks;
using SwConst;
using SwCommands;

namespace HydroTaskpane2.Decorators.Main
{
    public class UnitModeHandlerDecorator : HandlerDecorator, IControlProduct
    {
        #region unitMode variables 

        private readonly string draftingStandardPath = @"\\CAD_DE_SW\D_sw-pool\Hydro\System-Optionen\Macros\HydroTaskpane2\DIN-MODIFIZIERT.sldstd";
        private static readonly Dictionary<int, int> unitsMetricOptionPairs = new Dictionary<int, int>
        {
            {(int)swUserPreferenceIntegerValue_e.swUnitSystem, (int)swUnitSystem_e.swUnitSystem_Custom}, // set units
            // correct table
            {(int)swUserPreferenceIntegerValue_e.swUnitsLinear, (int)swLengthUnit_e.swMM}, // basic units - length unit (mm)
            {(int)swUserPreferenceIntegerValue_e.swUnitsLinearDecimalDisplay, (int)swFractionDisplay_e.swDECIMAL}, // basic units - length fractional/decimal display (decimal)
            {(int)swUserPreferenceIntegerValue_e.swUnitsLinearDecimalPlaces, 3}, // basic units - length decimals (.123)
            {(int)swUserPreferenceIntegerValue_e.swUnitsDualLinear, (int)swLengthUnit_e.swINCHES}, // basic units - dual dimension unit (inch)
            {(int)swUserPreferenceIntegerValue_e.swUnitsDualLinearDecimalDisplay, (int)swFractionDisplay_e.swDECIMAL}, // basic units - dual dimension fractional/decimal display (decimal)
            {(int)swUserPreferenceIntegerValue_e.swUnitsDualLinearDecimalPlaces, 3}, // basic units - dual dimension decimals (.123)
            {(int)swUserPreferenceIntegerValue_e.swUnitsAngular, (int)swAngleUnit_e.swDEGREES}, // basic units - angle unit (degrees)
            {(int)swUserPreferenceIntegerValue_e.swUnitsAngularDecimalPlaces, 3}, // basic units - angle decimals (.123)
            {(int)swUserPreferenceIntegerValue_e.swUnitsMassPropLength, (int)swLengthUnit_e.swMM}, // mass/section properties - length unit (mm)
            {(int)swUserPreferenceIntegerValue_e.swUnitsMassPropDecimalPlaces, 3}, // mass/section properties - length decimals (.123)
            {(int)swUserPreferenceIntegerValue_e.swUnitsMassPropMass, (int)swUnitsMassPropMass_e.swUnitsMassPropMass_Kilograms}, // mass/section properties - mass unit (kg)
            {(int)swUserPreferenceIntegerValue_e.swUnitsMassPropVolume, (int)swUnitsMassPropVolume_e.swUnitsMassPropVolume_Meters3}, // mass/section properties - per unit volume unit (m³)
            {(int)swUserPreferenceIntegerValue_e.swUnitsTimeUnits, (int)swUnitsTimeUnit_e.swUnitsTimeUnit_Second}, // motion units - time unit (s)
            {(int)swUserPreferenceIntegerValue_e.swUnitsTimeDecimalPlaces, 2} // motion units - time decimals (.12)
        };

        #endregion

        public UnitModeHandlerDecorator(ControlProductComponent control) : base(control)
        {

        }

        public override void Assemble()
        {
            control.Assemble();

            int type = (int)control.parameters.getParameter("controlType");

            CheckBox element = (CheckBox)GetControl();

            if (element.Name.ToLower().Contains("_metricmode_"))
            {
                element.IsChecked = true;
                element.Checked += new RoutedEventHandler(OnMetricChecked);
            }
            else if (element.Name.ToLower().Contains("_imperialmode_"))
            {
                element.IsChecked = false;
                element.Checked += new RoutedEventHandler(OnImperialChecked);
            }
        }

        public override void Dissassemble()
        {
            base.Dissassemble();

            CheckBox element = (CheckBox)GetControl();

            if (element.Name.ToLower().Contains("_metricmode_"))
            {
                element.Checked += new RoutedEventHandler(OnMetricChecked);
            }
            else if (element.Name.ToLower().Contains("_imperialmode_"))
            {
                element.Checked += new RoutedEventHandler(OnImperialChecked);
            }
        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }

        private void OnMetricChecked(object sender, RoutedEventArgs e)
        {
            CheckBox senderControl = (CheckBox)sender;

            controls = collectionSingleton.controlCollection;

            CheckBox imperialCheckBox = (CheckBox)(controls[senderControl.Name.Replace("_metricmode_", "_imperialmode_")]).GetControl();

            Debug.Print("Uncheck imperial box...");

            imperialCheckBox.IsChecked = false;

            Debug.Print("Uncheck imperial box...done");

            Debug.Print("Set Metric options...");

            // set options for metric mode
            // set overall drafting standard

            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;
            ModelDocExtension swExtension = swModel.Extension;

            swExtension.SetUserPreferenceInteger(
                (int)swUserPreferenceIntegerValue_e.swDetailingDimensionStandard,
                (int)swUserPreferenceOption_e.swDetailingNoOptionSpecified,
                (int)swDetailingStandard_e.swDetailingStandardUserDefined);

            swExtension.LoadDraftingStandard(draftingStandardPath);

            // set units

            foreach (int option in unitsMetricOptionPairs.Keys)
            {
                swExtension.SetUserPreferenceInteger(option, (int)swUserPreferenceOption_e.swDetailingNoOptionSpecified, unitsMetricOptionPairs[option]);
            }

            swModel.ForceRebuild3(true);

            Debug.Print("Set Metric options...done");
        }

        private void OnImperialChecked(object sender, RoutedEventArgs e)
        {
            CheckBox senderControl = (CheckBox)sender;

            controls = collectionSingleton.controlCollection;

            CheckBox metricCheckBox = (CheckBox)(controls[senderControl.Name.Replace("_imperialmode_", "_metricmode_")]).GetControl();

            Debug.Print("Uncheck metric box...");

            metricCheckBox.IsChecked = false;

            Debug.Print("Uncheck metric box...done");

            Debug.Print("Set Imperial options...");

            // set options for imperial mode
            // set overall drafting standard

            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;
            ModelDocExtension swExtension = swModel.Extension;

            swExtension.SetUserPreferenceInteger(
                (int)swUserPreferenceIntegerValue_e.swDetailingDimensionStandard,
                (int)swUserPreferenceOption_e.swDetailingNoOptionSpecified,
                (int)swDetailingStandard_e.swDetailingStandardANSI);

            // set units

            swExtension.SetUserPreferenceInteger(
                (int)swUserPreferenceIntegerValue_e.swUnitSystem,
                (int)swUserPreferenceOption_e.swDetailingNoOptionSpecified,
                (int)swUnitSystem_e.swUnitSystem_IPS);

            swModel.ForceRebuild3(true);

            Debug.Print("Set Imperial options...done");

        }

    }
}
