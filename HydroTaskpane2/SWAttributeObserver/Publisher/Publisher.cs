using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroTaskpane2.SWAttributeObserver.Observer;

namespace HydroTaskpane2.SWAttributeObserver.Publisher
{
    public class Publisher
    {
        private List<ISWObserver> observers;

        public Publisher()
        {
            observers = new List<ISWObserver>();
        }

        public void Attach(ISWObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(ISWObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (ISWObserver observer in observers)
            {
                observer.Update(this);
            }
        }
    }
}
