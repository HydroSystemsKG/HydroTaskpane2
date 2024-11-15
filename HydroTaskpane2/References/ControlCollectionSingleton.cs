using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroTaskpane2.Fabrication;

namespace HydroTaskpane2.References
{
    public sealed class ControlCollectionSingleton
    {
        // build Product collection
        public Dictionary<string, IControlProduct> controlCollection { get; set; }

        // singleton instantiation
        private static readonly ControlCollectionSingleton instance = new ControlCollectionSingleton();

        public static ControlCollectionSingleton GetInstance()
        {
            return instance;
        }

        private ControlCollectionSingleton()
        {
            controlCollection = new Dictionary<string, IControlProduct>();
        }


    }
}
