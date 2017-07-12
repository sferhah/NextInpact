using NextInpact.iOS;
using System.IO;
using System;
using NextInpact.Core.IO;

[assembly: Xamarin.Forms.Dependency(typeof(iOSSaveAndLoad))]
namespace NextInpact.iOS
{

    public class iOSSaveAndLoad : ISaveAndLoad
    {       
        public void Save(String folder, string filename, byte[] data)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var folderPath = Path.Combine(documentsPath, folder);
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
            var filePath = Path.Combine(folderPath, filename);
            System.IO.File.WriteAllBytes(filePath, data);
        }
        public byte[] Load(String folder, string filename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, folder, filename);
            return System.IO.File.ReadAllBytes(filePath);
        }
    }
}