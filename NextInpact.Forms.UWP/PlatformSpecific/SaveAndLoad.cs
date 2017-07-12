using System;
using System.IO;
using NextInpact.Core.IO;
using Windows.Storage;
using System.Threading.Tasks;

namespace NextInpact.UWP.PlatformSpecific
{
    public class WSaveAndLoad : ISaveAndLoad
    {           

        public void Save(String folder, string filename, byte[] data)
        {
            var r = SaveAsync(folder, filename, data).Result;
        }

        public async Task<bool> SaveAsync(String folder, string filename, byte[] data)
        {
            var targetFolder = await ApplicationData.Current.LocalCacheFolder.CreateFolderAsync(folder, CreationCollisionOption.OpenIfExists);
            StorageFile sampleFile = await targetFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteBytesAsync(sampleFile, data);
            return true;
        }

        public byte[] Load(String folder, string filename)
        {   
            return LoadAsync(folder, filename).Result;
        }

        public async Task<byte[]> LoadAsync(String folder, string filename)
        {   
            
            var targetFolder = await ApplicationData.Current.LocalCacheFolder.CreateFolderAsync(folder, CreationCollisionOption.OpenIfExists);

            StorageFile storageFile = (await targetFolder.TryGetItemAsync(filename)) as StorageFile;           

            if(storageFile == null)
            {
                return null;
            }

            byte[] result;
            using (Stream stream = await storageFile.OpenStreamForReadAsync())
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    result = memoryStream.ToArray();
                }
            }

            return result;
        }
    }
}