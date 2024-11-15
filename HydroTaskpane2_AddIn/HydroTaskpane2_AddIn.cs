using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using SldWorks;
using SwConst;
using SWPublished;
using SolidWorksTools;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using HydroTaskpane2_AddIn.Load_Taskpane;

namespace HydroTaskpane2_AddIn
{
    [Guid("44f1748a-2d72-4d81-b057-3cf723505be8"), ComVisible(true)]
    [SwAddin(Description = "Hydro Taskpane 2.0", Title = "Hydro Taskpane 2.0", LoadAtStartup = false)]

    public class HydroTaskpane2_AddIn : ISwAddin
    {
        SldWorks.SldWorks swApp;
        public LoadTaskpane load { get; set; }

        #region COM Registration
        //
        // Nicht vergessen die 64bit regasm auszuführen !!!
        //
        [ComRegisterFunctionAttribute]
        public static void RegisterFunction(Type t)
        {
            Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;

            string keyname = "SOFTWARE\\SolidWorks\\Addins\\{" + t.GUID.ToString() + "}";
            Microsoft.Win32.RegistryKey addinkey = hklm.CreateSubKey(keyname);
            addinkey.SetValue(null, 0);
            addinkey.SetValue("Description", "Hydro Taskpane 2.0");
            addinkey.SetValue("Title", "Hydro Taskpane 2.0");

        }

        [ComUnregisterFunctionAttribute]
        public static void UnregisterFunction(Type t)
        {
            Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;

            string keyname = "SOFTWARE\\SolidWorks\\Addins\\{" + t.GUID.ToString() + "}";
            hklm.DeleteSubKey(keyname);
        }
        #endregion COM Registration

        #region Solidworks Connection

        public bool ConnectToSW(object ThisSW, int Cookie)
        {
            Debug.Print(" ::::::::::::::::::::::::");
            Debug.Print(" :: Hydro Taskpane 2.0 ::");
            Debug.Print(" ::::::::::::::::::::::::");

            DateTime buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;

            Debug.Print(" :: Hydro Taskpane 2.0 :: Version " + buildDate.ToString("yyyy-MM-dd HH:mm:ss"));
            Debug.Print(" :: Hydro Taskpane 2.0 :: Loading Hydro Taskpane 2.0 AddIn in SldWorks...");

            swApp = (SldWorks.SldWorks)ThisSW;
            swApp.SetAddinCallbackInfo(0, this, Cookie);

            //Load Taskpane
            this.load = new LoadTaskpane(swApp);

            Debug.Print(" :: Hydro Taskpane 2.0 :: ...erledigt!");

            return true;
        }

        public bool DisconnectFromSW()
        {
            // unload taskpane
            this.load.unloadTaskpane();

            // remove option setting

            swApp = null;
            GC.Collect();

            return true;
        }

        #endregion

    }
}
