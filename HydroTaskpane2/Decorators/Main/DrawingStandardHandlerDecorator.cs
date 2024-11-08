using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HydroTaskpane2.Fabrication;
using HydroTaskpane2.References;

namespace HydroTaskpane2.Decorators.Main
{
    public class DrawingStandardHandlerDecorator : HandlerDecorator, IControlProduct
    {
        public DrawingStandardHandlerDecorator(ControlProductComponent control) : base(control)
        {
            this.control = new StandardHandlerDecorator(this.control);
        }

        public override void Assemble()
        {
            control.Assemble();

            int type = (int)control.parameters.getParameter("controlType");

            if (type != (int)ControlTypes.label && type != (int)ControlTypes.longLabel)
            {
                UIElement element = (UIElement)GetControl();
                string name = (string)control.parameters.getParameter("name");

                if (!name.ToLower().Contains("occurs") && !name.ToLower().Contains("changedescription"))
                {
                    element.IsEnabled = false;
                }
            }
        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }

    }
}
