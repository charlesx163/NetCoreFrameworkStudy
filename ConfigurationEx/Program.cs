using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConfigurationEx
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddExtendedXmlFile("appsettings.xml").Build();
            //configuration.C
            //var collection = configuration.Get<IEnumerable<Profile>>();
        }
    }
}
