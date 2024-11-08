using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroTaskpane2.Decorators.Main;

namespace HydroTaskpane2.Decorators.Reference
{
    public class DecoratorDict
    {
        public static Dictionary<int, Type> decorators = new Dictionary<int, Type>
        {
            {(int)Decorator_e.standard, typeof(StandardHandlerDecorator)},
            {(int)Decorator_e.material, typeof(MaterialHandlerDecorator)},
            {(int)Decorator_e.description_change, typeof(DescriptionHandlerDecorator)},
            {(int)Decorator_e.strength, typeof(StrengthHandlerDecorator)},
            {(int)Decorator_e.color, typeof(ColorHandlerDecorator)},
            {(int)Decorator_e.cv, typeof(CVHandlerDecorator)},
            {(int)Decorator_e.weldment, typeof(WeldmentHandlerDecorator)},
            {(int)Decorator_e.weld_init, typeof(WeldInitHandlerDecorator)},
            {(int)Decorator_e.drawing_standard, typeof(DrawingStandardHandlerDecorator)}
        };
    }
}
