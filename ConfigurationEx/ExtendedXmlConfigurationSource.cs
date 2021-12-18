using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationEx
{
    public class ExtendedXmlConfigurationSource: XmlConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new ExtendedXmlConfigurationProvider(this);
        }
    }

    public static class ExtendedXmlConfigurationExtensions
    {
        public static IConfigurationBuilder AddExtendedXmlFile(this IConfigurationBuilder builder, string path)
                => builder.AddExtendedXmlFile(path, false, false);
        public static IConfigurationBuilder AddExtendedXmlFile(this IConfigurationBuilder builder, string path, bool optional)
                => builder.AddExtendedXmlFile(path, optional, false);
        public static IConfigurationBuilder AddExtendedXmlFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            builder.Add(new ExtendedXmlConfigurationSource { Path = path, Optional = optional, ReloadOnChange = reloadOnChange });
            return builder;
        }
    }
}
