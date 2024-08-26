using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using SldWorks;
using SwConst;
using SWPublished;
using SolidWorksTools;
using System.Diagnostics;
using System.IO;
using HydroSolidworksLibrary;
using HydroTaskpane2;

namespace HydroTaskpane2_AddIn.Standard
{
    public class partStandard
    {
        public static SldWorks.SldWorks swApp;
        public static HydroTaskpane2_UI taskpane;
        

        public static string partDimensionsFieldContent;

        public partStandard(SldWorks.SldWorks swAppI)
        {
            swApp = swAppI;
        }

        private void setStandards()
        {
            // Color
            if (SldWorksStandards.getAttribute(ref swApp, SldWorksConstants.sldworks_attr_standardcolor) == "")
            {
                SldWorksStandards.setAttribute(ref swApp, SldWorksConstants.sldworks_attr_standardcolor, "not necessary");
                SldWorksStandards.setAttribute(ref swApp, SldWorksConstants.sldworks_attr_standardcolor_en, "not necessary");
                SldWorksStandards.setAttribute(ref swApp, SldWorksConstants.sldworks_attr_standardcolor_de, "nicht erforderlich");
            }

            // Lacquering
            if (SldWorksStandards.getAttribute(ref swApp, SldWorksConstants.sldworks_attr_lacquering) == "")
            {
                SldWorksStandards.setAttribute(ref swApp, SldWorksConstants.sldworks_attr_lacquering, "not necessary");
                SldWorksStandards.setAttribute(ref swApp, SldWorksConstants.sldworks_attr_lacquering_en, "not necessary");
                SldWorksStandards.setAttribute(ref swApp, SldWorksConstants.sldworks_attr_lacquering_de, "nicht erforderlich");
            }

            // get Dimensions
            List<int> dimensions_list = null;
            dimensions_list = SldWorksStandards.GetDimensions(ref swApp);

            if (dimensions_list == null)
            {
                Debug.Print(" :: Hydro Taskpane :: Dimension could not be computed due to error or empty model...");
                partDimensionsFieldContent = "not available";
            }
            else
            {
                partDimensionsFieldContent = dimensions_list[0].ToString() + " x " + dimensions_list[1].ToString() + " x " + dimensions_list[2].ToString();
            }

            SldWorksStandards.setAttribute(ref swApp, SldWorksConstants.sldworks_attr_dimension1, dimensions_list[2].ToString());
            SldWorksStandards.setAttribute(ref swApp, SldWorksConstants.sldworks_attr_dimension2, dimensions_list[1].ToString());
            SldWorksStandards.setAttribute(ref swApp, SldWorksConstants.sldworks_attr_dimension3, dimensions_list[0].ToString());

            SldWorksStandards.setAttribute(ref swApp, SldWorksConstants.sldworks_attr_components, "0");
            SldWorksStandards.setAttribute(ref swApp, SldWorksConstants.sldworks_attr_assemblymode_en, "component");
            SldWorksStandards.setAttribute(ref swApp, SldWorksConstants.sldworks_attr_assemblymode_de, "Komponente");

            // create SAP materialnumber if non existent
            if (string.IsNullOrWhiteSpace(SldWorksStandards.getAttribute(ref swApp, SldWorksConstants.sldworks_attr_sapmaterial)))
            {
                // check if old SAP_MATERIALNUMBER exists
                if (string.IsNullOrWhiteSpace(SldWorksStandards.getAttribute(ref swApp, "SAP_MATERIALNUMBER")))
                {
                    SldWorksStandards.setAttribute(ref swApp, SldWorksConstants.sldworks_attr_sapmaterial, "");
                }
                else
                {
                    SldWorksStandards.setAttribute(ref swApp, SldWorksConstants.sldworks_attr_sapmaterial, SldWorksStandards.getAttribute(ref swApp, "SAP_MATERIALNUMBER"));
                    SldWorksStandards.deleteAttribute(ref swApp, "SAP_MATERIALNUMBER");
                }
            }


        }
    }
}
