using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using HydroTaskpane2.Constants;
using HydroTaskpane2.Variable;
using HydroTaskpane2.SWAttributeReader;
using HydroTaskpane2.SWAttributeObserver;
using HydroTaskpane2.Connectors;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using SldWorks;
using SwConst;
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
        public static Dictionary<Tuple<string, int>, string> controlAttributeValues;
        public ObservableCollection<AttributeGroup> groups;

        private SWModelConnector modelConnector;
        private const string intProgID = "HydroTaskpane2.Taskpane.UI";
        private double startingHeight = 458.75;

        public HydroTaskpane2_UI()
        {
            InitializeComponent();
            this.modelConnector = new SWModelConnector();
        }

        public void CustomInit()
        {
            // populate treeview + populate fields
            populateTree();
        }

        #region hide content

        public void hide() // IT WORKS
        {
            // remove all selections
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

            // clear all controls + collapse treeview

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

            // change control visibility
            foreach (var item in FieldsListBox.Items)
            {
                var listBoxItem = FieldsListBox.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                listBoxItem.Visibility = System.Windows.Visibility.Hidden;
            }

            // adjust height parameter
            try
            {
                TaskpaneGrid.RowDefinitions[1].Height = new GridLength(startingHeight + 1, GridUnitType.Pixel);
            }
            catch { }
        }

        public void show(int type) // IT WORKS
        {
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
        }

        #endregion

        #region get control values
       
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
        
        #endregion

        public int sortType(string key)
        {
            int type = -1;

            if (key.ToLower() == "assembly")
            {
                type = (int)swDocumentTypes_e.swDocASSEMBLY;
            }
            else if (key.ToLower() == "drawing")
            {
                type = (int)swDocumentTypes_e.swDocDRAWING;
            }
            else if (key.ToLower() == "part")
            {
                type = (int)swDocumentTypes_e.swDocPART;
            }

            return type;
        }

        #region TreeView methods

        public void populateTree()
        {
            groups = new ObservableCollection<AttributeGroup>();

            Dictionary<string, Dictionary<string, List<string>>> groupReference = new Dictionary<string, Dictionary<string, List<string>>>
            {
                {"Part", AttributeConstants.partDict},
                {"Assembly", AttributeConstants.assemblyDict},
                {"Drawing", AttributeConstants.drawingDict}
            };

            Dictionary<string, string> images = new Dictionary<string, string>
            {
                {"Part", @"\\CAD_DE_SW\D_sw-pool\Hydro\System-Optionen\Macros\HydroTaskpane2\HydroTaskpane2\Images\part_icon_sldworks.png"},
                {"Assembly", @"\\CAD_DE_SW\D_sw-pool\Hydro\System-Optionen\Macros\HydroTaskpane2\HydroTaskpane2\Images\assembly_icon_sldworks.png"},
                {"Drawing", @"\\CAD_DE_SW\D_sw-pool\Hydro\System-Optionen\Macros\HydroTaskpane2\HydroTaskpane2\Images\drawing_icon_sldworks.png"}
            };

            foreach (string groupName in groupReference.Keys)
            {
                AttributeGroup refGroup = new AttributeGroup() { name = groupName };
                refGroup.groups = new ObservableCollection<AttributeGroup>();

                // add fields
                refGroup.fields = new ObservableCollection<Field>();

                foreach (string key in groupReference[groupName].Keys)
                {
                    AttributeGroup currentGroup = new AttributeGroup() { name = key };
                    List<string> attributeList = groupReference[groupName][key];
                    

                    foreach (string attr in attributeList)
                    {
                        int type = sortType(groupName);
                        Field field = new Field(attr, type);

                        ObservableCollection<Field> fields = new ObservableCollection<Field>();
                        fields.Add(field);

                        currentGroup.attributes.Add(new AttributeField() { name = attr, fields = fields }); // add attributes after list is concluded

                        currentGroup.fields.Add(field);
                        refGroup.fields.Add(field);

                    }

                    refGroup.groups.Add(currentGroup);
                }
                
                refGroup.image = images[groupName];

                groups.Add(refGroup);
            }

            AttributeGroups.ItemsSource = groups;
        }

        #endregion

    }

    #region Field Classes

    public class AttributeGroup
    {
        public string name { get; set; }

        public ObservableCollection<AttributeField> attributes { get; set; }
        public ObservableCollection<AttributeGroup> groups { get; set; }
        public ObservableCollection<Field> fields { get; set; }

        public string image { get; set; }
        public string space { get; set; }
        public string bold { get; set; }
        public bool enabled { get; set; }

        public IList<object> Items
        {
            get
            {
                IList<object> childNodes = new List<object>();
                if (this.groups == null)
                {
                    foreach (var attr in this.attributes)
                        childNodes.Add(attr);
                }
                else if (this.attributes == null)
                {
                    foreach (var group in this.groups)
                        childNodes.Add(group);
                }
                else
                {
                    foreach (var group in this.groups)
                        childNodes.Add(group);
                    foreach (var attr in this.attributes)
                        childNodes.Add(attr);
                }

                if (this.image != null)
                {
                    this.space = "   ";
                    this.bold = "Bold";
                }

                return childNodes;
            }
        }

        public IList<object> fieldItems
        {
            get
            {
                IList<object> childNodes = new List<object>(fields);
                return childNodes;
            }
        }

        public AttributeGroup()
        {
            this.attributes = new ObservableCollection<AttributeField>();
            this.fields = new ObservableCollection<Field>();
            this.enabled = true;
        }
    }

    public class AttributeField
    {
        public string name { get; set; }
        public string attribute { get; set; }
        public ObservableCollection<Field> fields { get; set; }
    }

    public class Field
    {
        public string label { get; set; }
        public string file { get; set; }
        public string controlType { get; set; }
        public int dataType { get; set; }

        public bool comboBoxControl { get; set; }
        public bool textBoxControl { get; set; }
        public bool checkBoxControl { get; set; }
        public bool labelControl { get; set; }

        public List<string> dropdown { get; set; }
        
        public object content { get; set; }
        public string height { get; set; }

        public Field(string label, int type)
        {
            this.label = label;
            this.content = null;
            this.dataType = type;
            this.height = "20";

            switch (type)
            {
                case (int)swDocumentTypes_e.swDocPART:
                    this.file = AttributeValueLists.partControlPathDict[this.label];
                    break;
                case (int)swDocumentTypes_e.swDocASSEMBLY:
                    this.file = AttributeValueLists.assemblyControlPathDict[this.label];
                    break;
                case (int)swDocumentTypes_e.swDocDRAWING:
                    this.file = AttributeValueLists.drawingControlPathDict[this.label];
                    break;
            }

            Debug.Print($"::: Field ::: Label: |{label}|; Type: |{this.dataType.ToString()}|; Hash Code: |{this.GetHashCode().ToString()}| :::");
            readDropdown();
        }

        public void readDropdown()
        {
            this.comboBoxControl = false;
            this.textBoxControl = false;
            this.checkBoxControl = false;
            this.labelControl = false;

            switch (this.file)
            {
                case "":
                    this.controlType = "textBox";
                    this.textBoxControl = true;
                    break;
                case "*":
                    this.controlType = "checkBox";
                    this.checkBoxControl = true;
                    break;
                case "-":
                    this.controlType = "label";
                    this.labelControl = true;
                    break;
                default:
                    this.controlType = "comboBox";
                    this.comboBoxControl = true;
                    break;
            }

            if (this.controlType == "comboBox")
            {
                string[] fileContent = File.ReadAllLines(this.file).Where(l => l.Contains(";")).Select(l => l.Replace(";", " | ")).ToArray();
                this.dropdown = new List<string>(fileContent);
            }
        }

    }

    #endregion

    #region converter methods

    public class BooleanToVisibilityConverter : IValueConverter
    {
        private object GetVisibility(object value)
        {
            if (!(value is bool))
                return Visibility.Collapsed;
            bool objValue = (bool)value;
            if (objValue)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo language)
        {
            return GetVisibility(value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo language)
        {
            throw new NotImplementedException();
        }
    }

    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter == null)
                return 0.7 * System.Convert.ToDouble(value);


            string[] split = parameter.ToString().Split('.');
            double parameterDouble = System.Convert.ToDouble(split[0]) + System.Convert.ToDouble(split[1]) / (Math.Pow(10, split[1].Length));
            return System.Convert.ToDouble(value) * parameterDouble;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Don't need to implement this
            return null;
        }
    }

    #endregion

}
