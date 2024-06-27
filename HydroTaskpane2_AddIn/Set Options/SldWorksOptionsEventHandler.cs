using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using SwCommands;
using SwConst;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Management;
using System.Windows;
using Microsoft.Win32;

namespace _3DExperienceHydroFixPack
{
    public class SldWorksOptionsEventHandler
    {
        #region solidworks delegates

        // Application Events
        // Open file Events - App
        private static DSldWorksEvents_OnIdleNotifyEventHandler appOnIdle = new DSldWorksEvents_OnIdleNotifyEventHandler(OnAppIdle);

        // Open file Events - App
        private static DSldWorksEvents_FileOpenNotify2EventHandler fileOpen = new DSldWorksEvents_FileOpenNotify2EventHandler(OnFileOpen);
        private static DSldWorksEvents_FileNewNotify2EventHandler fileNew = new DSldWorksEvents_FileNewNotify2EventHandler(OnFileNew);
        
        #endregion

        #region class variables

        // Class variables
        public static SldWorks.SldWorks swApp;
        public static ModelDoc2 swModel;

        public static Func<int> eventMethod;
        public static Func<int> openMethod;

        private static bool allowMethod { get; set; }
        public static bool _allowMethod
        {
            get { return allowMethod; }
            set { allowMethod = value; }
        }

        private static bool methodExecuted = false;

        #endregion

        // Constructor (Solidworks application instance + Model definition)
        public SldWorksOptionsEventHandler(SldWorks.SldWorks swApp2, Func<int> eventMethodI, Func<int> openMethodI)
        {
            swApp = swApp2;
            eventMethod = eventMethodI;
            openMethod = openMethodI;

            AttachAppEventHandlers();
        }

        // Application-level event handlers

        private static void AttachAppEventHandlers()
        {
            swApp.OnIdleNotify += appOnIdle;
            swApp.FileOpenNotify2 += fileOpen;
            swApp.FileNewNotify2 += fileNew;
        }

        private static void DetachAppEventHandlers()
        {
            swApp.OnIdleNotify -= appOnIdle;
            swApp.FileOpenNotify2 -= fileOpen;
            swApp.FileNewNotify2 -= fileNew;
        }

        public void FinishByDetach()
        {
            DetachAppEventHandlers();
        }
        
        // Event-handling function

        private static int OnAppIdle()
        {
            try
            {
                if (allowMethod && !methodExecuted)
                {
                    int result = eventMethod();

                    if (result == 0)
                    {
                        allowMethod = false;
                        methodExecuted = true;
                    }
                }
                else
                {
                    checkForModel();
                }
            }
            catch(Exception e)
            {
                Debug.Print($":: Event Handler :: OnAppIdle :: {e.ToString()}");
            }

            return 0;
        }

        private static void checkForModel()
        {
            try
            {
                ModelDoc2 swModel = swApp.ActiveDoc;

                if (swModel == null)
                {
                    if (methodExecuted)
                    {
                        allowMethod = false;
                    }
                    else
                    {
                        allowMethod = true;
                    }
                }
                else
                {
                    allowMethod = false;
                    methodExecuted = false;
                }

                swModel = null;
            }
            catch
            {
            }
        }

        private static int OnFileOpen(string fileName)
        {
            if (openMethod == null)
            {
                return 0;
            }

            try
            {
                return openMethod();
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
            }

            return 0;
        }

        private static int OnFileNew(object newDoc, int docType, string templateName)
        {
            if (openMethod == null)
            {
                return 0;
            }

            try
            {
                return openMethod();
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
            }

            return 0;
        }
    }
}
