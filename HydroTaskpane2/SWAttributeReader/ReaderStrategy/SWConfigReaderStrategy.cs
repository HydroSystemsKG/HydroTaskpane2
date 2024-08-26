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
    public class SWConfigReaderStrategy : ISWReaderStrategy
    {
        private SWReader reader;
        private SldWorks.SldWorks swApp;
        private ModelDoc2 swModel;

        private Dictionary<string, string> attributeValuePairs;

        public SWConfigReaderStrategy(SWReader reader)
        {
            this.reader = reader;

            this.swApp = reader.modelConnector.swApp;
            this.swModel = reader.modelConnector.swModel;

            this.attributeValuePairs = new Dictionary<string, string>();
        }

        public void Read()
        {
            Dictionary<string, List<string>> mergedDict = new Dictionary<string, List<string>>();
            List<Dictionary<string, string>> dictList = new List<Dictionary<string, string>>();

            Configuration configuration = default(Configuration);
            string[] configNames = swModel.GetConfigurationNames();

            foreach (string configName in configNames)
            {
                configuration = (Configuration)swModel.GetConfigurationByName(configName);

                Dictionary<string, string> configDict = ReadConfig(configuration);
                dictList.Add(configDict);
            }

            mergedDict = dictList.SelectMany(dict => dict)
                         .ToLookup(pair => pair.Key, pair => pair.Value)
                         .ToDictionary(group => group.Key, group => group.ToList());

            foreach (string key in mergedDict.Keys)
            {
                List<string> values = mergedDict[key];
                string processedValue;

                if (values.Distinct().Count() == 1)
                {
                    processedValue = values[0];
                }
                else
                {
                    processedValue = values.FirstOrDefault(s => !string.IsNullOrEmpty(s));
                }

                attributeValuePairs.Add(key, processedValue);
            }

        }

        public Dictionary<string, string> getDict()
        {
            return attributeValuePairs;
        }

        private Dictionary<string, string> ReadConfig(Configuration configuration)
        {
            Dictionary<string, string> configAttributeValuePairs = new Dictionary<string, string>();

            string attrValue;
            string attrResValue;
            bool resolved = true;
            bool linkAttr = true;

            CustomPropertyManager swCustPropMgr = configuration.CustomPropertyManager;
            string[] attributeNames = swCustPropMgr.GetNames();

            foreach (string attribute in attributeNames)
            {
                if (!attributeValuePairs.Keys.Contains(attribute))
                {
                    int exitcode = swCustPropMgr.Get6(attribute, false, out attrValue, out attrResValue, out resolved, out linkAttr);

                    if (exitcode == (int)swCustomInfoGetResult_e.swCustomInfoGetResult_ResolvedValue)
                    {
                        configAttributeValuePairs.Add(attribute, attrResValue.Trim());
                    }
                    else
                    {
                        configAttributeValuePairs.Add(attribute, "");
                    }
                }

            }

            return configAttributeValuePairs;
        }
    }
}
