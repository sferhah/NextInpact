using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NextInpact.IO
{
    public class SaveAndLoad
    {
        public static async Task SaveAsync(ImageFolder folder, string filename, byte[] data)
        {
            await Task.Run(() => Save(folder.ToString(), filename, data));
        }

        public static void Save(String folder, string filename, byte[] data)
        {            
            DependencyService.Get<ISaveAndLoad>().Save(folder,filename, data);
        }

        public static async Task<byte[]> LoadAsync(ImageFolder folder, string filename)
        {
            return await Task.Run(() => Load(folder.ToString(), filename));
        }

        public static byte[] Load(String folder, string filename)
        {
            return DependencyService.Get<ISaveAndLoad>().Load(folder,filename);
        }
    }

}
