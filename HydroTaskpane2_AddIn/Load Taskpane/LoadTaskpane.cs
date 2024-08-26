using System;
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
using HydroTaskpane2_AddIn.SWEventHandlerStrategy.Options;
using HydroTaskpane2_AddIn.SWEventHandlerStrategy.AttributeTemplates;
using HydroTaskpane2_AddIn.SWEventHandlerStrategy.TaskpaneEvents;
using System.IO;

namespace HydroTaskpane2_AddIn.Load_Taskpane
{
    [ComVisible(true)]
    public class LoadTaskpane
    {
        public SldWorks.SldWorks swApp { get; set; }

        // event handler strategy (options)
        private SWEventHandlerStrategy.SWContext swContextOptions;
        private SWEventHandlerStrategy.SWContext swContextAttributes;
        private SWEventHandlerStrategy.SWContext swContextTaskpane;

        private TaskpaneView swTaskpaneView { get; set; }
        private HydroTaskpane2_UI taskpane { get; set; }

        public LoadTaskpane(SldWorks.SldWorks swApp)
        {
            this.swApp = swApp;

            // add options event handler (attach all)
            swContextOptions = new SWEventHandlerStrategy.SWContext(swApp, new SWOptionStrategy());
            swContextAttributes = new SWEventHandlerStrategy.SWContext(swApp, new SWAttributeTemplateStrategy());

            swContextOptions.AttachEventHandlers();
            swContextAttributes.AttachEventHandlers();

            try
            {
                createTaskpane();
            }
            catch(Exception e)
            {
                Debug.Print($" :: Hydro Taskpane 2.0 :: Failed to create taskpaneview... Exception Type[{e.GetType().ToString()}]; {e.ToString()}");
            }

            // Set up events
            Debug.Print(" :: Hydro Taskpane 2.0 :: Add Events...");
            swContextTaskpane = new SWEventHandlerStrategy.SWContext(swApp, new SWTaskpaneStrategy(swTaskpaneView, taskpane));
            swContextTaskpane.AttachEventHandlers();
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
            // detach event handlers
            swContextOptions.DetachEventHandlers();
            swContextAttributes.DetachEventHandlers();
            swContextTaskpane.DetachEventHandlers();

            taskpane = null;

            // delete taskpaneview...
            swTaskpaneView.DeleteView();

            // ...release COM Object...
            Marshal.ReleaseComObject(swTaskpaneView);

            // ... and set to null
            swTaskpaneView = null;
        }

    }
}
