using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SldWorks;
using SwCommands;
using SwConst;

namespace HydroTaskpane2_AddIn.SWEventHandlerStrategy.Options
{
    public class SWOptionStrategy : ISWStrategy
    {
        public SldWorks.SldWorks swApp { get; set; }

        private bool methodExecuted;
        private bool allowMethod;

        public SWOptionStrategy()
        {

        }

        public void AttachEventHandlers()
        {
            this.methodExecuted = false;

            swApp.OnIdleNotify += OnAppIdle;
        }

        public void DetachEventHandlers()
        {
            swApp.OnIdleNotify -= OnAppIdle;
        }

        // Event-Handling Methods
        private int OnAppIdle()
        {
            try
            {
                if (allowMethod && !methodExecuted)
                {
                    OptionsClass options = new OptionsClass(swApp);
                    int result = (new OptionsClass(swApp)).setOptions();

                    if (result == 0)
                    {
                        allowMethod = false;
                        methodExecuted = true;
                    }
                }
                else
                {
                    checkForModel();
                }
            }
            catch (Exception e)
            {
                Debug.Print($":: Event Handler :: OnAppIdle :: {e.ToString()}");
            }

            return 0;
        }
        
        // Additional methods

        private void checkForModel()
        {
            try
            {
                ModelDoc2 swModel = swApp.ActiveDoc;

                if (swModel == null)
                {
                    if (methodExecuted)
                    {
                        allowMethod = false;
                    }
                    else
                    {
                        allowMethod = true;
                    }
                }
                else
                {
                    allowMethod = false;
                    methodExecuted = false;
                }

                swModel = null;
            }
            catch
            {
            }
        }
    }
}
