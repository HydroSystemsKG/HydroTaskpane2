using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using HydroTaskpane2.Connectors;
using HydroTaskpane2.Fabrication;
using HydroTaskpane2.References;
using HydroTaskpane2.References.Flags;
using HydroTaskpane2.SWAttributeObserver;
using HydroSolidworksLibrary;
using SldWorks;

namespace HydroTaskpane2.Decorators.Main
{
    public class DrafterHandlerDecorator : HandlerDecorator, IControlProduct
    {
        public DrafterHandlerDecorator(ControlProductComponent control) : base(control)
        {

        }

        public override void Assemble()
        {
            control.Assemble();

            int type = (int)control.parameters.getParameter("controlType");

            SldWorks.SldWorks swApp = SWModelConnector.GetInstance().swApp;

            swApp.OnIdleNotify += OnIdle;
        }

        public override void Dissassemble()
        {
            base.Dissassemble();

            SldWorks.SldWorks swApp = SWModelConnector.GetInstance().swApp;

            swApp.OnIdleNotify -= OnIdle;
        }

        public override UIElement GetControl()
        {
            return control.GetControl();
        }

        private int OnIdle()
        {
            if (DrafterUpdateFlag.GetInstance().flag)
            {
                getDrafter();

                DrafterUpdateFlag.GetInstance().flag = false;
            }

            return 0;
        }

        private void getDrafter()
        {
            DockPanel senderControl = (DockPanel)GetControl();
            Label valueLabel = (Label)senderControl.Children[1];

            // update dimension attributes
            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;
            SldWorks.SldWorks swApp = SWModelConnector.GetInstance().swApp;

            string name = senderControl.Name;
            string content = SldWorksStandards.getAttribute(ref swModel, SldWorksConstants.sldworks_attr_drawnby);

            valueLabel.Content = content;

            if (!string.IsNullOrEmpty(content))
            {
                Debug.Print($"Drafter Update - CONTROL: |{name}|; CONTENT: |{content}|");
                UpdatePublisher publisher = new UpdatePublisher();
                publisher.Update(name, content);

                publisher = null;
            }
            else
            {
                UpdatePublisher publisher = new UpdatePublisher();
                publisher.Update(name, "");

                publisher = null;
            }

            SWModelConnector.GetInstance().swModel.ForceRebuild3(true);

            senderControl.Children[1] = valueLabel;
            senderControl.UpdateLayout();
        }
    }
}
