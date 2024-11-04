using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HydroTaskpane2.Fabrication
{
    public class ControlProduct : ControlProductComponent, IControlProduct
    {
        public ControlProduct(FactoryParameters parameters)
        {
            this.parameters = parameters;
            this.strategy = parameters.strategy;
        }

        public override void Assemble()
        {
            strategy.Assemble(parameters);
        }

        public override UIElement GetControl()
        {
            return strategy.control;
        }

        public override void Clear()
        {
            strategy.Clear();
        }
    }
}
