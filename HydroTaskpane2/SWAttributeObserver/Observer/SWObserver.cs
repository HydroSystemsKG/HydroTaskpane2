using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using HydroTaskpane2.SWAttributeObserver.Publisher;
using HydroTaskpane2.SWAttributeObserver.Processing;

namespace HydroTaskpane2.SWAttributeObserver.Observer
{
    public class SWObserver : ISWObserver
    {
        private SWAttributePublisher publisher;

        public SWObserver(SWAttributePublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Update(Publisher.Publisher changedPublisher)
        {
            if (publisher == changedPublisher)
            {
                // extract new attribute value pair and set for current model
                Tuple<string, string> attributeValuePair = publisher.attributeValuePair;

                SWAttributeProcessor processor = new SWAttributeProcessor();
                processor.setAttribute(attributeValuePair);
            }
        }
    }
}
