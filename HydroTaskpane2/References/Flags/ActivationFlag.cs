using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroTaskpane2.References.Flags
{
    public sealed class ActivationFlag
    {
        public bool flag { get; set; }

        // singleton instantiation
        private static readonly ActivationFlag instance = new ActivationFlag();

        public static ActivationFlag GetInstance()
        {
            return instance;
        }

        private ActivationFlag()
        {
            flag = false;
        }
    }
}
