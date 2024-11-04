using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using HydroTaskpane2.Connectors;
using HydroTaskpane2.Fabrication;
using HydroTaskpane2.References;
using HydroTaskpane2.SWAttributeObserver;
using HydroSolidworksLibrary;
using SldWorks;

namespace HydroTaskpane2.Decorators
{
    public class StandardHandlerDecorator : HandlerDecorator, IControlProduct
    {
        public StandardHandlerDecorator(ControlProductComponent control) : base(control)
        {

        }

        public override void Assemble()
        {
            control.Assemble();

            int type = (int)control.parameters.getParameter("controlType");

            if (type != (int)ControlTypes.label && type != (int)ControlTypes.longLabel)
            {
                UIElement element = GetControl();

                element.LostFocus += new RoutedEventHandler(OnLostFocus);
            }
        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            string name = "";
            string content = "";

            // if sender is different than checkbox

            if (sender is CheckBox)
            {
                CheckBox senderControl = (CheckBox)sender;

                name = senderControl.Name;
                content = senderControl.IsChecked.ToString();
            }
            else if (sender is TextBox)
            {
                TextBox senderControl = (TextBox)sender;

                name = senderControl.Name;
                content = senderControl.Text;
            }
            else if (sender is ComboBox)
            {
                ComboBox senderControl = (ComboBox)sender;

                name = senderControl.Name;
                content = senderControl.Text;
            }

            if (!string.IsNullOrEmpty(content))
            {
                Debug.Print($"CONTROL: |{name}|; CONTENT: |{content}|");
                UpdatePublisher publisher = new UpdatePublisher();
                publisher.Update(name, content);

                publisher = null;
            }
            else
            {
                UpdatePublisher publisher = new UpdatePublisher();
                publisher.Update(name, "");

                publisher = null;
            }

            SWModelConnector.GetInstance().swModel.ForceRebuild3(true);
        }

    }
}
