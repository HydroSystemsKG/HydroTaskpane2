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

namespace HydroTaskpane2.Decorators
{
    public class CVHandlerDecorator : HandlerDecorator, IControlProduct
    {

        public CVHandlerDecorator(ControlProductComponent control) : base(control)
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

                element.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(OnTextChanged));
            }
        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox senderControl = (ComboBox)sender;

            if (senderControl.Text != (string)control.parameters.getParameter("standardValue"))
            {
                senderControl.IsEnabled = true;
            }
            else
            {
                senderControl.IsEnabled = false;
            }
        }

    }
}
