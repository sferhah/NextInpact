using Plugin.NetStandardStorage.Abstractions.Interfaces;
using Plugin.NetStandardStorage.Abstractions.Types;
using System.IO;
using System.Threading.Tasks;

namespace NextInpact.Core.IO
{
    public class SaveAndLoad
    {
        public static async Task<bool> SaveAsync(ImageFolder sub_folder, string filename, byte[] data)
        {
            return await Task.Run(() => Save(sub_folder, filename, data));
        }

        public static bool Save(ImageFolder sub_folder, string filename, byte[] data)
        {
            IFolder rootFolder = Plugin.NetStandardStorage.CrossStorage.FileSystem.LocalStorage;
            IFolder folder = rootFolder.CreateFolder(sub_folder.ToString(), CreationCollisionOption.OpenIfExists);
            IFile file = folder.CreateFile(filename, CreationCollisionOption.ReplaceExisting);

            using (Stream stream = file.Open(FileAccess.ReadWrite))
            {
                stream.Write(data, 0, data.Length);
            }

            return true;
        }

        public static async Task<byte[]> LoadAsync(ImageFolder sub_folder, string filename)
        {
            return await Task.Run(() => Load(sub_folder, filename));
        }

        public static byte[] Load(ImageFolder sub_folder, string filename)
        {
            IFolder rootFolder = Plugin.NetStandardStorage.CrossStorage.FileSystem.LocalStorage;
            IFolder folder = rootFolder.CreateFolder(sub_folder.ToString(), CreationCollisionOption.OpenIfExists);

            if (!folder.CheckFileExists(folder.FullPath + "/" + filename))
            {
                return null;
            }   

            IFile file = folder.GetFile(filename);

            using (Stream stream = file.Open(FileAccess.ReadWrite))
            {
                long length = stream.Length;
                byte[] streamBuffer = new byte[length];
                stream.Read(streamBuffer, 0, (int)length);
                return streamBuffer;
            }
        }
    }

}
