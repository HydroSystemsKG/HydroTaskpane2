using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroTaskpane2.Fabrication
{
    public abstract class Factory
    {
        public IControlProduct Create(FactoryParameters parameters)
        {
            return CreateProduct(parameters);
        }

        public abstract IControlProduct CreateProduct(FactoryParameters parameters);
    }
}
