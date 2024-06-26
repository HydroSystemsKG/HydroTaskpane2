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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace HydroTaskpane2
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dictionary<string, object> contentDict = new Dictionary<string, object>();

        public MainWindow()
        {
            InitializeComponent();

            // populate treeview + populate fields
            populateTree();
        }

        #region contentDict control

        public void updateDict()
        {
            foreach (string name in contentDict.Keys)
            {
                ContentControl control = (ContentControl)TaskpaneGrid.FindName(name);
                object content = control.Content;

                contentDict[name] = content;
            }
        }

        public void loadFromDict()
        {
            foreach (string name in contentDict.Keys)
            {
                ContentControl control = (ContentControl)TaskpaneGrid.FindName(name);
                object content = contentDict[name];

                control.Content = content;
            }
        }

        #endregion


        public int sortType(string key)
        {
            int type = 0;

            if (key.ToLower() == "assembly")
            {
                type = 1;
            }
            else if (key.ToLower() == "drawing")
            {
                type = 2;
            }

            return type;
        }


        #region TreeView methods and classes
        public void populateTree()
        {
            List<AttributeGroup> groups = new List<AttributeGroup>();
            Dictionary<string, Dictionary<string, List<string>>> groupReference = new Dictionary<string, Dictionary<string, List<string>>>
            {
                {"Part", AttributeConstants.partDict},
                {"Assembly", AttributeConstants.assemblyDict},
                {"Drawing", AttributeConstants.drawingDict}
            };

            Dictionary<string, string> images = new Dictionary<string, string>
            {
                {"Part", @"C:\Users\amenzel\source\repos\HydroTaskpane2\HydroTaskpane2\Images\part_icon_sldworks.png"},
                {"Assembly", @"C:\Users\amenzel\source\repos\HydroTaskpane2\HydroTaskpane2\Images\assembly_icon_sldworks.png"},
                {"Drawing", @"C:\Users\amenzel\source\repos\HydroTaskpane2\HydroTaskpane2\Images\drawing_icon_sldworks.png"}
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
                        // under work
                        object content = null;
                        
                        if (!contentDict.Keys.Contains(field.hashCode))
                        {
                            contentDict.Add(field.hashCode, null);
                        }
                        else if (contentDict[field.hashCode] != null)
                        {
                            content = contentDict[field.hashCode];
                            field.content = content;
                        }

                        // ############

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
        
    }

    public class AttributeGroup
    {
        public string name { get; set; }

        public ObservableCollection<AttributeField> attributes { get; set; }
        public ObservableCollection<AttributeGroup> groups { get; set; }
        public ObservableCollection<Field> fields { get; set; }

        public string image { get; set; }
        public string space { get; set; }
        public string bold { get; set; }

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
        }
    }

    public class AttributeField
    {
        public string name { get; set; }
        public string attribute { get; set; }
        public ObservableCollection<Field> fields { get; set; }
    }
    #endregion

    #region ComboBox methods and classes

    public class Field
    {
        public string label { get; set; }
        public string file { get; set; }
        public string controlType { get; set; }

        public bool comboBoxControl { get; set; }
        public bool textBoxControl { get; set; }
        public bool checkBoxControl { get; set; }
        public bool labelControl { get; set; }

        public List<string> dropdown { get; set; }

        public object content { get; set; }
        public string hashCode { get; set; }

        public Field(string label, int type)
        {
            this.hashCode = this.GetHashCode().ToString();
            this.label = label;
            switch (type)
            {
                case 0:
                    this.file = AttributeValueLists.partControlPathDict[this.label];
                    break;
                case 1:
                    this.file = AttributeValueLists.assemblyControlPathDict[this.label];
                    break;
                case 2:
                    this.file = AttributeValueLists.drawingControlPathDict[this.label];
                    break;
            }

            Debug.Print($"|{this.file}|");
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
                string[] fileContent = File.ReadAllLines(this.file).Where(l => l.Contains(";")).Select(l => l.Replace(";"," | ")).ToArray();
                this.dropdown = new List<string>(fileContent);
            }
        }

    }

    public class FieldView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private object _controlValue;
        public object controlValue
        {
            get { return _controlValue; }
            set
            {
                _controlValue = value;
                OnPropertyChanged(nameof(controlValue));
            }
        }

    }

    #region Converters

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

    #endregion

}
