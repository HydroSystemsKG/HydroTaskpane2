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
using HydroTaskpane2.Custom_Controls;

namespace HydroTaskpane2.Decorators
{
    public class MaterialHandlerDecorator : HandlerDecorator, IControlProduct
    {
        private string text;

        public MaterialHandlerDecorator(ControlProductComponent control) : base(control)
        {
            this.control = new StandardHandlerDecorator(this.control);
            this.text = "";
        }

        public override void Assemble()
        {
            control.Assemble();

            int type = (int)control.parameters.getParameter("controlType");

            if (type != (int)ControlTypes.label && type != (int)ControlTypes.longLabel)
            {
                ComboBox element = (ComboBox)GetControl();

                element.LostFocus += new RoutedEventHandler(OnLostFocus);
                element.GotFocus += new RoutedEventHandler(OnGotFocus);
            }
        }

        public override void Dissassemble()
        {
            base.Dissassemble();

            ComboBox element = (ComboBox)GetControl();

            element.LostFocus -= new RoutedEventHandler(OnLostFocus);
            element.GotFocus -= new RoutedEventHandler(OnGotFocus);
        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            ComboBox senderControl = (ComboBox)sender;
            Debug.Print($"current text: {text}; sender text: {senderControl.Text}");


            if (text == senderControl.Text && !string.IsNullOrWhiteSpace(text)) { return; }

            controls = collectionSingleton.controlCollection;

            try
            {
                TextBox tensileStrengthBox = (TextBox)(controls[controls.Keys.Where(k => new string[] { "tensile", "textbox" }.All(k.ToLower().Contains)).ToArray()[0]]).GetControl();
                TextBox yieldStrengthBox = (TextBox)(controls[controls.Keys.Where(k => new string[] { "yield", "textbox" }.All(k.ToLower().Contains)).ToArray()[0]]).GetControl();
                TextBox elongationBox = (TextBox)(controls[controls.Keys.Where(k => new string[] { "elongation", "textbox" }.All(k.ToLower().Contains)).ToArray()[0]]).GetControl();

                if (string.IsNullOrWhiteSpace(senderControl.Text))
                {
                    tensileStrengthBox.Text = "";
                    yieldStrengthBox.Text = "";
                    elongationBox.Text = "";

                    return;
                }

                double tensile = 0;
                double yield = 0;
                double elongation = 0;

                SetStrength window = new SetStrength();
                var result = window.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    tensile = window.tensile;
                    yield = window.yield;
                    elongation = window.elongation;
                }

                if (tensile != 0)
                {
                    // enable and set - Tensile strength
                    //tensileStrengthBox.IsEnabled = true;
                    tensileStrengthBox.Text = tensile.ToString();
                }

                if (yield != 0)
                {
                    // enable and set - Yield strength
                    //yieldStrengthBox.IsEnabled = true;
                    yieldStrengthBox.Text = yield.ToString();
                }

                if (elongation != 0)
                {
                    elongationBox.Text = elongation.ToString();
                }

                text = senderControl.Text;

            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            ComboBox senderControl = (ComboBox)sender;

            text = senderControl.Text;
        }

    }
}
