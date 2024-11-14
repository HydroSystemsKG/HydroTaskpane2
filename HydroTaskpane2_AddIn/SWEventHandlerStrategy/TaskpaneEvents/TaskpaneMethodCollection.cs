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
using HydroTaskpane2.References.Flags;
using HydroTaskpane2.References;
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

            try
            {
                taskpane.RemoveItems();
                taskpane.CustomTabInit();
                taskpane.fillAttributes();

                // replace "PTC_WM_REVISION" with new revision attribute
                generalMethodCollection.SetRevisionAttribute();
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
            }

            DebugBuilder.Print("End method");

            return 0;
        }

        public int swApp_FileOpenNotify2(string filename)
        {
            //generalMethodCollection.startAttribution();
            //generalMethodCollection.copyAttributeList();

            try
            {
                taskpane.RemoveItems();
                taskpane.CustomTabInit();
                taskpane.fillControls();

                if (Path.GetExtension(filename).ToLower().Contains("sldasm"))
                {
                    generalMethodCollection.ProcessBOM();
                }

                // replace "PTC_WM_REVISION" with new revision attribute
                DebugBuilder.Print("Replace PTC_WM_REVISION...");

                generalMethodCollection.SetRevisionAttribute();

                DebugBuilder.Print("Replace PTC_WM_REVISION...Done");

                // synch drawing to model (2D to 3D)
                generalMethodCollection.SynchDrawingToModel();
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
            }

            return 0;
        }

        public int swApp_ActiveModelDocChangeNotify()
        {
            try
            {
                taskpane.RemoveItems();
                taskpane.CustomTabInit();

                // turn flag off only when changing active model docs
                HandlingFlag.GetInstance().flag = false;

                taskpane.fillControls();

                HandlingFlag.GetInstance().flag = true;
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
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
                DebugBuilder.Print("When destroyed: Use RemoveItems()");

                //taskpane.RemoveItems();
            }
            catch (Exception e)
            {
                Debug.Print("ERROR - " + e.ToString());
            }

            DebugBuilder.Print("End method");

            return 0;
        }

        public int swDrawing_ActivateSheetPreNotify(string sheetName)
        {
            // check if tabs and sheets match

            // if so: do nothing

            // if not: add corresponding tab to new sheet

            if (!taskpane.getTabStatus())
            {
                DebugBuilder.Print($"Activating sheet {sheetName}");

                taskpane.RemoveItems();
                taskpane.CustomTabInit();

                // turn flag off only when changing active model docs
                HandlingFlag.GetInstance().flag = false;

                taskpane.fillControls();

                HandlingFlag.GetInstance().flag = true;

                DebugBuilder.Print("...done");
            }

            return 0;
        }

        public int swDrawing_AddItemNotify(int entityType, string itemName)
        {
            if (entityType == (int)swNotifyEntityType_e.swNotifyDrawingSheet)
            {
                DebugBuilder.Print($"{itemName} was added");
                DrawingDoc swDrawing = (DrawingDoc) SWModelConnector.GetInstance().swModel;

                swDrawing.ActivateSheet(itemName);
            }

            return 0;
        }

        #endregion

        #region taskpane event handlers

        public int swTaskPane_TaskPaneActivateNotify()
        {
            Debug.Print("ACTIVATED TASKPANE");

            try
            {
                ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;

                DebugBuilder.Print("Check if model is null...");

                if (swModel == null)
                {
                    DebugBuilder.Print("... model is null: Use hide()");
                    taskpane.RemoveItems();

                    return 1;
                }
                else
                {
                    DebugBuilder.Print("Update Drawing");
                    
                    generalMethodCollection.UpdateDrawing(taskpane);

                    DebugBuilder.Print("Update Drawing ...done");

                    generalMethodCollection.setDescription();
                    generalMethodCollection.ProcessBOM();

                    ActivationFlag.GetInstance().flag = true;
                    WeightUpdateFlag.GetInstance().flag = true;
                }

            }
            catch(Exception e)
            {
                DebugBuilder.PrintError(e);
            }

            return 0;
        }

        public int swTaskPane_TaskPaneDeactivateNotify()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                DebugBuilder.Print("Fill attributes");

                

                DebugBuilder.Print("End method");
            }
            catch(Exception e)
            {
                DebugBuilder.PrintError(e);
            }

            stopwatch.Stop();

            DebugBuilder.Print($"ELAPSED TIME: {(stopwatch.ElapsedMilliseconds / 1e3).ToString("0.00s")}");
            
            return 1;
        }

        public int swTaskPane_TaskPaneDestroyNotify()
        {
            return 1;
        }

        #endregion

    }
}
