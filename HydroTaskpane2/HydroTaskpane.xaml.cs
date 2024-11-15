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
using HydroTaskpane2.References.Flags;
using HydroTaskpane2.Fabrication;
using HydroTaskpane2.Connectors;
using HydroTaskpane2.SWAttributeObserver;
using HydroTaskpane2.Custom_Controls;
using HydroTaskpane2.Decorators.Main;
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

        public HydroTaskpane2_UI()
        {
            InitializeComponent();
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

                    try
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
                        else if (type == (int)swDocumentTypes_e.swDocDRAWING)
                        {
                            if (((string)parameters.getParameter("name")).ToLower().Contains("drawing") && !((string)parameters.getParameter("name")).ToLower().Contains("drawing_ang"))
                            {
                                product = factory.CreateProduct(parameters);

                                product.Assemble();

                                controlCollection.controlCollection.Add((string)parameters.getParameter("name"), product);
                                element = product.GetControl();
                            }
                            else if (((string)parameters.getParameter("name")).ToLower().Contains("drawing_ang"))
                            {
                                product = factory.CreateProduct(parameters);

                                product.Assemble();

                                controlCollection.controlCollection.Add((string)parameters.getParameter("name"), product);
                                element = product.GetControl();
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        Debug.Print($"Assembly ERROR: {e.ToString()}");
                    }

                    if (element != null)
                    {
                        var children = stackPanel.Children.Cast<UIElement>();
                        stackPanel.Children.Add(element);
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

                //Debug.Print(tab + $" {Properties.Count().ToString()}");

                TabItem tabItem = new TabItem
                {
                    Header = tab,
                    Name = tab.Replace(" ", "_").Replace("-", "_")
                };

                // add to tabItem content...
                TabContent content = new TabContent(new ListBox());
                Style style = FindResource("NoSelectionListBoxItemStyle") as Style;
                content.CustomListBoxInit(tabItem.Name, style);

                //CustomInit(content.stackPanel, tab);

                if (checkTab(tab))
                {
                    CustomInit(content.stackPanel, tab);
                    tabItem.Content = content.listBox;

                    tbCtrl.Items.Add(tabItem);
                }

                //tabItem.Content = content.listBox;
                //Debug.Print(checkTab(tab).ToString());

                //if (checkTab(tab))
                //{
                //    tbCtrl.Items.Add(tabItem);
                //}

                ((TabItem)tbCtrl.Items[0]).IsSelected = true;

            }

            HandlingFlag.GetInstance().flag = true;
        }

        public void RemoveItems()
        {
            HandlingFlag.GetInstance().flag = false;

            ControlCollectionSingleton controlCollection = ControlCollectionSingleton.GetInstance();

            foreach (string key in controlCollection.controlCollection.Keys)
            {
                ControlProductComponent component = (ControlProductComponent) controlCollection.controlCollection[key];
                component.Dissassemble();
            }

            controlCollection.controlCollection = null;
            controlCollection.controlCollection = new Dictionary<string, IControlProduct>();

            tbCtrl.Items.Clear();

            Debug.Print($"KEY COUNT: {controlCollection.controlCollection.Keys.Count()}");
            
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

                    //string templateName = ((Sheet)swDrawing.GetCurrentSheet()).GetTemplateName();

                    string[] sheetNames = swDrawing.GetSheetNames();

                    foreach (string sheet in sheetNames)
                    {
                        string templateName = (swDrawing.Sheet[sheet]).GetTemplateName();

                        templateName = System.IO.Path.GetFileNameWithoutExtension(templateName);

                        if (templateName.ToLower().Split('_').All(s => tab.ToLower().Contains(s)))
                        {
                            return true;
                        }
                    }
                }

            }

            return false;
        }

        public bool getTabStatus()
        {
            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;
            bool status = true;

            if (swModel.GetType() == (int)swDocumentTypes_e.swDocDRAWING)
            {
                int tabCount = tbCtrl.Items.Count;

                Debug.Print($"Tab count is {tabCount.ToString()}");

                List<string> templateNames = new List<string>();

                DrawingDoc swDrawing = (DrawingDoc)swModel;

                var Properties = typeof(PageNames).GetProperties(BindingFlags.Public | BindingFlags.Static).Select(x => x.GetValue(typeof(PageNames))).ToList();
                List<string> pages = new List<string>();

                foreach (string property in Properties)
                {
                    Debug.Print($"Add pages to pool... {property.Replace(" ", "_")}");

                    pages.Add(property.Replace(" ", "_"));
                }

                string[] sheetNames = swDrawing.GetSheetNames();

                foreach (string sheet in sheetNames)
                {
                    string templateName = (swDrawing.Sheet[sheet]).GetTemplateName();

                    templateName = System.IO.Path.GetFileNameWithoutExtension(templateName);

                    Debug.Print($"Check template name: {templateName} - passes condition 1? {(!templateNames.Contains(templateName) && pages.Contains(templateName)).ToString()}; passes condition 2? {(!templateNames.Contains(templateName)).ToString()}");

                    if (templateName.ToLower().Contains("angebotsblatt"))
                    {
                        if (!templateNames.Contains(templateName) && pages.Contains(templateName))
                        {
                            templateNames.Add(templateName);
                        }
                    }
                    else
                    {
                        if (!templateNames.Contains(templateName))
                        {
                            templateNames.Add(templateName);
                        }
                    }
                }

                Debug.Print($"tabCount: {tabCount}; templateNames: {templateNames.Count}");

                if (tabCount != templateNames.Count)
                {
                    status = false;
                }
            }

            return status;
        }

        #endregion

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
                        Debug.Print($"Fill control |{controlName}|");

                        try
                        {
                            ControlProductComponent product = (ControlProductComponent)controlCollection.controlCollection[controlName];

                            string value = attrAssembler.controlValuePairs[FieldList.controlAttributeClassesPairs[controlName]];

                            //Debug.Print($"Setting |{product.parameters.getParameter("name")}| to |{value}|");

                            SetControlValue(product, value);
                        }
                        catch(Exception ex)
                        {
                            Debug.Print($"Failed to fill |{controlName}|: {ex.ToString()}");
                            continue;
                        }
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

            Debug.Print(":: fillAttributes() :: Begin... ::");

            foreach (string key in controls.Keys)
            {
                if (!key.ToLower().Contains("label") && !key.ToLower().Contains("separator"))
                {
                    Debug.Print($":: fillAttributes() :: Update attribute for control |{key}|... ::");

                    ControlProductComponent product = (ControlProductComponent)controls[key];
                    object control = product.GetControl();

                    updateAttributes(control);

                    Debug.Print($":: fillAttributes() :: |{key}| is done... ::");
                }
            }
        }

        private void updateAttributes(object sender)
        {
            string name = "";
            string content = "";

            // if sender is different than checkbox

            Debug.Print($":: fillAttributes() :: updateAttributes() :: get name and get content... ::");

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

            Debug.Print($":: fillAttributes() :: updateAttributes() :: name: {name}; content: {content}... ::");

            if (!string.IsNullOrEmpty(content) && !string.IsNullOrWhiteSpace(content))
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

    }

}
