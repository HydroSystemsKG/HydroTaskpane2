using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using SwConst;
using SWPublished;
using SolidWorksTools;
using System.Diagnostics;
using HydroTaskpane2;
using HydroTaskpane2.Constants;

namespace HydroTaskpane2_AddIn.Load_Taskpane
{
    public class LoadTaskpane
    {
        public SldWorks.SldWorks swApp { get; set; }
        public TaskpaneView swTaskpaneView { get; set; }

        private HydroTaskpane2_UI taskpane_ui;
        public const string SWTASKPANE_PROGID = IDClass.SWTASKPANE_PROGID;

        public LoadTaskpane(SldWorks.SldWorks swApp)
        {
            this.swApp = swApp;

            createTaskpane();
            AttachEventHandlers();
        }

        private void createTaskpane()
        {
            // Create taskpane

            Debug.Print(" :: Hydro Taskpane 2.0 :: Load taskpane...");

            string[] iconpaths = new string[3];

            int smallImage = 0;
            int mediumImage = 0;
            int largeImage = 0;
            int imageSizeToUse = 0;
            imageSizeToUse = swApp.GetImageSize(out smallImage, out mediumImage, out largeImage);
            string iconpath = @"C:\Program Files\SolidWorks\Hydro\Icons\";
            string iconname = "Flieger_";
            iconpaths[0] = iconpath + iconname + smallImage.ToString() + ".png";
            iconpaths[1] = iconpath + iconname + mediumImage.ToString() + ".png";
            iconpaths[2] = iconpath + iconname + largeImage.ToString() + ".png";

            Debug.Print(" :: Hydro Taskpane 2.0 :: Loading icons " + iconpaths[0] + ", " + iconpaths[1] + " and " + iconpaths[2] + "...");

            Debug.Print(" :: Hydro Taskpane 2.0 :: Create taskpane...");
            swTaskpaneView = (TaskpaneView)swApp.CreateTaskpaneView3(iconpaths, "Hydro Taskpane");

            Debug.Print(" :: Hydro Taskpane 2.0 :: Add Controls...");
            taskpane_ui = (HydroTaskpane2_UI)swTaskpaneView.AddControl(SWTASKPANE_PROGID, string.Empty);

            Debug.Print(" :: Hydro Taskpane 2.0 :: Initialization...");
            taskpane_ui.CustomInit();

            // Set up events
            Debug.Print(" :: Hydro Taskpane 2.0 :: Add Events...");
            //AttachEventHandlers();
        }

        private void AttachEventHandlers()
        {
            this.swTaskpaneView.TaskPaneActivateNotify += swTaskPane_TaskPaneActivateNotify;
            this.swTaskpaneView.TaskPaneDeactivateNotify += swTaskPane_TaskPaneDeactivateNotify;
            this.swTaskpaneView.TaskPaneDestroyNotify += swTaskPane_TaskPaneDestroyNotify;
        }

        public void DetachEventHandlers()
        {
            this.swTaskpaneView.TaskPaneActivateNotify -= swTaskPane_TaskPaneActivateNotify;
            this.swTaskpaneView.TaskPaneDeactivateNotify -= swTaskPane_TaskPaneDeactivateNotify;
            this.swTaskpaneView.TaskPaneDestroyNotify -= swTaskPane_TaskPaneDestroyNotify;
        }

        private void unloadTaskpane()
        {
            taskpane_ui = null;

            // delete taskpaneview...
            swTaskpaneView.DeleteView();

            // ... and set to null
            swTaskpaneView = null;
        }

        #region Load taskpane event handlers

        private int swTaskPane_TaskPaneActivateNotify()
        {
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            if (swModel == null)
            {
                taskpane_ui.hideTreeView(true);
            }
            else if (swModel.GetType() == (int)swDocumentTypes_e.swDocPART)
            {
                taskpane_ui.disableTreeViewItem("assembly", false);
                taskpane_ui.disableTreeViewItem("drawing", false);
            }
            else if (swModel.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
            {
                taskpane_ui.disableTreeViewItem("part", false);
                taskpane_ui.disableTreeViewItem("drawing", false);
            }
            else if (swModel.GetType() == (int)swDocumentTypes_e.swDocDRAWING)
            {
                taskpane_ui.disableTreeViewItem("part", false);
                taskpane_ui.disableTreeViewItem("assembly", false);
            }

            return 0;
        }

        private int swTaskPane_TaskPaneDeactivateNotify()
        {
            Debug.Print(" :: Hydro Taskpane 2.0 :: TaskPaneDeactivateNotify...");
            return 1;
        }

        private int swTaskPane_TaskPaneDestroyNotify()
        {
            Debug.Print(" :: Hydro Taskpane 2.0 :: TaskPaneDestroyNotify...");
            return 1;
        }

        #endregion
    }
}
