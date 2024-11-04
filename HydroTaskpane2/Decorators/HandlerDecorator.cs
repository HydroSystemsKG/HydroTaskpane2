using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroTaskpane2.Fabrication;
using HydroTaskpane2.References;

namespace HydroTaskpane2.Decorators
{
    public abstract class HandlerDecorator : ControlProductComponent
    {
        protected ControlCollectionSingleton collectionSingleton;
        protected Dictionary<string, IControlProduct> controls;

        protected ControlProductComponent control;

        protected HandlerDecorator(ControlProductComponent control)
        {
            this.collectionSingleton = ControlCollectionSingleton.GetInstance();
            this.control = control;

            this.strategy = control.strategy;
            this.parameters = control.parameters;
        }

        // get controls from Singleton

        public ControlProductComponent getCollectionControl(string key)
        {
            ControlProductComponent product = (ControlProductComponent)controls[key];

            if (product.GetControl() == null)
            {
                product.Assemble();
            }

            return product;
        }

        public override void Clear()
        {
            base.Clear();
        }
    }
}
