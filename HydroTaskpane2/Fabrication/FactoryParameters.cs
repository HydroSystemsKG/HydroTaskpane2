using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HydroTaskpane2.References;
using HydroTaskpane2.Strategy;

namespace HydroTaskpane2.Fabrication
{
    public class FactoryParameters
    {
        public Dictionary<string, object> parameters { get; private set; }
        public IProductStrategy strategy { get; private set; }

        public FactoryParameters(object[] paramArray)
        {
            parameters = new Dictionary<string, object>();

            if (paramArray.Count() == FieldList.keys.Count())
            {
                for (int i = 0; i < paramArray.Count(); i++)
                {
                    parameters.Add(FieldList.keys[i], paramArray[i]);
                }
            }
            else
            {
                throw new SystemException("Parameter array does not match Key array!");
            }

            // get matching strategy
            IProductStrategy strategy = null;

            switch ((int)getParameter("controlType"))
            {
                case ((int)ControlTypes.comboBox):
                    strategy = new ComboBoxStrategy();
                    break;
                case ((int)ControlTypes.textBox):
                    strategy = new TextBoxStrategy();
                    break;
                case ((int)ControlTypes.label):
                    strategy = new LabelStrategy();
                    break;
                case ((int)ControlTypes.checkBox):
                    strategy = new CheckBoxStrategy();
                    break;
                case ((int)ControlTypes.separator):
                    strategy = new SeparatorStrategy();
                    break;
                case ((int)ControlTypes.stack):
                    strategy = new StackStrategy();
                    break;
            }

            this.strategy = strategy;
        }

        public object getParameter(string key)
        {
            return parameters[key];
        }

    }
}
