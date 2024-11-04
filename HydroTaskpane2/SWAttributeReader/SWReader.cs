using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroTaskpane2.Connectors;
using HydroTaskpane2.SWAttributeReader.ReaderStrategy;
using SldWorks;
using SwCommands;
using SwConst;

namespace HydroTaskpane2.SWAttributeReader
{
    public class SWReader
    {
        public SWModelConnector connector { get; private set; }
        public Dictionary<string, string> AttributeValuePairs { get; private set; }

        private readonly ISWReaderStrategy swCustomStrategy;
        private readonly ISWReaderStrategy swConfigStrategy;

        public SWReader()
        {
            connector = SWModelConnector.GetInstance();
            AttributeValuePairs = new Dictionary<string, string>();

            this.swCustomStrategy = new SWCustomReaderStrategy(this);
            this.swConfigStrategy = new SWConfigReaderStrategy(this);
        }

        public void getAttributeList()
        {
            swCustomStrategy.Read();
            swConfigStrategy.Read();

            Dictionary<string, string> customPairs = swCustomStrategy.getDict();
            Dictionary<string, string> configPairs = swConfigStrategy.getDict();

            Dictionary<string, List<string>> mergedDict = new Dictionary<string, List<string>>();

            if (checkPDM())
            {
                mergedDict = configPairs.Concat(customPairs).GroupBy(e => e.Key, e => e.Value).ToDictionary(g => g.Key, v => v.ToList());
            }
            else
            {
                mergedDict = customPairs.Concat(configPairs).GroupBy(e => e.Key, e => e.Value).ToDictionary(g => g.Key, v => v.ToList());
            }

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

                AttributeValuePairs.Add(key, processedValue);
            }
        }

        private bool checkPDM()
        {
            bool checkbox = connector.swApp.GetUserPreferenceToggle((int)swUserPreferenceToggle_e.swEnable3DEXPERIENCEIntegration);

            string AddInGUID = "{DD2533E5-1513-40D8-82B4-927790D0A896}";
            bool AddInLoaded = false;

            try
            {
                var AddInObject = connector.swApp.GetAddInObject(AddInGUID);
                if (!(AddInObject == null)) { AddInLoaded = true; }
            }
            catch { }

            return (checkbox && AddInLoaded);
        }
    }
}
