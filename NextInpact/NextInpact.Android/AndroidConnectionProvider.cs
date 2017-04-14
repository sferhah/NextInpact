using System;
using System.IO;
using NextInpact.Droid;
using NextInpact.Data;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidConnectionProvider))]
namespace NextInpact.Droid
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