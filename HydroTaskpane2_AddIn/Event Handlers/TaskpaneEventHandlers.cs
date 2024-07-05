using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using SwConst;
using SwCommands;
using System.Runtime.InteropServices;
using HydroTaskpane2;
using System.Reflection;

namespace HydroTaskpane2_AddIn.Event_Handlers
{
    internal class TaskpaneEventHandlers : EventHandlerMethods
    {
        #region event delegates

        // Model Change Event (Fire model events?) - Model
        private static DSldWorksEvents_ActiveModelDocChangeNotifyEventHandler modelChanged = new DSldWorksEvents_ActiveModelDocChangeNotifyEventHandler(OnModelDocChanged);

        #endregion

        #region class variables

        // Class variables
        public static ModelDoc2 swModel { get; set; }

        #endregion

        public TaskpaneEventHandlers(SldWorks.SldWorks swAppI, TaskpaneView swTaskpaneViewI, HydroTaskpane2_UI taskpaneI)
        {
            swApp = swAppI;
            swTaskpaneView = swTaskpaneViewI;
            taskpane = taskpaneI;
        }
        
        #region application-level event handlers

        // Application-level event handlers

        private static void AttachAppEventHandlers()
        {
            swApp.ActiveModelDocChangeNotify += modelChanged;

            // attach handlers to the app instance
            swApp.FileNewNotify2 += swApp_FileNewNotify2;
            swApp.FileOpenNotify2 += swApp_FileOpenNotify2;
            swApp.FileCloseNotify += swApp_FileCloseNotify;

            // taskpane events
            swTaskpaneView.TaskPaneActivateNotify += swTaskPane_TaskPaneActivateNotify;
            swTaskpaneView.TaskPaneDeactivateNotify += swTaskPane_TaskPaneDeactivateNotify;
            swTaskpaneView.TaskPaneDestroyNotify += swTaskPane_TaskPaneDestroyNotify;

        }

        private static void DetachAppEventHandlers()
        {
            swApp.ActiveModelDocChangeNotify -= modelChanged;

            // detach handlers to the app instance
            swApp.FileNewNotify2 -= swApp_FileNewNotify2;
            swApp.FileOpenNotify2 -= swApp_FileOpenNotify2;
            swApp.FileCloseNotify -= swApp_FileCloseNotify;

            // taskpane events
            swTaskpaneView.TaskPaneActivateNotify -= swTaskPane_TaskPaneActivateNotify;
            swTaskpaneView.TaskPaneDeactivateNotify -= swTaskPane_TaskPaneDeactivateNotify;
            swTaskpaneView.TaskPaneDestroyNotify -= swTaskPane_TaskPaneDestroyNotify;

            DetachDocEventHandlers();
        }

        public void StartByAttach()
        {
            AttachAppEventHandlers();
        }

        public void FinishByDetach()
        {
            DetachAppEventHandlers();
        }

        #endregion

        #region model-level event handlers

        // Model-level event handlers

        private static void AttachDocEventHandlers()
        {
            swModel = swApp.ActiveDoc;

            if (swModel != null)
            {
                switch ((swDocumentTypes_e)swModel.GetType())
                {
                    case swDocumentTypes_e.swDocDRAWING:
                        break;
                    case swDocumentTypes_e.swDocASSEMBLY:
                        break;
                    case swDocumentTypes_e.swDocPART:
                        break;
                }
            }

        }

        private static void DetachDocEventHandlers()
        {

            if (swModel != null)
            {
                switch ((swDocumentTypes_e)swModel.GetType())
                {
                    case swDocumentTypes_e.swDocDRAWING:
                        break;
                    case swDocumentTypes_e.swDocASSEMBLY:
                        break;
                    case swDocumentTypes_e.swDocPART:
                        break;
                }
            }

        }

        #endregion

        #region Attach Doc event handler

        // Event-handling function

        private static int OnModelDocChanged()
        {
            AttachDocEventHandlers();
            return 0;
        }

        #endregion

    }
}
