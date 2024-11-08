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

namespace HydroTaskpane2.Decorators.Main
{
    public class WeldmentHandlerDecorator : HandlerDecorator, IControlProduct
    {
        public WeldmentHandlerDecorator(ControlProductComponent control) : base(control)
        {
            this.control = new StandardHandlerDecorator(this.control);
        }

        public override void Assemble()
        {
            control.Assemble();

            int type = (int)control.parameters.getParameter("controlType");

            if (type != (int)ControlTypes.label && type != (int)ControlTypes.longLabel)
            {
                CheckBox element = (CheckBox)GetControl();

                element.Checked += new RoutedEventHandler(OnChecked);
                element.Unchecked += new RoutedEventHandler(OnUnchecked);

            }
        }

        public override void Dissassemble()
        {
            base.Dissassemble();

            CheckBox element = (CheckBox)GetControl();

            element.Checked -= new RoutedEventHandler(OnChecked);
            element.Unchecked -= new RoutedEventHandler(OnUnchecked);

        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            ChangeCheck(sender, true);

        }

        private void OnUnchecked(object sender, RoutedEventArgs e)
        {
            ChangeCheck(sender, false);
        }

        private void ChangeCheck(object sender, bool check)
        {
            controls = collectionSingleton.controlCollection;

            ComboBox dyeComboBox = (ComboBox)(controls[controls.Keys.Where(k => new string[] { "dyepenetration", "dropdown" }.All(k.ToLower().Contains)).ToArray()[0]]).GetControl();
            ComboBox visualComboBox = (ComboBox)(controls[controls.Keys.Where(k => new string[] { "visualinspection", "dropdown" }.All(k.ToLower().Contains)).ToArray()[0]]).GetControl();
            ComboBox qualityComboBox = (ComboBox)(controls[controls.Keys.Where(k => new string[] { "qualityimperfections", "dropdown" }.All(k.ToLower().Contains)).ToArray()[0]]).GetControl();

            CheckBox senderControl = (CheckBox)sender;

            if (check)
            {
                dyeComboBox.Text = "not required  |  nicht erforderlich";
                visualComboBox.Text = "not required  |  nicht erforderlich";
                qualityComboBox.Text = "DIN EN ISO 5817 -C-";

                dyeComboBox.IsEnabled = true;
                visualComboBox.IsEnabled = true;
                qualityComboBox.IsEnabled = true;
            }
            else
            {
                dyeComboBox.Text = "";
                visualComboBox.Text = "";
                qualityComboBox.Text = "";

                dyeComboBox.IsEnabled = false;
                visualComboBox.IsEnabled = false;
                qualityComboBox.IsEnabled = false;
            }
        }

    }
}
