using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroTaskpane2_AddIn.SWEventHandlerStrategy
{
    public interface ISWStrategy
    {
        SldWorks.SldWorks swApp { get; set; }
        void AttachEventHandlers();
        void DetachEventHandlers();
    }
}
