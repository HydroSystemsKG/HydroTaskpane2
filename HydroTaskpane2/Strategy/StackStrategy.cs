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
    public class StackStrategy : IProductStrategy
    {
        private readonly ControlCollectionSingleton collectionSingleton;
        private Dictionary<string, IControlProduct> controls;

        public UIElement control { get; private set; }

        public StackStrategy()
        {
            this.collectionSingleton = ControlCollectionSingleton.GetInstance();
        }

        public void Assemble(FactoryParameters parameters)
        {
            controls = collectionSingleton.controlCollection;
            int controlType = (int)parameters.getParameter("controlType");

            AssembleUIElement(parameters);

            DockPanel stackPanel = (DockPanel)control;

            // add handlers (if necessary)
        }

        public void AssembleUIElement(FactoryParameters parameters)
        {
            DockPanel stackPanel = new DockPanel();
            stackPanel.Name = (string)parameters.getParameter("name");
            stackPanel.Height = (int)parameters.getParameter("height") + 5;

            //stackPanel.Orientation = Orientation.Horizontal;

            // title label
            Label titleLabel = new Label();
            titleLabel.Content = (string)parameters.getParameter("standardValue");
            titleLabel.Height = (int)parameters.getParameter("height");

            titleLabel.HorizontalContentAlignment = HorizontalAlignment.Left;

            stackPanel.Children.Add(titleLabel);

            // value label
            Label valueLabel = new Label();
            valueLabel.Content = "";
            valueLabel.Height = (int)parameters.getParameter("height");
            
            valueLabel.HorizontalContentAlignment = HorizontalAlignment.Right;

            stackPanel.Children.Add(valueLabel);
            stackPanel.UpdateLayout();

            control = (UIElement)stackPanel;
        }

        public void Clear()
        {
            DockPanel stackPanel = (DockPanel)control;
            stackPanel.Children.Clear();
            stackPanel.UpdateLayout();

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
