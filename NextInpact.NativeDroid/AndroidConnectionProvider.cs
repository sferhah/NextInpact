using System;
using System.IO;
using NextInpact.NativeDroid;
using NextInpact.Core.Data;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidConnectionProvider))]
namespace NextInpact.NativeDroid
{
    public class AndroidConnectionProvider : IStringConnectionProvider
    {
        public String GetConnectionString(string sqliteFilename)
        {
            string dbPath = null;

            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            dbPath = Path.Combine(documentsPath, sqliteFilename);

            return dbPath;
        }      
    }
}