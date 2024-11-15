using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HydroTaskpane2.Fabrication;

namespace HydroTaskpane2.Strategy
{
    public interface IProductStrategy
    {
        UIElement control { get; }
        void AssembleUIElement(FactoryParameters parameters);
        void Assemble(FactoryParameters parameters);
        void Clear();
    }
}
