using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;
using HydroTaskpane2.Converters;

namespace HydroTaskpane2.Custom_Controls
{
    class TabContent
    {
        public StackPanel stackPanel { get; private set; }
        public ListBox listBox { get; private set; }

        public TabContent(ListBox listBox)
        {
            this.listBox = listBox;
        }

        public void CustomListBoxInit(string name, Style style)
        {
            this.listBox.Name = $"{name}_listBox";
            this.listBox.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#F7F7F7"); // "#F7F7F7"

            this.stackPanel = new StackPanel();
            this.stackPanel.Name = $"{name}_stackPanel";

            buildContent(style);
        }

        private void buildContent(Style style)
        {
            // set ListBox general settings

            ControlTemplate template = new ControlTemplate(typeof(ListBox));
            template.VisualTree = new FrameworkElementFactory(typeof(ItemsPresenter));

            this.listBox.Template = template;
            this.listBox.ItemContainerStyle = style;

            // set and add StackPanel to ListBox

            stackPanel.Orientation = Orientation.Vertical;

            Binding binding = new Binding("ActualWidth")
            {
                Source = this.listBox,
                Converter = new PercentageConverter(),
                ConverterParameter = "0.9"
            };

            stackPanel.SetBinding(StackPanel.WidthProperty, binding);

            stackPanel.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#F7F7F7");

            listBox.Items.Add(stackPanel);
            listBox.UpdateLayout();
        }
    }
}
