using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using HydroTaskpane2.Connectors;
using HydroTaskpane2.Fabrication;
using HydroTaskpane2.References.Flags;
using HydroTaskpane2.SWAttributeObserver;
using HydroSolidworksLibrary;
using SldWorks;
using SwConst;

namespace HydroTaskpane2.Decorators.Main
{
    public class WeightHandlerDecorator : HandlerDecorator, IControlProduct
    {
        private readonly double kgToLb = 2.204624; // kg to pounds

        public WeightHandlerDecorator(ControlProductComponent control) : base(control)
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
            if (WeightUpdateFlag.GetInstance().flag)
            {
                ChangeWeight(control.GetControl(), null);
                
                if (((DockPanel)GetControl()).Name.ToLower().Contains("_imperial"))
                {
                    WeightUpdateFlag.GetInstance().flag = false;
                }
                
            }

            return 0;
        }

        private void ChangeWeight(object sender, RoutedEventArgs e)
        {
            DockPanel senderControl = (DockPanel)sender;
            Label valueLabel = (Label)senderControl.Children[1];

            // update dimension attributes
            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;
            SldWorks.SldWorks swApp = SWModelConnector.GetInstance().swApp;

            string name = senderControl.Name;
            //string content = (string)valueLabel.Content;
            string content = SldWorksStandards.GetWeight(ref swApp);

            if (senderControl.Name.ToLower().Contains("_imperial"))
            {
                double impWeight;
                Double.TryParse(content, out impWeight);

                content = (impWeight * kgToLb).ToString("0.000");

                if (impWeight == 0)
                {
                    content = "-";
                }
            }

            valueLabel.Content = content;

            if (!string.IsNullOrEmpty(content))
            {
                Debug.Print($"Weight Update - CONTROL: |{name}|; CONTENT: |{content}|");
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
