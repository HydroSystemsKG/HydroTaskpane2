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
    public class ImperialHandlerDecorator : HandlerDecorator, IControlProduct
    {
        private readonly double inchTomm = 1.0 / 0.039370; // inch to mm
        private readonly double LbsToT = 1.0 / 2204.622622; // pounds to t
        private readonly double TonToT = 1.0 / 1.102312; // short ton to t
        private readonly double LbToKg = 1.0 / 2.204624; // pounds to kg

        public ImperialHandlerDecorator(ControlProductComponent control) : base(control)
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

                element.IsEnabled = false;
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

                TextBox metricTextBox = default(TextBox);

                controls = collectionSingleton.controlCollection;

                double metricValue = 0;
                double imperialValue = 0;

                if (senderControl.Name.ToLower().Contains("_inch_"))
                {
                    metricTextBox = (TextBox)(controls[senderControl.Name.Replace("_inch_", "_mm_")]).GetControl();

                    Double.TryParse(senderControl.Text, out imperialValue);

                    if (imperialValue != 0)
                    {
                        metricValue = imperialValue * inchTomm;
                    }
                }
                else if (senderControl.Name.ToLower().Contains("_capacity_lbs_"))
                {
                    if (controls.Keys.Contains(senderControl.Name.Replace("_lbs_", "_t_")))
                    {
                        // lbs to t

                        metricTextBox = (TextBox)(controls[senderControl.Name.Replace("_lbs_", "_t_")]).GetControl();

                        Double.TryParse(senderControl.Text, out imperialValue);

                        if (imperialValue != 0)
                        {
                            metricValue = imperialValue * LbsToT;
                        }
                    }
                    else if (controls.Keys.Contains(senderControl.Name.Replace("_ton_", "_t_")))
                    {
                        // ton to t

                        metricTextBox = (TextBox)(controls[senderControl.Name.Replace("_ton_", "_t_")]).GetControl();

                        Double.TryParse(senderControl.Text, out imperialValue);

                        if (imperialValue != 0)
                        {
                            metricValue = imperialValue * TonToT;
                        }
                    }
                }
                else if (senderControl.Name.ToLower().Contains("_capcity_lb_"))
                {
                    if (controls.Keys.Contains(senderControl.Name.Replace("_lb_", "_kg_")))
                    {
                        // lb to kg

                        metricTextBox = (TextBox)(controls[senderControl.Name.Replace("_lb_", "_kg_")]).GetControl();

                        Double.TryParse(senderControl.Text, out imperialValue);

                        if (imperialValue != 0)
                        {
                            metricValue = imperialValue * LbToKg;
                        }
                    }
                }

                metricTextBox.Text = metricValue.ToString("0.0");
            }
            catch(Exception ex)
            {
                Debug.Print("Error on OnTextChanged (Imperial): ...");
                Debug.Print(ex.ToString());
            }
        }
    }
}
