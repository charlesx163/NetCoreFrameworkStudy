using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Threading.Tasks;

namespace FileSystemEx
{
    class Program
    {
        static async Task Main(string[] args)
        {
            static void Print(int layer, string name) => Console.WriteLine($"{new string(' ', layer * 4)}{name}");
                new ServiceCollection()
                .AddSingleton<IFileProvider>(new PhysicalFileProvider(@"D:\test"))
                .AddSingleton<IFileManager, FileManager>().BuildServiceProvider()
                .GetService<IFileManager>()
                .ShowStructure(Print);
        }
    }
}
