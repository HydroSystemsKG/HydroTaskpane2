﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroTaskpane2.Connectors;
using SldWorks;
using SwCommands;
using SwConst;

namespace HydroTaskpane2.SWAttributeObserver.Processing
{
    public class SWAttributeProcessor
    {
        public SWModelConnector connector { get; private set; }

        public SWAttributeProcessor()
        {
            this.connector = SWModelConnector.GetInstance();
        }

        private void setConfigAttribute(Tuple<string, string> attributeValuePair, Configuration configuration)
        {
            ModelDoc2 swModel = connector.swModel;
            CustomPropertyManager swCustPropMgr;

            if (swModel.GetType() != (int)swDocumentTypes_e.swDocDRAWING)
            {
                swCustPropMgr = configuration.CustomPropertyManager;

                int result = swCustPropMgr.Add3(FieldName: attributeValuePair.Item1,
                                                FieldType: (int)swCustomInfoType_e.swCustomInfoText,
                                                FieldValue: attributeValuePair.Item2,
                                                OverwriteExisting: (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
            }
        }

        private void setCustomAttribute(Tuple<string, string> attributeValuePair)
        {
            ModelDoc2 swModel = connector.swModel;
            CustomPropertyManager swCustPropMgr = swModel.Extension.get_CustomPropertyManager("");

            int result = swCustPropMgr.Add3(FieldName: attributeValuePair.Item1,
                                            FieldType: (int)swCustomInfoType_e.swCustomInfoText,
                                            FieldValue: attributeValuePair.Item2,
                                            OverwriteExisting: (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
        }

        public void setAttribute(Tuple<string, string> attributeValuePair)
        {
            ModelDoc2 swModel = connector.swModel;
            Configuration configuration;
            string[] configNames = swModel.GetConfigurationNames();

            if (configNames != null)
            {
                foreach (string name in configNames)
                {
                    configuration = (Configuration)swModel.IGetConfigurationByName(name);
                    setConfigAttribute(attributeValuePair, configuration);
                }
            }

            setCustomAttribute(attributeValuePair);

            swModel.SetSaveFlag();
        }
    }
}
