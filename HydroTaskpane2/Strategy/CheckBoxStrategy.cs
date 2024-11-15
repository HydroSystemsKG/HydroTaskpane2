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
    public class CheckBoxStrategy : IProductStrategy
    {
        private readonly ControlCollectionSingleton collectionSingleton;
        private Dictionary<string, IControlProduct> controls;

        public UIElement control { get; private set; }

        public CheckBoxStrategy()
        {
            this.collectionSingleton = ControlCollectionSingleton.GetInstance();
        }

        public void Assemble(FactoryParameters parameters)
        {
            controls = collectionSingleton.controlCollection;
            int controlType = (int)parameters.getParameter("controlType");

            AssembleUIElement(parameters);

            CheckBox checkBox = (CheckBox)control;
        }

        public void AssembleUIElement(FactoryParameters parameters)
        {
            CheckBox checkBox = new CheckBox();

            checkBox.Name = (string)parameters.getParameter("name");
            checkBox.IsChecked = false;
            checkBox.Content = parameters.getParameter("standardValue");
            checkBox.Height = (int)parameters.getParameter("height");

            control = (UIElement)checkBox;
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

        public void Clear()
        {
            CheckBox checkBox = (CheckBox)control;

            checkBox.IsChecked = false;
        }

    }
}
