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
    public class SeparatorStrategy : IProductStrategy
    {
        private readonly ControlCollectionSingleton collectionSingleton;
        private Dictionary<string, IControlProduct> controls;

        public UIElement control { get; private set; }

        public SeparatorStrategy()
        {
            this.collectionSingleton = ControlCollectionSingleton.GetInstance();
        }

        public void Assemble(FactoryParameters parameters)
        {
            controls = collectionSingleton.controlCollection;
            int controlType = (int)parameters.getParameter("controlType");

            AssembleUIElement(parameters);
        }

        public void AssembleUIElement(FactoryParameters parameters)
        {
            DockPanel dockpanel = new DockPanel();
            dockpanel.Name = (string)parameters.getParameter("name");
            dockpanel.Height = (int)parameters.getParameter("height");

            Separator separator = new Separator();

            if (!string.IsNullOrWhiteSpace((string)parameters.getParameter("standardValue")))
            {
                Label label = new Label();

                label.Content = (string)parameters.getParameter("standardValue");
                label.FontWeight = FontWeights.Bold;
                DockPanel.SetDock(label, Dock.Left);

                dockpanel.Children.Add(label);
            }
            
            dockpanel.Children.Add(separator);

            control = (UIElement)dockpanel;
        }

        public void Clear()
        {
            return;
        }

        // get control from singleton

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
