using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroTaskpane2.References.Flags
{
    public sealed class DrafterUpdateFlag
    {
        public bool flag { get; set; }

        // singleton instantiation
        private static readonly DrafterUpdateFlag instance = new DrafterUpdateFlag();

        public static DrafterUpdateFlag GetInstance()
        {
            return instance;
        }

        private DrafterUpdateFlag()
        {
            flag = false;
        }
    }
}
