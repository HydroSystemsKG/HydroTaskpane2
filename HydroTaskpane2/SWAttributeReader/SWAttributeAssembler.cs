using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroTaskpane2.Connectors;
using HydroTaskpane2.References;

namespace HydroTaskpane2.SWAttributeReader
{
    public class SWAttributeAssembler
    {
        public SWModelConnector connector { get; private set; }
        private readonly SWReader swReader;

        public Dictionary<int, string> controlValuePairs { get; private set; }
        public Dictionary<string, string> AttributeValuePairs { get; private set; }

        public SWAttributeAssembler()
        {
            this.connector = SWModelConnector.GetInstance();
            this.swReader = new SWReader();

            this.AttributeValuePairs = new Dictionary<string, string>();
            this.controlValuePairs = new Dictionary<int, string>();
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

            foreach (int key in AttributeVariable.controlAttributePairs.Keys)
            {
                List<string> attributeKeys = AttributeVariable.controlAttributePairs[key];
                List<string> attributeValues = AttributeValuePairs.Where(a => attributeKeys.Contains(a.Key)).Select(a => a.Value).ToList();

                if (attributeValues.All(v => string.IsNullOrWhiteSpace(v) || string.IsNullOrEmpty(v)))
                {
                    controlValue = " ";
                }
                else
                {
                    controlValue = controlValue = string.Join("  |  ", attributeValues);
                }

                controlValuePairs.Add(key, controlValue);
            }
        }
    }
}
