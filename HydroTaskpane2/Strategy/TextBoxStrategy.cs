using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HydroTaskpane2.Fabrication;
using HydroTaskpane2.References;

namespace HydroTaskpane2.Strategy
{
    public class TextBoxStrategy : IProductStrategy
    {
        private readonly ControlCollectionSingleton collectionSingleton;
        private Dictionary<string, IControlProduct> controls;

        public UIElement control { get; private set; }

        public TextBoxStrategy()
        {
            this.collectionSingleton = ControlCollectionSingleton.GetInstance();
        }

        public void Assemble(FactoryParameters parameters)
        {
            controls = collectionSingleton.controlCollection;
            int controlType = (int)parameters.getParameter("controlType");

            AssembleUIElement(parameters);

            TextBox textBox = (TextBox)control;

            // add handlers (if necessary)
        }

        public void AssembleUIElement(FactoryParameters parameters)
        {
            TextBox textBox = new TextBox();
            textBox.VerticalContentAlignment = VerticalAlignment.Center;

            textBox.Name = (string)parameters.getParameter("name");
            textBox.Text = (string)parameters.getParameter("standardValue");
            textBox.Height = (int)parameters.getParameter("height");

            control = (UIElement)textBox;
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
