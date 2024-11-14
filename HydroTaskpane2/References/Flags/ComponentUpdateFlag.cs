using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroTaskpane2.References.Flags
{
    public sealed class ComponentUpdateFlag
    {
        public bool flag { get; set; }

        // singleton instantiation
        private static readonly ComponentUpdateFlag instance = new ComponentUpdateFlag();

        public static ComponentUpdateFlag GetInstance()
        {
            return instance;
        }

        private ComponentUpdateFlag()
        {
            flag = false;
        }
    }
}
