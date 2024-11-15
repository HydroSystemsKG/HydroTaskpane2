using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroTaskpane2.References.Flags
{
    public sealed class DrawingMatFlag
    {
        public bool flag { get; set; }

        // singleton instantiation
        private static readonly DrawingMatFlag instance = new DrawingMatFlag();

        public static DrawingMatFlag GetInstance()
        {
            return instance;
        }

        private DrawingMatFlag()
        {
            flag = false;
        }
    }
}
