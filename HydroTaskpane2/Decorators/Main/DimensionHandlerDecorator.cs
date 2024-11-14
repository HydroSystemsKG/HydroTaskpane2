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
using HydroTaskpane2.References.Flags;
using HydroTaskpane2.SWAttributeObserver;
using SldWorks;
using SwConst;

namespace HydroTaskpane2.Decorators.Main
{
    public class DimensionHandlerDecorator : HandlerDecorator, IControlProduct
    {
        private readonly double mmToInch = 0.039370; // mm to inch

        public DimensionHandlerDecorator(ControlProductComponent control) : base(control)
        {
            
        }

        public override void Assemble()
        {
            control.Assemble();

            DockPanel element = (DockPanel)GetControl();
            
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
            if (ActivationFlag.GetInstance().flag)
            {
                Debug.Print("ON FILE IN IDLE");
                ChangeDimension(control.GetControl(), null);

                if (((DockPanel)GetControl()).Name.ToLower().Contains("_imperial"))
                {
                    ActivationFlag.GetInstance().flag = false;
                }
            }

            return 0;
        }

        private void ChangeDimension(object sender, RoutedEventArgs e)
        {
            DockPanel senderControl = (DockPanel)sender;

            Label valueLabel = (Label) senderControl.Children[1];

            // get dimensions
            ModelDoc2 swModel = SWModelConnector.GetInstance().swModel;

            List<int> dimensions = null;
            dimensions = HydroSolidworksLibrary.SldWorksStandards.GetDimensions(ref swModel);

            if (dimensions == null)
            {
                valueLabel.Content = "not available";
            }
            else
            {
                dimensions = convertToDimensionUnit(sender, dimensions);

                valueLabel.Content = $"{dimensions[0].ToString()} x {dimensions[1].ToString()} x {dimensions[2].ToString()}";

                // update dimension attributes

                string name = senderControl.Name;
                string content = ((string)valueLabel.Content).Replace(" x ", " | ");

                if (!string.IsNullOrEmpty(content))
                {
                    Debug.Print($"Dimension Update - CONTROL: |{name}|; CONTENT: |{content}|");
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
            }

            senderControl.Children[1] = valueLabel;
            senderControl.UpdateLayout();
        }

        private List<int> convertToDimensionUnit(object sender, List<int> dimensions)
        {
            DockPanel senderControl = (DockPanel)sender;

            if (senderControl.Name.ToLower().Contains("_imperial"))
            {
                List<int> imperialDimensions = new List<int>();

                foreach (int d in dimensions)
                {
                    int idimension = (int)(d * (mmToInch));
                    imperialDimensions.Add(idimension);
                }

                return imperialDimensions;
            }
            else
            {
                return dimensions;
            }
        }
    }
}
