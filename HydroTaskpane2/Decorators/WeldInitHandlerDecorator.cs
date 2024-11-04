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
    public class WeldInitHandlerDecorator : HandlerDecorator, IControlProduct
    {
        public WeldInitHandlerDecorator(ControlProductComponent control) : base(control)
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

                element.Initialized += new EventHandler(OnInitialize);

            }
        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }

        public void OnInitialize(object sender, EventArgs e)
        {
            ComboBox senderControl = (ComboBox)sender;

            senderControl.IsEnabled = false;
        }
    }
}
