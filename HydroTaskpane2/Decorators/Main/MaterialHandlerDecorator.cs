using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using HydroTaskpane2.Connectors;
using HydroTaskpane2.Fabrication;
using HydroTaskpane2.References;
using HydroTaskpane2.Custom_Controls;
using HydroTaskpane2.SWAttributeObserver;
using HydroSolidworksLibrary;
using SldWorks;
using SwConst;

namespace HydroTaskpane2.Decorators.Main
{
    public class MaterialHandlerDecorator : HandlerDecorator, IControlProduct
    {
        private Dictionary<string, string> materialdensities;
        private readonly double kgToLb = 2.204624; // kg to pounds
        private string text;

        public MaterialHandlerDecorator(ControlProductComponent control) : base(control)
        {
            this.control = new StandardHandlerDecorator(this.control);
            this.materialdensities = new Dictionary<string, string>();
            this.text = "";
        }

        public override void Assemble()
        {
            control.Assemble();

            int type = (int)control.parameters.getParameter("controlType");

            if (type != (int)ControlTypes.label)
            {
                ComboBox element = (ComboBox)GetControl();

                FillDictionary(ref materialdensities, SldWorksConstants.filename_part_materialdensities);

                element.LostFocus += new RoutedEventHandler(OnLostFocus);
                element.GotFocus += new RoutedEventHandler(OnGotFocus);
                
                element.LostFocus += new RoutedEventHandler(OnLostFocus_Weight);
            }
        }

        public override void Dissassemble()
        {
            base.Dissassemble();

            ComboBox element = (ComboBox)GetControl();

            element.LostFocus -= new RoutedEventHandler(OnLostFocus);
            element.GotFocus -= new RoutedEventHandler(OnGotFocus);

            element.LostFocus -= new RoutedEventHandler(OnLostFocus_Weight);
        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            ComboBox senderControl = (ComboBox)sender;
            Debug.Print($"current text: {text}; sender text: {senderControl.Text}");


            if (text == senderControl.Text && !string.IsNullOrWhiteSpace(text)) { return; }

            controls = collectionSingleton.controlCollection;

            try
            {
                TextBox tensileStrengthBox = (TextBox)(controls[controls.Keys.Where(k => new string[] { "tensile", "textbox" }.All(k.ToLower().Contains)).ToArray()[0]]).GetControl();
                TextBox yieldStrengthBox = (TextBox)(controls[controls.Keys.Where(k => new string[] { "yield", "textbox" }.All(k.ToLower().Contains)).ToArray()[0]]).GetControl();
                TextBox elongationBox = (TextBox)(controls[controls.Keys.Where(k => new string[] { "elongation", "textbox" }.All(k.ToLower().Contains)).ToArray()[0]]).GetControl();

                if (string.IsNullOrWhiteSpace(senderControl.Text))
                {
                    tensileStrengthBox.Text = "";
                    yieldStrengthBox.Text = "";
                    elongationBox.Text = "";

                    return;
                }

                double tensile = 0;
                double yield = 0;
                double elongation = 0;

                if (!senderControl.Text.ToLower().Contains("verguetungsstahl")) { return; }

                SetStrength window = new SetStrength();
                var result = window.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    tensile = window.tensile;
                    yield = window.yield;
                    elongation = window.elongation;
                }

                if (tensile != 0)
                {
                    // enable and set - Tensile strength
                    //tensileStrengthBox.IsEnabled = true;
                    tensileStrengthBox.Text = tensile.ToString();
                }

                if (yield != 0)
                {
                    // enable and set - Yield strength
                    //yieldStrengthBox.IsEnabled = true;
                    yieldStrengthBox.Text = yield.ToString();
                }

                if (elongation != 0)
                {
                    elongationBox.Text = elongation.ToString();
                }

                text = senderControl.Text;

            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            ComboBox senderControl = (ComboBox)sender;

            text = senderControl.Text;
        }

        private void OnLostFocus_Weight(object sender, RoutedEventArgs e)
        {
            ComboBox senderControl = (ComboBox)sender;
            string material = ReadMultipleDropdown(senderControl.Text, " | ", 0);

            if (!string.IsNullOrWhiteSpace(material))
            {
                SldWorks.SldWorks swApp = SWModelConnector.GetInstance().swApp;
                double weight;

                Debug.Print(" :: Hydro Taskpane :: " + senderControl.Name + " :: MyComboBox LostFocusEvent SetMaterial :: start...");
                Debug.Print(" :: Hydro Taskpane :: " + senderControl.Name + " :: MyComboBox LostFocusEvent SetMaterial :: senderBox Text: " + senderControl.Text + "...");

                if (string.IsNullOrWhiteSpace(ReadMultipleDropdown(senderControl.Text, " | ", 0)))
                {
                    Debug.Print(" :: Hydro Taskpane :: " + senderControl.Name + " :: MyComboBox LostFocusEvent SetMaterial :: no material definition found, nothing to do...");
                }
                else
                {
                    Debug.Print(" :: Hydro Taskpane :: " + senderControl.Name + " :: MyComboBox LostFocusEvent SetMaterial :: material set to " + ReadMultipleDropdown(senderControl.Text, " | ", 0) + "...");

                    if (ReadMultipleDropdown(senderControl.Text, " | ", 0).Equals("n/a"))
                    {
                        SetMaterialWeight setWeight = new SetMaterialWeight();
                        var result = setWeight.ShowDialog();

                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            weight = setWeight.weight;
                            SldWorksStandards.SetWeight(ref swApp, weight);
                        }
                        else
                        {
                            SldWorksStandards.SetWeight(ref swApp, 0);
                        }
                    }
                    else
                        UpdateWeight(ReadMultipleDropdown(senderControl.Text, " | ", 0));
                }

                // SetMaterial(ReadMultipleDropdown(senderBox.Text, delimiter, 0), ReadMultipleDropdown(senderBox.Text, delimiter, 1));

                string weightString = SldWorksStandards.GetWeight(ref swApp);
                setWeightLabel(weightString);

                senderControl.IsDropDownOpen = false;

                Debug.Print(" :: Hydro Taskpane :: " + senderControl.Name + " :: MyComboBox LostFocusEvent SetMaterial :: ...done.");
                Debug.Print(" :: Hydro Taskpane :: ");
            }
        }

        private string ReadMultipleDropdown(string text, string delimiter, int index)
        {
            List<string> s = new List<string>(text.Split(new string[] { delimiter }, StringSplitOptions.None));

            return s[index];
        }

        #region weight methods

        private void UpdateWeight(string material)
        {
            SldWorks.SldWorks swApp = SWModelConnector.GetInstance().swApp;
            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;

            int density = 0;
            int density_default = 7860;
            string density_str = null;
            bool usedefault = false;
            bool fileonnetwork = false;

            Debug.Print(" :: Hydro Taskpane :: UpdateWeight :: try to get density for " + material + "...");

            bool check = materialdensities.TryGetValue(material, out density_str);

            if (check)
            {
                Debug.Print(" :: Hydro Taskpane :: UpdateWeight :: density for " + material + " found...");

                if (Int32.TryParse(density_str, out density))
                {
                    Debug.Print(" :: Hydro Taskpane :: UpdateWeight :: density for " + material + " successfully converted to int...");
                }
                else
                {
                    swApp.SendMsgToUser2("Failed to convert density for " + material + ". Please contact cad@hydro.aero and attach a copy of this message. Falling back to default.\nAttention: Weight calculation might be wrong.", (int)swMessageBoxIcon_e.swMbStop, (int)swMessageBoxBtn_e.swMbOk);
                    usedefault = true;
                }
            }
            else
            {
                // reload dictionary in case a change was made in the meantime

                Debug.Print(" :: Hydro Taskpane :: UpdateWeight :: density for " + material + " not found, try to update database...");

                if (File.Exists(SldWorksConstants.filename_part_materialdensities_server))
                {
                    fileonnetwork = true;

                    FillDictionary(ref materialdensities, SldWorksConstants.filename_part_materialdensities_server);

                    check = materialdensities.TryGetValue(material, out density_str);

                    if (check)
                    {
                        Debug.Print(" :: Hydro Taskpane :: UpdateWeight :: density for " + material + " found on 2nd attempt...");

                        if (Int32.TryParse(density_str, out density))
                        {
                            Debug.Print(" :: Hydro Taskpane :: UpdateWeight :: density for " + material + " successfully converted to int...");
                        }
                        else
                        {
                            swApp.SendMsgToUser2("Failed to convert density for " + material + ". Please contact cad@hydro.aero and attach a copy of this message. Falling back to default.\nAttention: Weight calculation might be wrong.", (int)swMessageBoxIcon_e.swMbStop, (int)swMessageBoxBtn_e.swMbOk);
                            usedefault = true;
                        }
                    }
                    else
                    {
                        Debug.Print(" :: Hydro Taskpane :: UpdateWeight :: density for " + material + " not found on 2nd attempt...");
                    }
                }
                else
                {
                    Debug.Print(" :: Hydro Taskpane :: UpdateWeight :: updating data base not possible, file " + SldWorksConstants.filename_part_materialdensities_server + " not available...");
                }


                Debug.Print(" :: Hydro Taskpane :: UpdateWeight :: density for " + material + " not found...ask user...");

                SetMaterialDensity form1 = new SetMaterialDensity(material);
                var result = form1.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    density = form1.density;

                    Debug.Print(" :: Hydro Taskpane :: UpdateWeight :: density for " + material + " retrieved from user: " + density + "...");

                    if (fileonnetwork)
                    {
                        // add value to database without asking user
                        File.AppendAllText(SldWorksConstants.filename_part_materialdensities_server, "\n" + material + ";" + density.ToString());
                        materialdensities.Add(material, density.ToString());
                    }
                }
                else
                {
                    usedefault = true;
                }

            }

            if (usedefault)
            {
                swModel.Extension.SetUserPreferenceDouble((int)swUserPreferenceDoubleValue_e.swMaterialPropertyDensity, 0, density_default);
                Debug.Print(" :: Hydro Taskpane :: UpdateWeight :: density for " + material + " set to " + density_default + "...");
            }
            else
            {
                swModel.Extension.SetUserPreferenceDouble((int)swUserPreferenceDoubleValue_e.swMaterialPropertyDensity, 0, density);
                Debug.Print(" :: Hydro Taskpane :: UpdateWeight :: density for " + material + " set to " + density + "...");
            }

            Debug.Print(" :: Hydro Taskpane :: UpdateWeight :: update weigth...");

            string weightString = SldWorksStandards.GetWeight(ref swApp);
            setWeightLabel(weightString);
        }

        private void setWeightLabel(string weight)
        {
            DockPanel weightStack = (DockPanel)(controls[controls.Keys.Where(k => new string[] { "_weight_", "stack" }.All(k.ToLower().Contains)).ToArray()[0]]).GetControl();

            Label valueLabel = (Label)weightStack.Children[1];

            valueLabel.Content = weight;

            // update dimension attributes - metric

            string name = weightStack.Name;
            string content = (string)valueLabel.Content;

            if (!string.IsNullOrEmpty(content))
            {
                Debug.Print($"Weight Update - CONTROL: |{name}|; CONTENT: |{content}|");
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

            weightStack.Children[1] = valueLabel;
            weightStack.UpdateLayout();

            setImperialWeightLabel(weight);

            SWModelConnector.GetInstance().swModel.ForceRebuild3(true);
        }

        private void setImperialWeightLabel(string weight)
        {
            string name;
            string content;
            
            try
            {
                DockPanel impWeightStack = (DockPanel)(controls[controls.Keys.Where(k => new string[] { "_imperialweight_", "stack" }.All(k.ToLower().Contains)).ToArray()[0]]).GetControl();

                // update dimension attributes - imperial
                double impWeight;
                Double.TryParse(weight, out impWeight);

                string impContent = (impWeight * kgToLb).ToString("0.000");

                if (impWeight == 0)
                {
                    impContent = "-";
                }

                Label impValueLabel = (Label)impWeightStack.Children[1];
                impValueLabel.Content = impContent;

                name = impWeightStack.Name;
                content = impContent;

                if (!string.IsNullOrEmpty(content))
                {
                    Debug.Print($"Imperial Weight Update - CONTROL: |{name}|; CONTENT: |{content}|");
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

                impWeightStack.Children[1] = impValueLabel;
                impWeightStack.UpdateLayout();
            }
            catch (Exception e)
            {
                Debug.Print($"Set Weight Label error: {e.ToString()}");
            }
        }

        #endregion

        #region fill density dict

        private void FillDictionary(ref Dictionary<string, string> dict, string filename)
        {
            FillDictionary(ref dict, filename, false);
        }

        private void FillDictionary(ref Dictionary<string, string> dict, string filename, bool inverse)
        {

            string[] dict_content = null;

            try
            {
                dict_content = File.ReadAllLines(filename);
            }
            catch (Exception e)
            {
                Debug.Print(" :: Hydro Taskpane :: Error creating dictionary for file " + filename + ": " + e.Message);
            }

            foreach (var line in dict_content)
            {
                try
                {
                    if (inverse)
                        dict.Add(line.Split(';')[1], line.Split(';')[0]);
                    else
                        dict.Add(line.Split(';')[0], line.Split(';')[1]);
                }
                catch (Exception e)
                {
                    Debug.Print(" :: Hydro Taskpane :: Error creating dictionary in line " + line + ": " + e.Message);
                    continue;
                }
            }
        }

        #endregion
    }
}
