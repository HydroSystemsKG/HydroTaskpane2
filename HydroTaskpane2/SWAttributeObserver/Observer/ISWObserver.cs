using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroTaskpane2.SWAttributeObserver.Publisher;

namespace HydroTaskpane2.SWAttributeObserver.Observer
{
    public interface ISWObserver
    {
        void Update(Publisher.Publisher publisher);
    }
}
