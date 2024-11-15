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


namespace HydroTaskpane2.Strategy
{
    public class LabelStrategy : IProductStrategy
    {
        private readonly ControlCollectionSingleton collectionSingleton;
        private Dictionary<string, IControlProduct> controls;

        public UIElement control { get; private set; }

        public LabelStrategy()
        {
            this.collectionSingleton = ControlCollectionSingleton.GetInstance();
        }

        public void Assemble(FactoryParameters parameters)
        {
            controls = collectionSingleton.controlCollection;
            int controlType = (int)parameters.getParameter("controlType");

            AssembleUIElement(parameters);

            Label label = (Label)control;

            // add handlers (if necessary)
        }

        public void AssembleUIElement(FactoryParameters parameters)
        {
            Label label = new Label();

            label.Name = (string)parameters.getParameter("name");
            label.Content = (string)parameters.getParameter("standardValue");
            label.Height = (int)parameters.getParameter("height");

            control = (UIElement)label;
        }

        public void Clear()
        {
            return;
        }

        // get control from Singleton

        private ControlProductComponent getCollectionControl(string key)
        {
            ControlProductComponent product = (ControlProductComponent)controls[key];

            if (product.GetControl() == null)
            {
                product.Assemble();
            }

            return product;
        }

    }
}
