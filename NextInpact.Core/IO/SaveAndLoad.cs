using MvvmCross.Platform;
using System;
using System.Threading.Tasks;


namespace NextInpact.Core.IO
{
    public class SaveAndLoad
    {
        public static async Task SaveAsync(ImageFolder folder, string filename, byte[] data)
        {
            await Task.Run(() => Save(folder.ToString(), filename, data));
        }

        public static void Save(String folder, string filename, byte[] data)
        {
            Mvx.Resolve<ISaveAndLoad>().Save(folder, filename, data);            
        }

        public static async Task<byte[]> LoadAsync(ImageFolder folder, string filename)
        {
            return await Task.Run(() => Load(folder.ToString(), filename));
        }

        public static byte[] Load(String folder, string filename)
        {
            return Mvx.Resolve<ISaveAndLoad>().Load(folder, filename);            
        }
    }

}
