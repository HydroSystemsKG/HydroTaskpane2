using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using SldWorks;
using SwCommands;
using SwConst;

namespace HydroTaskpane2.Connectors
{
    public abstract class SWAppConnector
    {
        public SldWorks.SldWorks swApp { get; set; }

        public SWAppConnector()
        {
            this.swApp = (SldWorks.SldWorks)Marshal.GetActiveObject("SldWorks.Application");
        }

        protected virtual void disconnect()
        {
            Marshal.ReleaseComObject(swApp);
            swApp = null;
        }
    }
}
