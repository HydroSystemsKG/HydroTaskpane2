using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroTaskpane2.Decorators.Reference;

namespace HydroTaskpane2.Fabrication
{
    public class ControlProductFactory : Factory
    {
        public ControlProductFactory()
        {

        }

        public override IControlProduct CreateProduct(FactoryParameters parameters)
        {
            IControlProduct product;
            Type type = null;

            try
            {
                type = DecoratorDict.decorators[(int)parameters.getParameter("decorator")];
            }
            catch { }

            if (type != null)
            {
                product = (IControlProduct)Activator.CreateInstance(type, new ControlProduct(parameters));
            }
            else
            {
                product = new ControlProduct(parameters);
            }

            return product;
        }
    }
}
