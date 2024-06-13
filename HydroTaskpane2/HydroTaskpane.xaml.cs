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
using HydroTaskpane2.Constants;
using System.Collections.ObjectModel;

namespace HydroTaskpane2
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // populate treeview
            populateTree();
        }

        public static void populateTextBoxes()
        {
            
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

                foreach (string key in groupReference[groupName].Keys)
                {
                    AttributeGroup currentGroup = new AttributeGroup() { name = key };
                    List<string> attributeList = groupReference[groupName][key];

                    foreach (string attr in attributeList)
                    {
                        currentGroup.attributes.Add(new AttributeField() { name = attr }); // add attributes after list is concluded
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

        public AttributeGroup()
        {
            this.attributes = new ObservableCollection<AttributeField>();
        }
    }

    public class AttributeField
    {
        public string name { get; set; }
        public string attribute { get; set; }
    }
    #endregion
}
