using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SldWorks;
using SwCommands;
using SwConst;

namespace HydroTaskpane2.Connectors
{
    public class SWModelConnector : SWAppConnector
    {
        public ModelDoc2 swModel { get; private set; }

        public SWModelConnector() : base()
        {
            swModel = (ModelDoc2)swApp.ActiveDoc;
        }

        public void SWDisconnect()
        {
            swModel = null;
            base.disconnect();
        }
    }
}
