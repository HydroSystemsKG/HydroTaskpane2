using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroTaskpane2.References.Flags
{
    public sealed class HandlingFlag
    {
        public bool flag { get; set; }

        // singleton instantiation
        private static readonly HandlingFlag instance = new HandlingFlag();

        public static HandlingFlag GetInstance()
        {
            return instance;
        }

        private HandlingFlag()
        {
            flag = false;
        }
    }
}
