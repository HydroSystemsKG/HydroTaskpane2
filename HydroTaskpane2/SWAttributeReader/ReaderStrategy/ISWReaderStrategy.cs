using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroTaskpane2.SWAttributeReader.ReaderStrategy
{
    public interface ISWReaderStrategy
    {
        void Read();
        Dictionary<string, string> getDict();
    }
}
