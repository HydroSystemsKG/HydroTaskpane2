using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using HydroTaskpane2.Connectors;
using HydroTaskpane2.Fabrication;
using HydroTaskpane2.References;
using HydroTaskpane2.Decorators.Main;

namespace HydroTaskpane2.Decorators.Angebotsblatt
{
    public class MetricHandlerDecorator : HandlerDecorator, IControlProduct
    {
        private readonly double mmToInch = 0.039370; // mm to inch
        private readonly double tToLbs = 2204.622622; // ton to pounds
        private readonly double tToTon = 1.102312; // short ton
        private readonly double kgToLb = 2.204624; // kg to pounds

        public MetricHandlerDecorator(ControlProductComponent control) : base(control)
        {
            this.control = new StandardHandlerDecorator(this.control);
        }

        public override void Assemble()
        {
            control.Assemble();

            int type = (int)control.parameters.getParameter("controlType");

            if (type != (int)ControlTypes.label && type != (int)ControlTypes.longLabel)
            {
                TextBox element = (TextBox)GetControl();

                element.IsEnabled = true;
                element.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(OnTextChanged));

            }
        }

        public override void Dissassemble()
        {
            base.Dissassemble();

            TextBox element = (TextBox)GetControl();
            element.RemoveHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(OnTextChanged));
        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool flag = HandlingFlag.GetInstance().flag;

                if (!flag) { return; }

                TextBox senderControl = (TextBox)sender;

                if (!senderControl.IsEnabled) { return; }

                TextBox imperialTextBox = default(TextBox);

                controls = collectionSingleton.controlCollection;

                double metricValue = 0;
                double ImperialValue = 0;

                if (senderControl.Name.ToLower().Contains("_mm_"))
                {
                    // mm to inch

                    imperialTextBox = (TextBox)(controls[senderControl.Name.Replace("_mm_", "_inch_")]).GetControl();

                    Double.TryParse(senderControl.Text, out metricValue);

                    if (metricValue != 0)
                    {
                        ImperialValue = metricValue * mmToInch;
                    }
                }
                else if (senderControl.Name.ToLower().Contains("_capacity_t_"))
                {
                    bool condition = controls.Keys.Contains(senderControl.Name.Replace("_t_", "_lbs_"));

                    if (controls.Keys.Contains(senderControl.Name.Replace("_t_", "_lbs_")))
                    {
                        // t to lbs

                        imperialTextBox = (TextBox)(controls[senderControl.Name.Replace("_t_", "_lbs_")]).GetControl();

                        Double.TryParse(senderControl.Text, out metricValue);

                        if (metricValue != 0)
                        {
                            ImperialValue = metricValue * tToLbs;
                        }
                    }
                    else if (controls.Keys.Contains(senderControl.Name.Replace("_t_", "_ton_")))
                    {
                        // t to ton

                        imperialTextBox = (TextBox)(controls[senderControl.Name.Replace("_t_", "_ton_")]).GetControl();

                        Double.TryParse(senderControl.Text, out metricValue);

                        if (metricValue != 0)
                        {
                            ImperialValue = metricValue * tToTon;
                        }
                    }
                }
                else if (senderControl.Name.ToLower().Contains("_capcity_kg_"))
                {
                    if (controls.Keys.Contains(senderControl.Name.Replace("_kg_", "_lb_")))
                    {
                        // kg to lb

                        imperialTextBox = (TextBox)(controls[senderControl.Name.Replace("_kg_", "_lb_")]).GetControl();

                        Double.TryParse(senderControl.Text, out metricValue);

                        if (metricValue != 0)
                        {
                            ImperialValue = metricValue * kgToLb;
                        }
                    }
                }

                imperialTextBox.Text = ImperialValue.ToString("0.0");
            }
            catch(Exception ex)
            {
                Debug.Print("Error on OnTextChanged (Metric): ...");
                Debug.Print(ex.ToString());
            }

            

        }
    }
}
