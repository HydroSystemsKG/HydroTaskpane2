using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroTaskpane2.References.Flags
{
    public sealed class WeightUpdateFlag
    {
        public bool flag { get; set; }

        // singleton instantiation
        private static readonly WeightUpdateFlag instance = new WeightUpdateFlag();

        public static WeightUpdateFlag GetInstance()
        {
            return instance;
        }

        private WeightUpdateFlag()
        {
            flag = false;
        }
    }
}
