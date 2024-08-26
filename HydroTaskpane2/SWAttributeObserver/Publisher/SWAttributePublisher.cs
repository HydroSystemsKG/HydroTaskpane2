using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroTaskpane2.SWAttributeObserver.Publisher
{
    public class SWAttributePublisher : Publisher
    {
        private Tuple<string, string> _attributeValuePair;
        public Tuple<string, string> attributeValuePair
        {
            get { return _attributeValuePair; }
            set
            {
                _attributeValuePair = value;
                NotifyObservers();
            }
        }

        public SWAttributePublisher() : base()
        {

        }
    }
}
