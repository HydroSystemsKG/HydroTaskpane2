using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace HydroTaskpane2_AddIn
{
    public class DebugBuilder
    {
        private static string method { get; set; }
        private static string className { get; set; }
        private static string projectName { get; set; }

        public static void Print(string messageString)
        {
            getTrace();
            Debug.Print($"::: {projectName} ::: {className} ::: {method} ::: <m> {messageString} /> :::");
        }

        public static void PrintError(Exception e)
        {
            getTrace();
            string message = $"<ERROR> Type:[{e.GetType().ToString()}]; Message:[{e.Message.ToString()}]/>";
            Print(message);
        }

        public static void getTrace()
        {
            StackFrame frame = (new StackTrace()).GetFrame(1);
            
            method = frame.GetMethod().Name;
            className = frame.GetMethod().DeclaringType.Name;
            projectName = Assembly.GetCallingAssembly().GetName().Name;
        }
    }
}
