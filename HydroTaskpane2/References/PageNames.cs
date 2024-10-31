using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroTaskpane2.References
{
    public class PageNames
    {
        private PageNames(string value) { Value = value; }

        public string Value { get; private set; }

        public static string standard { get { return (new PageNames("Standard")).ToString(); } }
        public static string angCobra { get { return (new PageNames("Angebotsblatt A4 Cobra")).ToString(); } } // Angebotsblatt_A4_Cobra.slddrt
        public static string angDHeber { get { return (new PageNames("Angebotsblatt A4 D-Heber")).ToString(); } } // Angebotsblatt_A4_D-Heber.slddrt
        public static string angDHeberZwei { get { return (new PageNames("Angebotsblatt A4 D-Heber zweizeilig")).ToString(); } } // Angebotsblatt_A4_D-Heber_zweizeilig.slddrt
        public static string angHydrStuetze { get { return (new PageNames("Angebotsblatt A4 Hydr Stuetze")).ToString(); } } // Angebotsblatt_A4_Hydr_Stuetze.slddrt
        public static string angIDGR { get { return (new PageNames("Angebotsblatt A4 IDGR")).ToString(); } } // Angebotsblatt_A4_IDGR.slddrt
        public static string angMechStuetze { get { return (new PageNames("Angebotsblatt A4 Mech Stuetze")).ToString(); } } // Angebotsblatt_A4_Mech_Stuetze.slddrt
        public static string angPruefvorr { get { return (new PageNames("Angebotsblatt A4 Pruefvorrichtung")).ToString(); } } // Angebotsblatt_A4_Pruefvorrichtung.slddrt
        public static string angRHeber { get { return (new PageNames("Angebotsblatt A4 R-Heber")).ToString(); } } // Angebotsblatt_A4_R-Heber.slddrt
        public static string angRHeberZwei { get { return (new PageNames("Angebotsblatt A4 R-Heber zweizeilig")).ToString(); } } // Angebotsblatt_A4_R-Heber_zweizeilig.slddrt + Angebotsblatt_A4_R-Heber_zweizeilig_20230912.slddrt
        public static string angSteuerp { get { return (new PageNames("Angebotsblatt A4 Steuerpult")).ToString(); } } // Angebotsblatt_A4_Steuerpult.slddrt
        public static string angTOW { get { return (new PageNames("Angebotsblatt A4 TOW")).ToString(); } } // Angebotsblatt_A4_TOW.slddrt
        public static string angWiegeeinr { get { return (new PageNames("Angebotsblatt A4 Wiegeeinrichtung")).ToString(); } } // Angebotsblatt_A4_Wiegeeinrichtung.slddrt

        public override string ToString()
        {
            return Value;
        }
    }
}
