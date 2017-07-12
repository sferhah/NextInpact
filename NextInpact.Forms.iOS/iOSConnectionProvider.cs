using NextInpact.iOS;
using System.IO;
using System;
using NextInpact.Data;

[assembly: Xamarin.Forms.Dependency(typeof(iOSConnectionProvider))]
namespace NextInpact.iOS
{

    public class iOSConnectionProvider : IStringConnectionProvider
    {
        public String GetConnectionString(string sqliteFilename)
        {          

            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            string dbPath = Path.Combine(libFolder, sqliteFilename);

            return dbPath;
        }
    }
}