using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroTaskpane2.Variable;

namespace HydroTaskpane2.SWAttributeObserver
{
    public class AttributeValuePairGenerator
    {
        private string label;
        private string content;
        public Dictionary<string, string> attributeValuePairs { get; private set; }

        public AttributeValuePairGenerator(string label, object content)
        {
            this.label = label;
            this.content = content.ToString();

            this.attributeValuePairs = new Dictionary<string, string>();
        }

        public void assemblePairDict()
        {
            List<string> attributes = getAttributesFromLabel(label);

            if (attributes.Count == 0 || attributes == null) { return; }

            if (!content.Contains(" | "))
            {
                attributeValuePairs.Add(attributes[0], content);
            }
            else
            {
                string[] attributeValues = content.Split(new string[] { " | " }, StringSplitOptions.None);

                for (int i = 0; i < attributes.Count; i++)
                {
                    attributeValuePairs.Add(attributes[i], attributeValues[i]);
                }
            }
        }

        private List<string> getAttributesFromLabel(string label)
        {
            return AttributeVariable.controlAttributes[label];
        }

    }
}
