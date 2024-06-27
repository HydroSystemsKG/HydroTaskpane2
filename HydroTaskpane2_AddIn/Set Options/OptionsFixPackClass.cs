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
using System.IO;

namespace _3DExperienceHydroFixPack
{
    class OptionsFixPackClass
    {
        #region check for user

        public static string simulationUserListPath = @"C:\Users\amenzel\source\repos\_Archives\simulationUsers.txt";

        public static bool verifyForSimulationUser()
        {
            // check if user has permission to set Simulation Options
            bool simulationUser = false;
            string windowsuser = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToLowerInvariant();

            var list = File.ReadLines(simulationUserListPath).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                if (windowsuser == "hydro\\" + list[i])
                {
                    simulationUser = true;
                    
                }
            }

            return simulationUser;
        }

        #endregion

        public static int counter = 0;

        public static int mainMethod()
        {
            SldWorks.SldWorks swApp = (SldWorks.SldWorks)Marshal.GetActiveObject("SldWorks.Application");

            OptionsClass options = new OptionsClass(swApp);

            Console.WriteLine("Set standard options...");
            options.setOptions();

            options = null;

            swApp = null;

            return 0;
        }

        public static int openMethod()
        {
            SldWorks.SldWorks swApp = (SldWorks.SldWorks)Marshal.GetActiveObject("SldWorks.Application");

            bool simulationUser = verifyForSimulationUser();

            if (simulationUser)
            {
                SimulationOptionsClass simulationOptions = new SimulationOptionsClass(swApp);
                Console.WriteLine("Set simulation options...");
                simulationOptions.setOptions();
                simulationOptions = null;
            }

            swApp = null;

            return 0;
        }

        public static int testMethod()
        {
            Console.WriteLine("Activate method " + counter.ToString());
            counter++;

            return 0;
        }

    }
}
