using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SldWorks;
using SwCommands;
using SwConst;
using HydroSolidworksLibrary;
using HydroTaskpane2_AddIn.SWEventHandlerStrategy.AttributeTemplates;

namespace HydroTaskpane2_AddIn.SWEventHandlerStrategy.AttributeTemplates
{
    public class SWAttributeTemplateStrategy : ISWStrategy
    {
        public SldWorks.SldWorks swApp { get; set; }

        public SWAttributeTemplateStrategy()
        {

        }

        public void AttachEventHandlers()
        {
            swApp.FileOpenNotify2 += OnFileOpen;
            swApp.FileNewNotify2 += OnFileNew;
        }

        public void DetachEventHandlers()
        {
            swApp.FileOpenNotify2 -= OnFileOpen;
            swApp.FileNewNotify2 -= OnFileNew;
        }

        // Event-Handling methods

        private int OnFileOpen(string fileName)
        {
            startAttribution();

            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            if (!(swModel.GetType() == (int)swDocumentTypes_e.swDocDRAWING))
            {
                try
                {
                    Debug.Print("CREATE TEMPLATE");
                    AttributeTemplate.createTemplate(swApp);

                    if (!AttributeTemplate.migAttributePresent)
                    {
                        SldWorksStandards.CopyListOpenMethod(ref swModel);
                        Debug.Print("COPIED LIST 1");
                    }
                    else if (AttributeTemplate.locateAttribute("HYDRO_MIG_CONVERTED").Item1)
                    {
                        SldWorksStandards.CopyListOpenMethod(ref swModel);
                        Debug.Print("COPIED LIST 2");
                    }
                }
                catch (Exception e)
                {
                    Debug.Print(e.ToString());
                }
            }

            return 0;
        }

        private int OnFileNew(object newDoc, int docType, string templateName)
        {
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            if (!(swModel.GetType() == (int)swDocumentTypes_e.swDocDRAWING))
            {
                try
                {
                    Debug.Print("CREATE TEMPLATE");
                    AttributeTemplate.createTemplate(swApp);
                }
                catch (Exception e)
                {
                    Debug.Print(e.ToString());
                }
            }

            return 0;
        }

        // Additional methods

        private void startAttribution()
        {
            // Implement Attribution method
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            // check if SAP information was migrated
            if (swModel != null)
            {
                try
                {
                    Debug.Print("INITIATE ATTRIBUTION ################");
                    AttributeTemplate.AttributionMethod(swApp, swModel);
                }
                catch (Exception e)
                {
                    Debug.Print(e.ToString());
                }
            }

            swModel = null;

        }

    }
}
