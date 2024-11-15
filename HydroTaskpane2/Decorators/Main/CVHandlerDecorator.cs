﻿using System;
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

            if (type != (int)ControlTypes.label)
            {
                ComboBox element = (ComboBox)GetControl();

                element.IsEnabled = false;
                
            }
        }

        public override void Dissassemble()
        {
            base.Dissassemble();
        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }
        
    }
}
