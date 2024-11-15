using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroTaskpane2.References.Flags
{
    public sealed class DateUpdateFlag
    {
        public bool flag { get; set; }

        // singleton instantiation
        private static readonly DateUpdateFlag instance = new DateUpdateFlag();

        public static DateUpdateFlag GetInstance()
        {
            return instance;
        }

        private DateUpdateFlag()
        {
            flag = false;
        }
    }
}
