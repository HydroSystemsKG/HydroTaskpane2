﻿using System;
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

        #endregion

        public OptionsClass(SldWorks.SldWorks swAppI)
        {
            swApp = swAppI;
        }

        public virtual void setOptions()
        {
            try
            {
                // change all options
                // pdf color
                changePDFColor();

                // toolbox
                changeToolboxSystemOptions();

                // dateipositionen
                changeReferencedDocuments();

                // PDM
                changePDMOptions();

                // additional changes
                changeAdditionalSettings();
            }
            catch (Exception e)
            {
                Debug.Print("::: SetOptions ::: ERROR: " + e.ToString());
            }
        }

        #region change options 

        private static void changeToolboxSystemOptions()
        {
            // set system options
            // Set toolbox path
            string toolboxPath = @"C:\Program Files\SolidWorks\Hydro\Toolbox_Hydro-Systems_local_3dx\";
            swApp.SetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swHoleWizardToolBoxFolder, toolboxPath);

            // Uncheck toolbox default search checkbox
            swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swUseFolderAsDefaultSearchLocation, false);

            // change toolbox browser path (Registry Key)
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

        private static void changeAdditionalSettings()
        {
            // turn "3D Interconnect" checkbox off - Systemoptions : General : Import
            swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swMultiCAD_Enable3DInterconnect, false);
        }

        private static void changePDFColor()
        {
            swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swPDFExportInColor, false);
        }

        private static void changeReferencedDocuments()
        {
            // set paths
            List<string> referenceList = new List<string>
            {
                toolboxRegistryPath(),
                cachePath()
            };

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
