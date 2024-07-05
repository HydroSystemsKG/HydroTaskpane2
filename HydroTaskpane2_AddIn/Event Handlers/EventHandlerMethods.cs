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

namespace HydroTaskpane2_AddIn.Event_Handlers
{
    public class EventHandlerMethods
    {
        public static TaskpaneView swTaskpaneView { get; set; }
        public static HydroTaskpane2_UI taskpane { get; set; }
        public static SldWorks.SldWorks swApp { get; set; }

        protected static int swApp_FileNewNotify2(object newDoc, int docType, string templateName)
        {
            taskpane.hideContent(false);
            taskpane.removeSelection();

            if (docType == (int)swDocumentTypes_e.swDocPART)
            {
                taskpane.hideTypeControls(0, false); //part
                taskpane.hideTypeControls(1, true); // assembly
                taskpane.hideTypeControls(2, true); // drawing
            }
            else if (docType == (int)swDocumentTypes_e.swDocASSEMBLY)
            {
                taskpane.hideTypeControls(0, true); //part
                taskpane.hideTypeControls(1, false); // assembly
                taskpane.hideTypeControls(2, true); // drawing
            }
            else if (docType == (int)swDocumentTypes_e.swDocDRAWING)
            {
                taskpane.hideTypeControls(0, true); //part
                taskpane.hideTypeControls(1, true); // assembly
                taskpane.hideTypeControls(2, false); // drawing
            }

            return 0;
        }

        protected static int swApp_FileOpenNotify2(string filename)
        {
            taskpane.hideContent(false);
            taskpane.removeSelection();

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

            return 0;
        }

        protected static int swApp_FileCloseNotify(string filename, int reason)
        {
            taskpane.removeSelection();
            return 0;
        }

        #region taskpane event handlers

        protected static int swTaskPane_TaskPaneActivateNotify()
        {
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

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
            return 1;
        }

        protected static int swTaskPane_TaskPaneDestroyNotify()
        {
            Debug.Print(" :: Hydro Taskpane 2.0 :: TaskPaneDestroyNotify...");
            return 1;
        }

        #endregion

    }
}
