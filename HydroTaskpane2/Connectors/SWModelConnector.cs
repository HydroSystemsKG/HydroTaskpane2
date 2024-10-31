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

        private static readonly SWModelConnector instance = new SWModelConnector();

        public static SWModelConnector GetInstance()
        {
            instance.SWConnect();
            return instance;
        }

        private SWModelConnector()
        {
            
        }

        public void SWConnect()
        {
            base.connect();

            try
            {
                swModel = (ModelDoc2)swApp.ActiveDoc;
            }
            catch { }
        }

        public void SWDisconnect()
        {
            swModel = null;
            base.disconnect();
        }
    }
}
