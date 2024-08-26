using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SldWorks;
using SwConst;
using SwCommands;
using HydroTaskpane2;
using HydroTaskpane2.Connectors;
using System.IO;

namespace HydroTaskpane2_AddIn.SWEventHandlerStrategy.TaskpaneEvents
{
    public class TaskpaneMethodCollection : SWAppConnector
    {
        private SWTaskpaneStrategy swTaskpaneStrategy;
        private GeneralMethodCollection generalMethodCollection;

        private TaskpaneView swTaskpaneView;
        private HydroTaskpane2_UI taskpane;

        public TaskpaneMethodCollection(SWTaskpaneStrategy swTaskpaneStrategy) : base()
        {
            this.swTaskpaneStrategy = swTaskpaneStrategy;

            this.swTaskpaneView = swTaskpaneStrategy.swTaskpaneView;
            this.taskpane = swTaskpaneStrategy.taskpane;

            this.generalMethodCollection = new GeneralMethodCollection();
        }

        #region application event handlers

        public int swApp_FileNewNotify2(object newDoc, int docType, string templateName)
        {
            DebugBuilder.Print("When New: Call hide() and then show()");

            taskpane.hide();
            taskpane.show(docType);

            DebugBuilder.Print("End method");

            return 0;
        }

        public int swApp_FileOpenNotify2(string filename)
        {
            DebugBuilder.Print("When Open: Call hide() and then show() based on extension...");

            taskpane.hide();

            string extension = Path.GetExtension(filename).ToLower();

            if (extension.Contains("prt"))
            {
                DebugBuilder.Print("Extension contains (PRT)");
                taskpane.show((int)swDocumentTypes_e.swDocPART);
            }
            else if (extension.Contains("asm"))
            {
                DebugBuilder.Print("Extension contains (ASM)");
                taskpane.show((int)swDocumentTypes_e.swDocASSEMBLY);
            }
            else if (extension.Contains("drw"))
            {
                DebugBuilder.Print("Extension contains (DRW)");
                taskpane.show((int)swDocumentTypes_e.swDocDRAWING);
            }

            //generalMethodCollection.startAttribution();
            //generalMethodCollection.copyAttributeList();

            DebugBuilder.Print("Fill controls");

            taskpane.fillControls();

            DebugBuilder.Print("End method");

            return 0;
        }

        public int swApp_FileCloseNotify(string filename, int reason)
        {
            try
            {
                DebugBuilder.Print("When closing: use hide()");

                taskpane.hide();
            }
            catch (Exception e)
            {
                Debug.Print("ERROR - " + e.ToString());
            }

            DebugBuilder.Print("End method");

            return 0;
        }

        #endregion

        #region model event handlers

        public int swModel_DestroyNotify2(int DestroyType)
        {
            try
            {
                DebugBuilder.Print("When destroyed: Use hide()");

                // remove content and selection from treeView and hide all content

                taskpane.hide();

                //taskpane.hideTypeControls(docType, true);
            }
            catch (Exception e)
            {
                Debug.Print("ERROR - " + e.ToString());
            }

            DebugBuilder.Print("End method");

            return 0;
        }

        #endregion

        #region taskpane event handlers

        public int swTaskPane_TaskPaneActivateNotify()
        {
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            DebugBuilder.Print("Check if model is null...");

            if (swModel == null)
            {
                DebugBuilder.Print("... model is null: Use hide()");
                try
                {
                    taskpane.hide();

                    return 1;
                }
                catch (Exception e)
                {
                    DebugBuilder.PrintError(e);
                }
            }
            else
            {
                DebugBuilder.Print("... model is not null: setDescription() + fillControls()");
                try
                {
                    // set description and copy attributes
                    DebugBuilder.Print("... setDescription() ...");

                    generalMethodCollection.setDescription();
                }
                catch (Exception e)
                {
                    DebugBuilder.PrintError(e);
                    DebugBuilder.Print(e.ToString());
                }

                //Debug.Print("############## FILL CONTROLS ##############");
                DebugBuilder.Print("... fillControls() ...");

                taskpane.fillControls();
            }

            DebugBuilder.Print("End method");

            return 0;
        }

        public int swTaskPane_TaskPaneDeactivateNotify()
        {
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            DebugBuilder.Print("Fill attributes");

            taskpane.fillAttributes(swModel);

            DebugBuilder.Print("End method");

            return 1;
        }

        public int swTaskPane_TaskPaneDestroyNotify()
        {
            return 1;
        }

        #endregion

    }
}
