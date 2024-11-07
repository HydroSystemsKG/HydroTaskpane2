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
using SldWorks;

namespace HydroTaskpane2.Decorators
{
    public class ColorHandlerDecorator : HandlerDecorator, IControlProduct
    {
        public ColorHandlerDecorator(ControlProductComponent control) : base(control)
        {
            this.control = new StandardHandlerDecorator(this.control);
        }

        public override void Assemble()
        {
            control.Assemble();

            int type = (int)control.parameters.getParameter("controlType");

            if (type != (int)ControlTypes.label && type != (int)ControlTypes.longLabel)
            {
                ComboBox element = (ComboBox)GetControl();

                element.SelectionChanged += new SelectionChangedEventHandler(OnSelectionChanged);
                element.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(OnTextChanged));
            }
        }

        public override void Dissassemble()
        {
            base.Dissassemble();

            ComboBox element = (ComboBox)GetControl();

            element.SelectionChanged += new SelectionChangedEventHandler(OnSelectionChanged);
            element.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(OnTextChanged));

        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox senderControl = (ComboBox)sender;

            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;
            double[] matprops = swModel.MaterialPropertyValues;

            if (senderControl.SelectedItem == null) { return; }

            string item = senderControl.SelectedItem.ToString();

            if (item.ToLower().Contains("lacquered"))
            {
                if (item.ToLower().Contains("unlacquered"))
                {
                    matprops[0] = 128.0 / 256.0;
                    matprops[1] = 128.0 / 256.0;
                    matprops[2] = 128.0 / 256.0;
                }
                else
                {
                    matprops[0] = 0.0 / 256.0;
                    matprops[1] = 128.0 / 256.0;
                    matprops[2] = 255.0 / 256.0;
                }
            }
            else if (item.ToLower().Contains("gal"))
            {
                matprops[0] = 256.0 / 256.0;
                matprops[1] = 256.0 / 256.0;
                matprops[2] = 0.0 / 256.0;
            }
            else if (item.ToLower().Contains("anodized"))
            {
                matprops[0] = 256.0 / 256.0;
                matprops[1] = 0.0 / 256.0;
                matprops[2] = 0.0 / 256.0;
            }
            else
            {
                matprops[0] = 128.0 / 256.0;
                matprops[1] = 128.0 / 256.0;
                matprops[2] = 128.0 / 256.0;
            }

            swModel.MaterialPropertyValues = matprops;
            swModel.GraphicsRedraw2();

            senderControl.IsDropDownOpen = false;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            bool flag = HandlingFlag.GetInstance().flag;

            if (!flag) { return; }

            controls = collectionSingleton.controlCollection;

            try
            {
                ComboBox colorComboBox = (ComboBox)(controls[controls.Keys.Where(k => new string[] { "color", "dropdown" }.All(k.ToLower().Contains)).ToArray()[0]]).GetControl();
                ComboBox varnComboBox = (ComboBox)(controls[controls.Keys.Where(k => new string[] { "varnishing", "dropdown" }.All(k.ToLower().Contains)).ToArray()[0]]).GetControl();

                ComboBox senderControl = (ComboBox)sender;

                if (senderControl.Text.ToLower().Contains("lacquered") && !senderControl.Text.ToLower().Contains("unlacquered"))
                {
                    colorComboBox.IsEnabled = true;
                    varnComboBox.IsEnabled = true;
                }
                else
                {
                    colorComboBox.Text = (string)((ControlProductComponent)(controls[controls.Keys.Where(k => new string[] { "color", "dropdown" }.All(k.ToLower().Contains)).ToArray()[0]])).parameters.getParameter("standardValue");
                    varnComboBox.Text = (string)((ControlProductComponent)(controls[controls.Keys.Where(k => new string[] { "varnishing", "dropdown" }.All(k.ToLower().Contains)).ToArray()[0]])).parameters.getParameter("standardValue");

                    colorComboBox.IsEnabled = false;
                    varnComboBox.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
        }
    }
}
