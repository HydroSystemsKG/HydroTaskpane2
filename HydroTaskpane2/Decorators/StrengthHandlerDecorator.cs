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

namespace HydroTaskpane2.Decorators
{
    class StrengthHandlerDecorator : HandlerDecorator, IControlProduct
    {
        public StrengthHandlerDecorator(ControlProductComponent control) : base(control)
        {
            this.control = new StandardHandlerDecorator(this.control);
        }

        public override void Assemble()
        {
            control.Assemble();

            int type = (int)control.parameters.getParameter("controlType");

            if (type != (int)ControlTypes.label && type != (int)ControlTypes.longLabel)
            {
                TextBox element = (TextBox)GetControl();

                element.LostFocus += new RoutedEventHandler(OnLostFocus);
                element.TextChanged += new TextChangedEventHandler(OnTextChanged);

                element.IsEnabled = false;
            }
        }

        public override void Dissassemble()
        {
            base.Dissassemble();

            TextBox element = (TextBox)GetControl();

            element.LostFocus -= new RoutedEventHandler(OnLostFocus);
            element.TextChanged -= new TextChangedEventHandler(OnTextChanged);

        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            controls = collectionSingleton.controlCollection;

            TextBox textBox = (TextBox)sender;

            if (textBox.Text != "")
            {
                bool letterInString = textBox.Text.Any(x => char.IsLetter(x));

                if (letterInString)
                {
                    textBox.Text = "";
                }
            }

        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            bool flag = HandlingFlag.GetInstance().flag;

            if (!flag) { return; }

            TextBox senderControl = (TextBox)sender;

            string name = senderControl.Name;
            string content = senderControl.Text;

            if (!string.IsNullOrEmpty(content))
            {
                Debug.Print($"CONTROL: |{name}|; CONTENT: |{content}|");
                UpdatePublisher publisher = new UpdatePublisher();
                publisher.Update(name, content);

                publisher = null;
            }

            SWModelConnector.GetInstance().swModel.ForceRebuild3(true);
        }

    }
}
