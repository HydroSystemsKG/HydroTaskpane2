using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using SwConst;
using SwCommands;
using HydroTaskpane2;

namespace HydroTaskpane2_AddIn.SWEventHandlerStrategy.TaskpaneEvents
{
    public class SWTaskpaneStrategy : ISWStrategy
    {
        public SldWorks.SldWorks swApp { get; set; }
        public TaskpaneView swTaskpaneView { get; private set; }
        public HydroTaskpane2_UI taskpane { get; private set; }

        private TaskpaneMethodCollection methodCollection;


        public SWTaskpaneStrategy(TaskpaneView swTaskpaneView, HydroTaskpane2_UI taskpane)
        {
            this.swTaskpaneView = swTaskpaneView;
            this.taskpane = taskpane;

            this.methodCollection = new TaskpaneMethodCollection(this);
        }

        public void AttachEventHandlers()
        {
            swApp.ActiveModelDocChangeNotify += OnModelDocChanged;

            // attach handlers to the app instance
            swApp.FileNewNotify2 += methodCollection.swApp_FileNewNotify2;
            swApp.FileOpenNotify2 += methodCollection.swApp_FileOpenNotify2;
            swApp.FileCloseNotify += methodCollection.swApp_FileCloseNotify;

            // taskpane events
            swTaskpaneView.TaskPaneActivateNotify += methodCollection.swTaskPane_TaskPaneActivateNotify;
            swTaskpaneView.TaskPaneDeactivateNotify += methodCollection.swTaskPane_TaskPaneDeactivateNotify;
            swTaskpaneView.TaskPaneDestroyNotify += methodCollection.swTaskPane_TaskPaneDestroyNotify;
        }

        public void DetachEventHandlers()
        {
            swApp.ActiveModelDocChangeNotify -= OnModelDocChanged;

            // detach handlers to the app instance
            swApp.FileNewNotify2 -= methodCollection.swApp_FileNewNotify2;
            swApp.FileOpenNotify2 -= methodCollection.swApp_FileOpenNotify2;
            swApp.FileCloseNotify -= methodCollection.swApp_FileCloseNotify;

            // taskpane events
            swTaskpaneView.TaskPaneActivateNotify -= methodCollection.swTaskPane_TaskPaneActivateNotify;
            swTaskpaneView.TaskPaneDeactivateNotify -= methodCollection.swTaskPane_TaskPaneDeactivateNotify;
            swTaskpaneView.TaskPaneDestroyNotify -= methodCollection.swTaskPane_TaskPaneDestroyNotify;

            DetachDocEventHandlers();
        }

        // Model-level event handlers

        private void AttachDocEventHandlers()
        {
            ModelDoc2 swModel = swApp.ActiveDoc;

            if (swModel != null)
            {
                switch ((swDocumentTypes_e)swModel.GetType())
                {
                    case swDocumentTypes_e.swDocDRAWING:
                        (swModel as DrawingDoc).DestroyNotify2 += methodCollection.swModel_DestroyNotify2;
                        break;
                    case swDocumentTypes_e.swDocASSEMBLY:
                        (swModel as AssemblyDoc).DestroyNotify2 += methodCollection.swModel_DestroyNotify2;
                        break;
                    case swDocumentTypes_e.swDocPART:
                        (swModel as PartDoc).DestroyNotify2 += methodCollection.swModel_DestroyNotify2;
                        break;
                }
            }
        }

        private void DetachDocEventHandlers()
        {
            ModelDoc2 swModel = swApp.ActiveDoc;

            if (swModel != null)
            {
                switch ((swDocumentTypes_e)swModel.GetType())
                {
                    case swDocumentTypes_e.swDocDRAWING:
                        (swModel as DrawingDoc).DestroyNotify2 -= methodCollection.swModel_DestroyNotify2;
                        break;
                    case swDocumentTypes_e.swDocASSEMBLY:
                        (swModel as AssemblyDoc).DestroyNotify2 -= methodCollection.swModel_DestroyNotify2;
                        break;
                    case swDocumentTypes_e.swDocPART:
                        (swModel as PartDoc).DestroyNotify2 -= methodCollection.swModel_DestroyNotify2;
                        break;
                }
            }
        }

        // Attach Doc event Handler

        private int OnModelDocChanged()
        {
            AttachDocEventHandlers();
            return 0;
        }
    }
}
