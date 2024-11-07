using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Diagnostics;
using HydroTaskpane2.Connectors;
using HydroTaskpane2.Fabrication;
using HydroTaskpane2.References;

namespace HydroTaskpane2.Strategy
{
    public class ComboBoxStrategy : IProductStrategy
    {
        private string[] dropdown;

        public UIElement control { get; private set; }
        private string standardValue;

        public ComboBoxStrategy()
        {

        }

        public void Assemble(FactoryParameters parameters)
        {
            int controlType = (int)parameters.getParameter("controlType");

            AssembleUIElement(parameters);

            ComboBox comboBox = (ComboBox)control;

            // add handlers (common)

            comboBox.GotFocus += new RoutedEventHandler(OnGotFocus); // not causing any trouble
            comboBox.KeyDown += new KeyEventHandler(OnKeyDown); // not causing any trouble
            comboBox.KeyUp += new KeyEventHandler(OnKeyUp); // problematic
            comboBox.LostFocus += new RoutedEventHandler(OnLostFocus); // not causing any trouble

        }

        public void AssembleUIElement(FactoryParameters parameters)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.IsEditable = true;

            comboBox.IsTextSearchEnabled = false;
            comboBox.VerticalContentAlignment = VerticalAlignment.Center;

            comboBox.Name = (string)parameters.getParameter("name");
            comboBox.Text = (string)parameters.getParameter("standardValue");
            comboBox.Height = (int)parameters.getParameter("height");

            string filepath = (string)parameters.getParameter("filePath");

            this.dropdown = File.ReadAllLines(filepath).Where(l => l.Contains(";")).Select(l => l.Replace(";", " | ")).ToArray();

            if (this.dropdown == null || this.dropdown.Length == 0)
            {
                this.dropdown = File.ReadAllLines(filepath);
            }

            foreach (string item in this.dropdown)
            {
                comboBox.Items.Add(item);
            }

            control = (UIElement)comboBox;
        }

        public void Clear()
        {
            ComboBox comboBox = (ComboBox)control;
            comboBox.Text = string.Empty;
        }

        #region event handlers

        // common handlers

        private void OnGotFocus(object sender, RoutedEventArgs e) //OK
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.Text == "Please select  |  Bitte auswählen") { comboBox.Text = ""; }

            ResetDropdown(comboBox);
            comboBox.IsDropDownOpen = true;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (!comboBox.IsDropDownOpen) { comboBox.IsDropDownOpen = true; }

        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            ComboBox senderControl = (ComboBox)sender;

            if (e.Key == Key.Enter)
            {
                if (senderControl.SelectedIndex > 0)
                {
                    senderControl.IsDropDownOpen = false;
                }
            }
            else if (e.Key != Key.Down && e.Key != Key.Up)
            {
                Debug.WriteLine(" :: Hydro Taskpane :: Event ComboBox Keypress :: update dropdown...");
                UpdateDropdown(sender); // causing interference
            }

            Debug.WriteLine(" :: Hydro Taskpane :: Event ComboBox Keypress :: ...done.");
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            ComboBox senderControl = (ComboBox)sender;

            if (sender == null || senderControl == null) { return; }

            senderControl.IsDropDownOpen = false;

        }

        #endregion

        #region additional methods

        private void UpdateDropdown(object sender)
        {
            ComboBox senderControl = (ComboBox)sender;

            try
            {
                // string[] dropdown_content = File.ReadAllLines(dropdownfiles[dropdown.Name]);

                string[] updateDropdown = dropdown.Select(i => i).Where(t => t.ToLower().Contains(senderControl.Text.ToLower())).ToArray();

                senderControl.Items.Clear();

                for (int i = 0; i < updateDropdown.Count(); i++)
                {
                    senderControl.Items.Add(updateDropdown[i]);
                }

                senderControl.Items.Add("");

                //senderControl.SelectedIndex = senderControl.Text.Length;
            }
            catch (Exception e)
            {
                Debug.Print(" ERROR: " + e.ToString());
                //senderControl.SelectedIndex = senderControl.Text.Length;
            }

        }

        private string ReadMultipleDropDown(string text, string delimiter, int index)
        {
            List<string> s = new List<string>(text.Split(new string[] { delimiter }, StringSplitOptions.None));

            return s[index];
        }

        private void ResetDropdown(object sender)
        {
            ComboBox senderControl = (ComboBox)sender;
            string text = senderControl.Text;

            try
            {
                senderControl.Items.Clear();

                foreach (var item in dropdown)
                {
                    Debug.Print(" :: Hydro Taskpane :: " + senderControl.Name + " :: MyComboBoxDropdown Reset :: processing line \"" + item + "\"...");
                    senderControl.Items.Add(item);
                }

                senderControl.Items.Add(" ");
            }
            catch (Exception e)
            {
                Debug.Print(" :: Hydro Taskpane :: " + ((ComboBox)sender).Name + " :: MyComboBoxDropdown Reset :: error filling dropdown: " + e.Message);
            }

            senderControl.Text = text;
        }

        #endregion

    }
}
