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
using HydroTaskpane2.References.Flags;
using HydroTaskpane2.SWAttributeObserver;
using HydroSolidworksLibrary;
using SldWorks;

namespace HydroTaskpane2.Decorators.Main
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

            switch (type)
            {
                case ((int)ControlTypes.comboBox):
                    ComboBox comboBox = (ComboBox) GetControl();
                    comboBox.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(OnTextChanged));
                    break;
                case ((int)ControlTypes.textBox):
                    TextBox textBox = (TextBox)GetControl();
                    textBox.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(OnTextChanged));
                    break;
                case ((int)ControlTypes.checkBox):
                    CheckBox checkBox = (CheckBox)GetControl();
                    checkBox.Checked += new RoutedEventHandler(OnCheckChange);
                    checkBox.Unchecked += new RoutedEventHandler(OnCheckChange);
                    break;
            }
            

        }

        public override void Dissassemble()
        {
            base.Dissassemble();

            int type = (int)control.parameters.getParameter("controlType");

            switch (type)
            {
                case ((int)ControlTypes.comboBox):
                    ComboBox comboBox = (ComboBox)GetControl();
                    comboBox.RemoveHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(OnTextChanged));
                    break;
                case ((int)ControlTypes.textBox):
                    TextBox textBox = (TextBox)GetControl();
                    textBox.RemoveHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(OnTextChanged));
                    break;
                case ((int)ControlTypes.checkBox):
                    CheckBox checkBox = (CheckBox)GetControl();
                    checkBox.Checked -= new RoutedEventHandler(OnCheckChange);
                    checkBox.Unchecked -= new RoutedEventHandler(OnCheckChange);
                    break;
            }
        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }

        #region specific handlers

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SetControlAttributes(sender, null);
        }

        private void OnCheckChange(object sender, RoutedEventArgs e)
        {
            SetControlAttributes(sender, null);
        }

        #endregion

        private void SetControlAttributes(object sender, RoutedEventArgs e)
        {
            bool flag = HandlingFlag.GetInstance().flag;

            if (!flag) { return; }

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
                Debug.Print($"Standard Handler Decorator - CONTROL: |{name}|; CONTENT: |{content}|");
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
