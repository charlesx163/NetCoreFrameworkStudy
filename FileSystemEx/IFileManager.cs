using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemEx
{
    public interface IFileManager
    {
        void ShowStructure(Action<int, string> render);
        Task<string> ReadAllTextAsync(string path);
    }
    public class FileManager : IFileManager
    {
        private readonly IFileProvider _fileProvider;
        public FileManager(IFileProvider fileProvider)
        => _fileProvider = fileProvider;
        public void ShowStructure(Action<int, string> render)
        {
            int indent = -1;
            Render("");
            void Render(string subPath)
            {
                indent++;
                foreach(var fileInfo in _fileProvider.GetDirectoryContents(subPath))
                {
                    render(indent, fileInfo.Name);
                    if (fileInfo.IsDirectory)
                    {
                        Render($@"{subPath}\{fileInfo.Name}".Trim('\\'));
                  
                    }
                }
                indent--;
            }
        }

        public async Task<string> ReadAllTextAsync(string path)
        {
            byte[] buffer;
            using var stream = _fileProvider.GetFileInfo(path).CreateReadStream();
            buffer = new byte[stream.Length];
            await stream.ReadAsync(buffer,0,buffer.Length);

            return Encoding.Default.GetString(buffer);
        }

    }
}
