﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Integration;
using SldWorks;
using SwConst;
using SWPublished;
using SolidWorksTools;
using System.Diagnostics;
using HydroTaskpane2;
using HydroTaskpane2.Constants;
using System.Runtime.InteropServices;
using HydroTaskpane2_AddIn.Host;

namespace HydroTaskpane2_AddIn.Load_Taskpane
{
    [ComVisible(true)]
    public class LoadTaskpane
    {
        public SldWorks.SldWorks swApp { get; set; }

        private TaskpaneView swTaskpaneView { get; set; }
        private HydroTaskpane2_UI taskpane { get; set; }

        public LoadTaskpane(SldWorks.SldWorks swApp)
        {
            this.swApp = swApp;

            try
            {
                createTaskpane();
            }
            catch(Exception e)
            {
                Debug.Print($" :: Hydro Taskpane 2.0 :: Failed to create taskpaneview... Exception Type[{e.GetType().ToString()}]; {e.ToString()}");
            }
            
            //AttachEventHandlers();
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

            string title = "Hydro Taskpane 2.0";
            Debug.Print(" :: Hydro Taskpane 2.0 :: Loading icons " + iconpaths[0] + ", " + iconpaths[1] + " and " + iconpaths[2] + "...");

            // add taskpane host control to view
            addHost(iconpaths, title);

            // Set up events
            Debug.Print(" :: Hydro Taskpane 2.0 :: Add Events...");
            //AttachEventHandlers();
        }

        private void addHost(string[] iconpaths, string title)
        {
            Debug.Print(" :: Hydro Taskpane 2.0 :: Create taskpane...");
            swTaskpaneView = (TaskpaneView)swApp.CreateTaskpaneView3(iconpaths, title);

            Debug.Print(" :: Hydro Taskpane 2.0 :: Add Controls...");
            this.taskpane = new HydroTaskpane2_UI();


            ElementHost element = new CustomElementHost
            {
                Child = this.taskpane,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            this.swTaskpaneView.DisplayWindowFromHandlex64(element.Handle.ToInt64());

            this.taskpane.CustomInit();
        }

        public void unloadTaskpane()
        {
            taskpane = null;

            // delete taskpaneview...
            swTaskpaneView.DeleteView();

            // ...release COM Object...
            Marshal.ReleaseComObject(swTaskpaneView);

            // ... and set to null
            swTaskpaneView = null;
        }

        /*
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
        */

        #region Load taskpane event handlers
        /*
        private int swTaskPane_TaskPaneActivateNotify()
        {
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            if (swModel == null)
            {
                taskpane_ui.hydroTaskpane.hideTreeView(true);
            }
            else if (swModel.GetType() == (int)swDocumentTypes_e.swDocPART)
            {
                taskpane_ui.hydroTaskpane.disableTreeViewItem("assembly", false);
                taskpane_ui.hydroTaskpane.disableTreeViewItem("drawing", false);
            }
            else if (swModel.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
            {
                taskpane_ui.hydroTaskpane.disableTreeViewItem("part", false);
                taskpane_ui.hydroTaskpane.disableTreeViewItem("drawing", false);
            }
            else if (swModel.GetType() == (int)swDocumentTypes_e.swDocDRAWING)
            {
                taskpane_ui.hydroTaskpane.disableTreeViewItem("part", false);
                taskpane_ui.hydroTaskpane.disableTreeViewItem("assembly", false);
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
        */
        #endregion
    }
}
