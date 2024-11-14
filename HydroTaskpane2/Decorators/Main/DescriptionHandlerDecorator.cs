using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HydroTaskpane2.Connectors;
using HydroTaskpane2.Fabrication;
using HydroTaskpane2.References;
using HydroTaskpane2.References.Flags;
using HydroSolidworksLibrary;
using System.Diagnostics;
using SldWorks;
using SwConst;

namespace HydroTaskpane2.Decorators.Main
{
    public class DescriptionHandlerDecorator : HandlerDecorator, IControlProduct
    {
        public DescriptionHandlerDecorator(ControlProductComponent control) : base(control)
        {
            this.control = new StandardHandlerDecorator(this.control);
        }

        public override void Assemble()
        {
            control.Assemble();

            int type = (int)control.parameters.getParameter("controlType");

            if (type != (int)ControlTypes.label)
            {
                ComboBox comboBox = (ComboBox)GetControl();

                comboBox.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(OnTextChanged));
            }
        }

        public override void Dissassemble()
        {
            base.Dissassemble();

            ComboBox comboBox = (ComboBox)GetControl();

            comboBox.RemoveHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(OnTextChanged));

        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }

        private void OnTextChanged(object sender, RoutedEventArgs e)
        {
            bool flag = HandlingFlag.GetInstance().flag;

            if (!flag) { return; }

            controls = collectionSingleton.controlCollection;

            SldWorks.SldWorks swApp = SWModelConnector.GetInstance().swApp;
            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;

            ComboBox comboBox = (ComboBox)sender;
            string modelType;

            if (swModel.GetType() == (int)swDocumentTypes_e.swDocPART)
            {
                modelType = "part";
            }
            else if (swModel.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
            {
                modelType = "assembly";
            }
            else
            {
                return;
            }

            if (comboBox.Name == $"{modelType}_description_dropdown" || comboBox.Name == $"{modelType}_additionaltext_dropdown")
            {
                string description = null;
                string addInfo = null;
                string delimiter = " | ";

                List<string> descriptionAttr = new List<string>
                {
                    SldWorksConstants.sldworks_attr_description_complete_en,
                    SldWorksConstants.sldworks_attr_description_complete_de
                };

                for (int i = 0; i < AttributeVariable.controlAttributePairs[FieldList.controlAttributeClassesPairs[comboBox.Name]].ToArray().Count(); i++)
                {

                    try
                    {
                        ControlProductComponent productDescription = getCollectionControl($"{modelType}_description_dropdown");
                        ControlProductComponent productAdditionalText = getCollectionControl($"{modelType}_additionaltext_dropdown");

                        ComboBox tempDescriptionComboBox = (ComboBox)productDescription.GetControl();
                        ComboBox tempAdditionalTextComboBox = (ComboBox)productAdditionalText.GetControl();

                        description = ReadMultipleDropDown(tempDescriptionComboBox.Text, delimiter, i);
                        addInfo = ReadMultipleDropDown(tempAdditionalTextComboBox.Text, delimiter, i);

                        Debug.Print($"DESCRIPTION: {tempDescriptionComboBox.Text}; ADDINFO: {tempAdditionalTextComboBox.Text}");

                        SldWorksStandards.setAttribute(ref swApp, descriptionAttr[i], description + " " + addInfo);
                    }
                    catch
                    {

                    }
                }
            }
            else
            {
                return;
            }
        }

        private string ReadMultipleDropDown(string text, string delimiter, int index)
        {
            List<string> s = new List<string>(text.Split(new string[] { delimiter }, StringSplitOptions.None));

            return s[index];
        }

    }
}
