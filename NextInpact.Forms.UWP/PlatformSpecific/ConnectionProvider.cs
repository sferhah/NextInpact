using System;
using System.IO;
using NextInpact.Core.Data;

namespace NextInpact.Forms.UWP.PlatformSpecific
{
    public class ConnectionProvider : IStringConnectionProvider
    {
        public String GetConnectionString(string sqliteFilename)
        {   
            string documentsPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            var dbPath = Path.Combine(documentsPath, sqliteFilename);
            return dbPath;
        }      
    }
}