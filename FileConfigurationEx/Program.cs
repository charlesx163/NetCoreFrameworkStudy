using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace FileConfigurationEx
{
    class Program
    {
        static void Main(string[] args)
        {
            #region
            //var source = new FakeConfigurationSource
            //{
            //    Path = @"D:\2019VSProject\NetCoreFrameworkStudy\FileConfigurationEx\profile.json"
            //};
            //Console.WriteLine(source.FileProvider == null);
            //source.ResolveFileProvider();

            //var fileProvider = (PhysicalFileProvider)source.FileProvider;
            //Console.WriteLine(fileProvider.Root);
            //Console.WriteLine(source.Path);
            #endregion
            var rootPath = Directory.GetCurrentDirectory();
            var c=rootPath.AsSpan();
            var source = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("profile.json").Build();

            Console.WriteLine("Hello World!");
        }
        private class FakeConfigurationSource : FileConfigurationSource
        {
            public override IConfigurationProvider Build(IConfigurationBuilder builder)
            {
                throw new NotImplementedException();
            }
        }
    }



}
