using PCLStorage;
using System.Threading.Tasks;

namespace NextInpact.Core.IO
{
    public class SaveAndLoad
    {
        public static async Task SaveAsync(ImageFolder sub_folder, string filename, byte[] data)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync(sub_folder.ToString(), CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            using (System.IO.Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                stream.Write(data, 0, data.Length);
            }
        }

        public static async Task<byte[]> LoadAsync(ImageFolder sub_folder, string filename)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync(sub_folder.ToString(), CreationCollisionOption.OpenIfExists);
            
            if ((await folder.CheckExistsAsync(filename)) != ExistenceCheckResult.FileExists)
            {
                return null;
            }

            IFile file = await folder.GetFileAsync(filename);

            using (System.IO.Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                long length = stream.Length;
                byte[] streamBuffer = new byte[length];
                stream.Read(streamBuffer, 0, (int)length);
                return streamBuffer;
            }
        }
    }

}
