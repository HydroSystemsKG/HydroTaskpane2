using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HydroTaskpane2.Strategy;

namespace HydroTaskpane2.Fabrication
{
    public abstract class ControlProductComponent
    {
        public FactoryParameters parameters;
        public IProductStrategy strategy;

        public virtual void Assemble()
        {

        }

        public abstract UIElement GetControl();
    }
}
