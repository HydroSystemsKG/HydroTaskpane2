using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HydroTaskpane2.Connectors;
using HydroTaskpane2.Fabrication;
using HydroTaskpane2.References;
using HydroTaskpane2.Decorators.Main;

namespace HydroTaskpane2.Decorators.Angebotsblatt
{
    public class ModeSwitchHandlerDecorator : HandlerDecorator, IControlProduct
    {
        public ModeSwitchHandlerDecorator(ControlProductComponent control) : base(control)
        {
            
        }

        public override void Assemble()
        {
            control.Assemble();

            int type = (int)control.parameters.getParameter("controlType");

            if (type != (int)ControlTypes.label && type != (int)ControlTypes.longLabel)
            {
                CheckBox element = (CheckBox)GetControl();

                if (element.Name.ToLower().Contains("_mode_metric_"))
                {
                    element.IsChecked = true;
                    element.Checked += new RoutedEventHandler(OnMetricChecked);
                }
                else if (element.Name.ToLower().Contains("_mode_imperial_"))
                {
                    element.IsChecked = false;
                    element.Checked += new RoutedEventHandler(OnImperialChecked);
                }
            }
        }

        public override void Dissassemble()
        {
            base.Dissassemble();

            CheckBox element = (CheckBox)GetControl();

            if (element.Name.ToLower().Contains("_mode_metric_"))
            {
                element.Checked -= new RoutedEventHandler(OnMetricChecked);
            }
            else if (element.Name.ToLower().Contains("_mode_imperial_"))
            {
                element.Checked -= new RoutedEventHandler(OnImperialChecked);
            }
        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }

        #region mode event handlers

        private void OnMetricChecked(object sender, RoutedEventArgs e)
        {
            CheckBox senderControl = (CheckBox)sender;

            controls = collectionSingleton.controlCollection;

            CheckBox imperialCheckBox = (CheckBox)(controls[senderControl.Name.Replace("_mode_metric_", "_mode_imperial_")]).GetControl();

            imperialCheckBox.IsChecked = false;

            foreach (string controlName in controls.Keys)
            {
                if (!controlName.ToLower().Contains("label") && !controlName.ToLower().Contains("separator"))
                {
                    if (new string[] { "_inch_", "_ton_", "_lbs_", "_lb_" }.Any(controlName.ToLower().Contains))
                    {
                        ControlProductComponent product = (ControlProductComponent)controls[controlName];
                        TextBox textBox = (TextBox)product.GetControl();

                        textBox.IsEnabled = false;
                    }
                    else if (new string[] { "_mm_", "_t_", "_kg_" }.Any(controlName.ToLower().Contains))
                    {
                        ControlProductComponent product = (ControlProductComponent)controls[controlName];
                        TextBox textBox = (TextBox)product.GetControl();

                        textBox.IsEnabled = true;
                    }
                }
            }

        }

        private void OnImperialChecked(object sender, RoutedEventArgs e)
        {
            CheckBox senderControl = (CheckBox)sender;

            controls = collectionSingleton.controlCollection;

            CheckBox metricCheckBox = (CheckBox)(controls[senderControl.Name.Replace("_mode_imperial_", "_mode_metric_")]).GetControl();

            metricCheckBox.IsChecked = false;

            foreach (string controlName in controls.Keys)
            {
                if (!controlName.ToLower().Contains("label") && !controlName.ToLower().Contains("separator"))
                {
                    if (new string[] { "_inch_", "_ton_", "_lbs_", "_lb_" }.Any(controlName.ToLower().Contains))
                    {
                        ControlProductComponent product = (ControlProductComponent)controls[controlName];
                        TextBox textBox = (TextBox)product.GetControl();

                        textBox.IsEnabled = true;
                    }
                    else if (new string[] { "_mm_", "_t_", "_kg_" }.Any(controlName.ToLower().Contains))
                    {
                        ControlProductComponent product = (ControlProductComponent)controls[controlName];
                        TextBox textBox = (TextBox)product.GetControl();

                        textBox.IsEnabled = false;
                    }
                }
            }
        }

        #endregion
    }
}
