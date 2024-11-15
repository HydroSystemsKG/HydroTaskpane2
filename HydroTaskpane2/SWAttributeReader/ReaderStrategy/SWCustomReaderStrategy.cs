using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using SwCommands;
using SwConst;

namespace HydroTaskpane2.SWAttributeReader.ReaderStrategy
{
    public class SWCustomReaderStrategy : ISWReaderStrategy
    {
        private SWReader reader;
        private SldWorks.SldWorks swApp;
        private ModelDoc2 swModel;

        private Dictionary<string, string> attributeValuePairs;

        public SWCustomReaderStrategy(SWReader reader)
        {
            this.reader = reader;

            this.swApp = reader.connector.swApp;
            this.swModel = reader.connector.swModel;

            this.attributeValuePairs = new Dictionary<string, string>();
        }

        public void Read()
        {
            string attrValue;
            string attrResValue;
            bool resolved = true;
            bool linkAttr = true;

            CustomPropertyManager swCustPropMgr = swModel.Extension.get_CustomPropertyManager("");
            string[] attributeNames = swCustPropMgr.GetNames();

            if (attributeNames == null) { return; }

            foreach (string attribute in attributeNames)
            {
                if (!attributeValuePairs.Keys.Contains(attribute))
                {
                    int exitcode = swCustPropMgr.Get6(attribute, false, out attrValue, out attrResValue, out resolved, out linkAttr);

                    if (exitcode == (int)swCustomInfoGetResult_e.swCustomInfoGetResult_ResolvedValue)
                    {
                        attributeValuePairs.Add(attribute, attrResValue.Trim());
                    }
                    else
                    {
                        attributeValuePairs.Add(attribute, "");
                    }
                }
            }
        }

        public Dictionary<string, string> getDict()
        {
            return attributeValuePairs;
        }
    }
}
