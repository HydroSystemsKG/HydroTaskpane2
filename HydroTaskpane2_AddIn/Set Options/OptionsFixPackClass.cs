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
using HydroSolidworksLibrary;

namespace _3DExperienceHydroFixPack
{
    class OptionsFixPackClass
    {

        public static int counter = 0;

        public static int mainMethod()
        {
            SldWorks.SldWorks swApp = (SldWorks.SldWorks)Marshal.GetActiveObject("SldWorks.Application");

            bool addIn = SldWorksStandards.AddInIsLoaded();
            bool checkBox = SldWorksStandards.checkIntegrationCheckbox();

            if (checkBox && addIn)
            {
                OptionsClass options = new OptionsClass(swApp);

                Console.WriteLine("Set standard options...");
                options.setOptions();

                options = null;
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
