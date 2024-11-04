using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroTaskpane2.References;

namespace HydroTaskpane2.SWAttributeObserver
{
    public class AttributeValuePairGenerator
    {
        private string name;
        private string content;
        public Dictionary<string, string> attributeValuePairs { get; private set; }

        public AttributeValuePairGenerator(string name, object content)
        {
            this.name = name;
            this.content = content.ToString();

            this.attributeValuePairs = new Dictionary<string, string>();
        }

        public void AssemblePairDict()
        {
            List<string> attributes = getAttributesFromName(name);

            if (attributes.Count() == 0 || attributes == null) { return; }

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

        private List<string> getAttributesFromName(string name)
        {
            return AttributeVariable.controlAttributePairs[FieldList.controlAttributeClassesPairs[name]];
        }
    }
}
