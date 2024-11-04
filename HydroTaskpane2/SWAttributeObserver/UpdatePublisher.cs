using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using HydroTaskpane2.SWAttributeObserver.Publisher;
using HydroTaskpane2.SWAttributeObserver.Observer;

namespace HydroTaskpane2.SWAttributeObserver
{
    public class UpdatePublisher
    {
        private AttributeValuePairGenerator generator;
        private SWAttributePublisher publisher;

        public UpdatePublisher()
        {
            this.publisher = new SWAttributePublisher();
        }

        public void Update(string name, object content)
        {
            generator = new AttributeValuePairGenerator(name, content);
            generator.assemblePairDict();

            // subscriber/observer
            SWObserver observer = new SWObserver(publisher);
            publisher.Attach(observer);

            if (generator.attributeValuePairs != null)
            {
                foreach (string attribute in generator.attributeValuePairs.Keys)
                {
                    Tuple<string, string> tuple = new Tuple<string, string>(attribute, generator.attributeValuePairs[attribute]);
                    publisher.attributeValuePair = tuple;
                }
            }

            publisher.Detach(observer);
            observer = null;
        }

    }
}
