using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HydroTaskpane2.Fabrication
{
    public interface IControlProduct
    {
        void Assemble();
        UIElement GetControl();
        void Clear();
    }
}
