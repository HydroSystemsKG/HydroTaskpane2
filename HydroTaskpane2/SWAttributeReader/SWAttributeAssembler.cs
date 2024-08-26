using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroTaskpane2.Connectors;
using HydroTaskpane2.Variable;
using SldWorks;
using SwCommands;
using SwConst;

namespace HydroTaskpane2.SWAttributeReader
{
    public class SWAttributeAssembler
    {
        public SWModelConnector modelConnector { get; private set; }
        private readonly SWReader swReader;

        public Dictionary<string, string> ControlValuePairs { get; private set; }
        public Dictionary<string, string> AttributeValuePairs { get; private set; }

        public SWAttributeAssembler()
        {
            this.modelConnector = new SWModelConnector();
            this.swReader = new SWReader();

            this.AttributeValuePairs = new Dictionary<string, string>();
            this.ControlValuePairs = new Dictionary<string, string>();
        }

        public void assembleAttributes()
        {
            swReader.getAttributeList();
            AttributeValuePairs = swReader.AttributeValuePairs;

            IterateControls();
        }

        private void IterateControls()
        {
            string controlValue;

            foreach (string control in AttributeVariable.controlAttributes.Keys)
            {
                List<string> attributeKeys = AttributeVariable.controlAttributes[control];
                List<string> attributeValues = AttributeValuePairs.Where(a => attributeKeys.Contains(a.Key)).Select(a => a.Value).ToList();

                if (attributeValues.All(v => string.IsNullOrEmpty(v)))
                {
                    controlValue = "";
                }
                else
                {
                    controlValue = string.Join("  |  ", attributeValues);
                }

                ControlValuePairs.Add(control, controlValue);
            }
        }
    }
}
