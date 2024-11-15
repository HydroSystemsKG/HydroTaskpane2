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

namespace HydroTaskpane2_AddIn.SWEventHandlerStrategy.Options
{
    class OptionsClass
    {
        #region class variables

        private static SldWorks.SldWorks swApp;
        public static SldWorks.SldWorks _swApp
        {
            get { return swApp; }
            set { swApp = value; }
        }


        private static Dictionary<string, int> pdmOptionsDict = new Dictionary<string, int>
        {
            // save page
            {"SavePGP", 0}, // unchecked
            {"ReplaceByNewRevision", 1}, // checked
            {"Save-RetainFileName", 0}, // unchecked
            {"Save-DrawingTitleFromModel", 2}, // checked (On every save)
            {"EnableInteractiveAsyncSave", 0}, // subkey is on a different registry (\SaveFolder3DS)
            // open page
            { "RefreshAtOpen", 1}, // checked
            {"NoRevisionWarning", 0}, // checked
            {"ChooseOpenMode", 1}, // checked
        };

        public static string databasePath = @"C:\Program Files\SolidWorks\Hydro\Toolbox_Hydro-Systems_local_3dx\lang\english\SWBrowser.sldedb";

        private static List<string> StandardReferences = new List<string>
        {
            @"S:\Normteilzeichnungen 50000 - 59999",
            @"S:\Nummer 00000 - 00119",
            @"S:\Nummer 00120 - 00399",
            @"S:\Nummer 00400 - 00444",
            @"S:\Nummer 00445 - 00499",
            @"S:\Nummer 00500 - 00999",
            @"S:\Nummer 10000 - 14999",
            @"S:\Nummer 15000 - 17999",
            @"S:\Nummer 18000 - 18999",
            @"S:\Nummer 19000 - 25016",
            @"S:\Nummer 25017 - 31999",
            @"S:\Nummer 32000 - 39999",
            @"S:\Nummer 40000 - 99999",
            @"S:\Nummer A-Z und Sondernummern",
            @"S:\Toolbox_Normteile",
            @"S:\Tools 00000000000000 - 49999999999999",
            @"S:\Tools 97A00000000000 - 98A49999999999",
            @"S:\Tools 98A50000000000 - 98A99999999999",
            @"S:\Tools 98B00000000000 - 98D99999999999",
            @"S:\Tools 98E00000000000 - 98F49999999999",
            @"S:\Tools 98F50000000000 - 99Z99999999999",
            @"S:\Tools 50000000000000 - 96A99999999999",
            @"S:\Tools A0000000000000 - B9999999999999",
            @"S:\Tools C0000000000000 - IAE99999999999",
            @"S:\Tools J0000000000000 - Z9999999999999",
            @"S:\Zwicky",
            @"S:\LFT-Teile"
        };

        #endregion

        public OptionsClass(SldWorks.SldWorks swAppI)
        {
            swApp = swAppI;
        }

        public virtual int setOptions()
        {
            bool addIn = HydroSolidworksLibrary.SldWorksStandards.AddInIsLoaded();
            bool checkBox = HydroSolidworksLibrary.SldWorksStandards.checkIntegrationCheckbox();

            if (checkBox && addIn)
            {
                try
                {
                    // change all options
                    // pdf color
                    changePDFColor();

                    // toolbox
                    string toolboxPath = @"C:\Program Files\SolidWorks\Hydro\Toolbox_Hydro-Systems_local_3dx\";
                    changeToolboxSystemOptions(toolboxPath, true);

                    // dateipositionen
                    changeReferencedDocuments();

                    // PDM
                    changePDMOptions();

                    // additional changes
                    changeAdditionalSettings();
                }
                catch (Exception e)
                {
                    Debug.Print("::: SetOptions (Mit PDM) ::: ERROR: " + e.ToString());
                }
            }
            else
            {
                try
                {
                    // change all options
                    // pdf color
                    changePDFColor();

                    // toolbox
                    string toolboxPath = @"S:\Hydro\System-Optionen\Toolbox_Hydro-Systems";
                    changeToolboxSystemOptions(toolboxPath, false);

                    // dateipositionen
                    changeReferencedDocuments(StandardReferences);

                    // additional changes
                    changeAdditionalSettings();

                }
                catch (Exception e)
                {
                    Debug.Print("::: SetOptions (Ohne PDM) ::: ERROR: " + e.ToString());
                }
            }

            return 0;
        }

        #region change options 

        private static void changeToolboxSystemOptions(string toolboxPath, bool changeDatabase)
        {
            // set system options
            // Set toolbox path
            swApp.SetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swHoleWizardToolBoxFolder, toolboxPath);

            // Uncheck toolbox default search checkbox
            swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swUseFolderAsDefaultSearchLocation, false);

            // change toolbox browser path (Registry Key)
            if (changeDatabase)
            {
                string cpfPath = toolboxRegistryPath();
                try
                {
                    changeToolboxDatabase();
                }
                catch (Exception e)
                {
                    Debug.Print($"::: OptionsClass ::: changeToolboxSystemOptions ::: failed to change toolbox database - {e.ToString()} :::");
                }
            }

        }

        private static void changeAdditionalSettings()
        {
            // turn "3D Interconnect" checkbox off - Systemoptions : General : Import
            swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swMultiCAD_Enable3DInterconnect, false);
        }

        private static void changePDFColor()
        {
            swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swPDFExportInColor, false);
        }

        private static void changeReferencedDocuments(List<string> referenceList = null)
        {
            // set paths
            if (referenceList == null)
            {
                referenceList = new List<string>
                {
                    toolboxRegistryPath(),
                    cachePath()
                };
            }

            string newReference = String.Join(";", referenceList);
            swApp.SetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swFileLocationsDocuments, newReference);
        }

        private static void changePDMOptions()
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Dassault Systemes\SolidWorksPDM", RegistryKeyPermissionCheck.ReadWriteSubTree);
            RegistryKey subregistryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Dassault Systemes\SolidWorksPDM\SaveFolder3DS", RegistryKeyPermissionCheck.ReadWriteSubTree);

            string[] valueNames = registryKey.GetValueNames();
            foreach (string subKey in valueNames)
            {
                if (pdmOptionsDict.Keys.Contains(subKey))
                {
                    object value = registryKey.GetValue(subKey);

                    if ((int)value != pdmOptionsDict[subKey])
                    {
                        value = (object)pdmOptionsDict[subKey];
                        registryKey.SetValue(subKey, value);
                    }

                    Debug.WriteLine($"SUBKEY: {subKey}; VALUE: {value.ToString()}");
                }
            }

            valueNames = subregistryKey.GetValueNames();
            foreach (string subKey in valueNames)
            {
                if (pdmOptionsDict.Keys.Contains(subKey))
                {
                    object value = subregistryKey.GetValue(subKey);

                    if ((int)value != pdmOptionsDict[subKey])
                    {
                        value = (object)pdmOptionsDict[subKey];
                        subregistryKey.SetValue(subKey, value);
                    }

                    Debug.WriteLine($"SUBKEY: {subKey}; VALUE: {value.ToString()}");
                }
            }

            registryKey.Close();
            subregistryKey.Close();

            registryKey = null;
            subregistryKey = null;

        }

        private static void changeToolboxDatabase()
        {
            // call exe from ToolboxQuery
            string processPath = @"\\CAD_DE_SW\D_sw-pool\Hydro\System-Optionen\Macros\ToolboxQuery\ToolboxQuery\ToolboxQuery\bin\Release\ToolboxQuery.exe";

            ProcessStartInfo info = new ProcessStartInfo(processPath)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process();
            process.StartInfo = info;
            process.Start();
        }

        #endregion

        #region path tools

        public static string toolboxRegistryPath()
        {
            string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToLower().Split('\\').Last();
            string value = $@"C:\Users\{user}\AppData\Local\DassaultSystemes\CATTemp\ENOUSWC\Resources\OnPremise\Toolbox";

            return value;
        }

        public static string cachePath()
        {
            string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToLower().Split('\\').Last();
            string cache = $@"D:\3DEXPERIENCE\OnPremise({user})";

            return cache;

        }

        #endregion

    }
}
