using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroTaskpane2_AddIn.SWEventHandlerStrategy
{
    public class SWContext
    {
        private readonly ISWStrategy swStrategy;

        public SWContext(SldWorks.SldWorks swApp, ISWStrategy swStrategy)
        {
            this.swStrategy = swStrategy;
            swStrategy.swApp = swApp;
        }

        public void AttachEventHandlers()
        {
            swStrategy.AttachEventHandlers();
        }

        public void DetachEventHandlers()
        {
            swStrategy.DetachEventHandlers();
        }
    }
}
