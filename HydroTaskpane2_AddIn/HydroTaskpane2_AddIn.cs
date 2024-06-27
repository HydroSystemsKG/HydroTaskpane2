using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using System.Runtime.InteropServices;
using SwConst;
using SWPublished;
using SolidWorksTools;
using System.Diagnostics;

namespace HydroTaskpane2_AddIn
{
    [Guid("44f1748a-2d72-4d81-b057-3cf723505be8"), ComVisible(true)]
    [SwAddin(Description = "Hydro Taskpane 2.0", Title = "Hydro Taskpane 2.0", LoadAtStartup = false)]

    public class HydroTaskpane2_AddIn : ISwAddin
    {
        SldWorks.SldWorks swApp;

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

        public bool ConnectToSW(object ThisSW, int Cookie)
        {
            swApp = (SldWorks.SldWorks)ThisSW;

            return true;
        }

        public bool DisconnectFromSW()
        {
            swApp = null;
            GC.Collect();

            return true;
        }
    }
}
