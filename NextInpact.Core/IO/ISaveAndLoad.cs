using System;

namespace NextInpact.Core.IO
{
    public interface ISaveAndLoad
    {
        void Save(String folder, string filename, byte[] data);
        byte[] Load(String folder, string filename);
    }
}
