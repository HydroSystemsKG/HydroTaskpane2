using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using SwConst;
using SwCommands;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using HydroTaskpane2;
using HydroTaskpane2.Attribution;

namespace HydroTaskpane2_AddIn.Event_Handlers
{
    public class EventHandlerMethods
    {
        public static TaskpaneView swTaskpaneView { get; set; }
        public static HydroTaskpane2_UI taskpane { get; set; }
        public static SldWorks.SldWorks swApp { get; set; }

        #region application event handlers

        protected static int swApp_FileNewNotify2(object newDoc, int docType, string templateName)
        {
            DebugBuilder.Print("TESTING FileNewNotify2");
            taskpane.hideContent(false);
            taskpane.removeSelection(true);

            if (docType == (int)swDocumentTypes_e.swDocPART)
            {
                taskpane.hideTypeControls((int)swDocumentTypes_e.swDocPART, false); //part
                taskpane.hideTypeControls((int)swDocumentTypes_e.swDocASSEMBLY, true); // assembly
                taskpane.hideTypeControls((int)swDocumentTypes_e.swDocDRAWING, true); // drawing
            }
            else if (docType == (int)swDocumentTypes_e.swDocASSEMBLY)
            {
                taskpane.hideTypeControls((int)swDocumentTypes_e.swDocPART, true); //part
                taskpane.hideTypeControls((int)swDocumentTypes_e.swDocASSEMBLY, false); // assembly
                taskpane.hideTypeControls((int)swDocumentTypes_e.swDocDRAWING, true); // drawing
            }
            else if (docType == (int)swDocumentTypes_e.swDocDRAWING)
            {
                taskpane.hideTypeControls((int)swDocumentTypes_e.swDocPART, true); //part
                taskpane.hideTypeControls((int)swDocumentTypes_e.swDocASSEMBLY, true); // assembly
                taskpane.hideTypeControls((int)swDocumentTypes_e.swDocDRAWING, false); // drawing
            }

            GeneralMethodCollection.startAttribution(swApp);

            return 0;
        }

        protected static int swApp_FileOpenNotify2(string filename)
        {
            DebugBuilder.Print("TESTING FileOpenNotify2");
            taskpane.hideContent(false);
            taskpane.removeSelection(true);

            string extension = Path.GetExtension(filename).ToLower();

            if (extension.Contains("prt"))
            {
                taskpane.hideTypeControls(0, false); //part
                taskpane.hideTypeControls(1, true); // assembly
                taskpane.hideTypeControls(2, true); // drawing
            }
            else if (extension.Contains("asm"))
            {
                taskpane.hideTypeControls(0, true); //part
                taskpane.hideTypeControls(1, false); // assembly
                taskpane.hideTypeControls(2, true); // drawing
            }
            else if (extension.Contains("drw"))
            {
                taskpane.hideTypeControls(0, true); //part
                taskpane.hideTypeControls(1, true); // assembly
                taskpane.hideTypeControls(2, false); // drawing
            }

            GeneralMethodCollection.startAttribution(swApp);
            GeneralMethodCollection.copyAttributeList(swApp);

            return 0;
        }

        protected static int swApp_FileCloseNotify(string filename, int reason)
        {
            try
            {
                DebugBuilder.Print("TESTING FileCloseNotify");
                taskpane.removeSelection(true);
                taskpane.hideContent(true);
            }
            catch (Exception e)
            {
                Debug.Print("ERROR - " + e.ToString());
            }

            return 0;
        }

        #endregion

        #region model event handlers

        protected static int swModel_DestroyNotify2(int DestroyType)
        {
            try
            {
                DebugBuilder.Print("TESTING FileCloseNotify (clearControls, removeSelection and hideContent)");

                // remove content and selection from treeView and hide all content
                ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;
                int docType = swModel.GetType();

                taskpane.clearControls();
                taskpane.removeSelection(true);
                taskpane.hideContent(true);

                //taskpane.hideTypeControls(docType, true);

                swModel = null;
            }
            catch (Exception e)
            {
                Debug.Print("ERROR - " + e.ToString());
            }

            return 0;
        }

        #endregion

        #region taskpane event handlers

        protected static int swTaskPane_TaskPaneActivateNotify()
        {
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            GeneralMethodCollection.setDescription(swApp);

            DebugBuilder.Print("TEST");
            
            if (swModel == null)
            {
                try
                {
                    Debug.Print(" :: Hydro Taskpane 2.0 :: swTaskPane_TaskPaneActivateNotify :: Hide TreeView... ::");
                    taskpane.hideContent(true);

                    return 1;
                }
                catch (Exception e)
                {
                    Debug.Print($" :: Hydro Taskpane 2.0 :: swTaskPane_TaskPaneActivateNotify :: ...Exception Type[{e.GetType().ToString()}]; {e.ToString()} ::");
                }
            }
            

            return 0;
        }

        protected static int swTaskPane_TaskPaneDeactivateNotify()
        {
            Debug.Print(" :: Hydro Taskpane 2.0 :: TaskPaneDeactivateNotify...");
            DebugBuilder.Print("TESTING");
            return 1;
        }

        protected static int swTaskPane_TaskPaneDestroyNotify()
        {
            Debug.Print(" :: Hydro Taskpane 2.0 :: TaskPaneDestroyNotify...");
            DebugBuilder.Print("TESTING");
            return 1;
        }

        #endregion

    }
}
