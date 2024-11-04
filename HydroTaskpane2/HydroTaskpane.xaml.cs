using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using HydroTaskpane2.References;
using HydroTaskpane2.Fabrication;
using HydroTaskpane2.Connectors;
using HydroTaskpane2.SWAttributeObserver;
using HydroTaskpane2.Custom_Controls;
using HydroTaskpane2.Decorators;
using HydroTaskpane2.SWAttributeReader;
using SldWorks;
using SwConst;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using SwCommands;
using Newtonsoft.Json;

namespace HydroTaskpane2
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    [ProgId(intProgID)]
    public partial class HydroTaskpane2_UI : UserControl
    {
        private const string intProgID = "HydroTaskpane2.Taskpane.UI";
        private double startingHeight;

        public HydroTaskpane2_UI()
        {
            InitializeComponent();

            this.startingHeight = 458.75;
        }

        #region custom init

        private void CustomInit(StackPanel stackPanel, string tabName)
        {
            // build all products
            ControlProductFactory factory = new ControlProductFactory();

            ControlCollectionSingleton controlCollection = ControlCollectionSingleton.GetInstance();
            int type = -1;

            try
            {
                SWModelConnector connector = SWModelConnector.GetInstance();
                if (connector.swModel != null)
                {
                    type = connector.swModel.GetType();
                }
            }
            catch
            {

            }

            foreach (object[] paramArray in FieldList.controlArrays)
            {
                FactoryParameters parameters = new FactoryParameters(paramArray);
                IControlProduct product;
                UIElement element = default(UIElement);

                //Debug.Print($"PARAMETER NAME: {(string)parameters.getParameter("tab")}; TAB HEADER: {tabName}; COMPARE: {((string)parameters.getParameter("tab") != tabName).ToString()}");

                if ((string)parameters.getParameter("tab") != tabName)
                {
                    continue;
                }
                else
                {

                    if (((string)parameters.getParameter("name")).ToLower().Contains("part") && type == (int)swDocumentTypes_e.swDocPART)
                    {
                        product = factory.CreateProduct(parameters);

                        product.Assemble();

                        controlCollection.controlCollection.Add((string)parameters.getParameter("name"), product);
                        element = product.GetControl();
                    }
                    else if (((string)parameters.getParameter("name")).ToLower().Contains("assembly") && type == (int)swDocumentTypes_e.swDocASSEMBLY)
                    {
                        product = factory.CreateProduct(parameters);

                        product.Assemble();

                        controlCollection.controlCollection.Add((string)parameters.getParameter("name"), product);
                        element = product.GetControl();
                    }
                    else if (((string)parameters.getParameter("name")).ToLower().Contains("drawing") && type == (int)swDocumentTypes_e.swDocDRAWING)
                    {
                        product = factory.CreateProduct(parameters);

                        product.Assemble();

                        controlCollection.controlCollection.Add((string)parameters.getParameter("name"), product);
                        element = product.GetControl();
                    }

                    Debug.Print($"Adding element...{element == null}");

                    if (element != null)
                    {
                        var children = stackPanel.Children.Cast<UIElement>();
                        Debug.Print($"Add element - {children.Count()}");
                        stackPanel.Children.Add(element);
                        Debug.Print("...Element added");
                    }
                }

            }
        }

        public void CustomTabInit()
        {
            var Properties = typeof(PageNames).GetProperties(BindingFlags.Public | BindingFlags.Static).Select(x => x.GetValue(typeof(PageNames))).ToList();

            foreach (string property in Properties)
            {
                string tab = property;

                Debug.Print(tab + $" {Properties.Count().ToString()}");

                TabItem tabItem = new TabItem
                {
                    Header = tab,
                    Name = tab.Replace(" ", "_").Replace("-", "_")
                };

                // add to tabItem content...
                TabContent content = new TabContent(new ListBox());
                Style style = FindResource("NoSelectionListBoxItemStyle") as Style;
                content.CustomListBoxInit(tabItem.Name, style);

                CustomInit(content.stackPanel, tab);

                tabItem.Content = content.listBox;
                Debug.Print(checkTab(tab).ToString());

                if (checkTab(tab))
                {
                    tbCtrl.Items.Add(tabItem);
                }

            }
        }

        private bool checkTab(string tab)
        {
            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;

            if (swModel == null) { return false; }

            if (tab == PageNames.standard)
            {
                return true;
            }
            else
            {
                switch (swModel.GetType())
                {
                    case ((int)swDocumentTypes_e.swDocASSEMBLY):
                        return false;
                    case ((int)swDocumentTypes_e.swDocPART):
                        return false;
                }

                if (swModel.GetType() == (int)swDocumentTypes_e.swDocDRAWING)
                {
                    DrawingDoc swDrawing = (DrawingDoc)swModel;

                    string templateName = ((Sheet)swDrawing.GetCurrentSheet()).GetTemplateName();
                    templateName = System.IO.Path.GetFileNameWithoutExtension(templateName);

                    if (templateName.ToLower().Split('_').All(s => tab.ToLower().Contains(s)))
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        #endregion

        #region hide content
        
        public void hide() // IT WORKS
        {
            /*
            // remove all selections + collapse treeview
            try
            {
                foreach (AttributeGroup group in AttributeGroups.Items)
                {
                    var treeViewItem = AttributeGroups.ItemContainerGenerator.ContainerFromItem(group) as TreeViewItem;
                    treeViewItem.IsExpanded = false;
                }

                foreach (AttributeGroup group in AttributeGroups.Items)
                {
                    var treeViewItem = AttributeGroups.ItemContainerGenerator.ContainerFromItem(group) as TreeViewItem;
                    treeViewItem.IsSelected = false;
                }

            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
            }
            */

            /*
            // clear all controls

            List<Field> fieldList = new List<Field>();

            foreach (AttributeGroup group in AttributeGroups.Items)
            {
                fieldList.AddRange(group.fields.ToList());

                var treeViewItem = AttributeGroups.ItemContainerGenerator.ContainerFromItem(group) as TreeViewItem;
                treeViewItem.IsEnabled = false;

                FocusManager.SetFocusedElement(FocusManager.GetFocusScope(treeViewItem), null);
                Keyboard.ClearFocus();
            }

            foreach (Field field in fieldList)
            {
                field.content = null;
            }
            */

            // clear all controls
            ControlCollectionSingleton controlCollection = ControlCollectionSingleton.GetInstance();

            foreach (string controlName in controlCollection.controlCollection.Keys)
            {
                if (!controlName.ToLower().Contains("label") && !controlName.ToLower().Contains("separator"))
                {
                    ControlProductComponent product = (ControlProductComponent)controlCollection.controlCollection[controlName];
                    product.Clear();
                }
            }

            // change control visibility
            /*
            foreach (var item in FieldsListBox.Items)
            {
                var listBoxItem = FieldsListBox.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                listBoxItem.Visibility = System.Windows.Visibility.Hidden;
            }
            */

            // change control visibility
            TaskpaneGrid.Visibility = Visibility.Hidden;

            // adjust height parameter OK
            try
            {
                TaskpaneGrid.RowDefinitions[1].Height = new GridLength(startingHeight + 1, GridUnitType.Pixel);
            }
            catch { }
        }

        public void show(int type) // IT WORKS
        {
            /*
            // enable type treeViewItem
            foreach (AttributeGroup group in AttributeGroups.Items)
            {
                string groupName = group.name;
                int groupType = sortType(groupName.ToLower());

                var treeViewItem = AttributeGroups.ItemContainerGenerator.ContainerFromItem(group) as TreeViewItem;

                if (groupType == type)
                {
                    treeViewItem.IsEnabled = true;
                }
            }
            */

            TaskpaneGrid.Visibility = Visibility.Visible;
        }
        
        #endregion

        #region get control values

        #region load control values

        public void fillControls()
        {
            try
            {
                ControlCollectionSingleton controlCollection = ControlCollectionSingleton.GetInstance();

                SWAttributeAssembler attrAssembler = new SWAttributeAssembler();
                attrAssembler.assembleAttributes();

                foreach (string controlName in controlCollection.controlCollection.Keys)
                {
                    if (!controlName.ToLower().Contains("label") && !controlName.ToLower().Contains("separator"))
                    {
                        ControlProductComponent product = (ControlProductComponent)controlCollection.controlCollection[controlName];

                        string value = attrAssembler.controlValuePairs[FieldList.controlAttributeClassesPairs[controlName]];

                        Debug.Print($"Setting |{product.parameters.getParameter("name")}| to |{value}|");

                        SetControlValue(product, value);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
            }
        }

        private void SetControlValue(ControlProductComponent product, string value)
        {
            object control = default(object);

            switch ((int)product.parameters.getParameter("controlType"))
            {
                case ((int)ControlTypes.checkBox):
                    control = (CheckBox)product.GetControl();
                    bool.TryParse(value, out bool result);
                    ((CheckBox)control).IsChecked = result;
                    break;
                case ((int)ControlTypes.comboBox):
                    control = (ComboBox)product.GetControl();
                    ((ComboBox)control).Text = value;
                    break;
                case ((int)ControlTypes.textBox):
                    control = (TextBox)product.GetControl();
                    ((TextBox)control).Text = value;
                    break;
            };
        }

        #endregion

        #region fill attributes (after closing Taskpane session)

        public void fillAttributes()
        {
            Dictionary<string, IControlProduct> controls = ControlCollectionSingleton.GetInstance().controlCollection;

            foreach (string key in controls.Keys)
            {
                ControlProduct product = (ControlProduct)controls[key];
                object control = product.GetControl();

                updateAttributes(control);

            }
        }

        private void updateAttributes(object sender)
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

        #endregion

        /*
        public void fillControls() // IT WORKS
        {
            try
            {
                SWAttributeAssembler assembler = new SWAttributeAssembler();
                assembler.assembleAttributes();

                List<Field> fieldList = new List<Field>();

                foreach (AttributeGroup group in AttributeGroups.Items)
                {
                    fieldList.AddRange(group.fields.ToList());
                }

                foreach (Field field in fieldList)
                {
                    field.content = assembler.ControlValuePairs[field.label];
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
            }

        }

        public void fillAttributes(ModelDoc2 swModel) // IT WORKS
        {
            try
            {
                List<Field> fieldList = new List<Field>();

                foreach (var item in FieldsListBox.Items)
                {
                    var listBoxItem = (ListBoxItem)FieldsListBox.ItemContainerGenerator.ContainerFromItem(item);

                    FocusManager.SetFocusedElement(FocusManager.GetFocusScope(listBoxItem), null);
                    Keyboard.ClearFocus();

                    Field field = (Field)item;

                    if (field.dataType == swModel.GetType())
                    {
                        if (field.content != null)
                        {
                            string content = field.content.ToString();
                            string label = field.label;

                            Debug.Print($"LABEL: {label}; CONTENT: {content}");

                            UpdatePublisher publisher = new UpdatePublisher();
                            publisher.Update(label, content);

                            publisher = null;
                        }
                    }
                   
                }
            }
            catch(Exception e)
            {
                Debug.Print(e.ToString());
            }
        }
        */

        #endregion

    }

}
