using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextInpact.Core.IO
{
    public interface ISaveAndLoad
    {
        void Save(String folder, string filename, byte[] data);
        byte[] Load(String folder, string filename);
    }
}
