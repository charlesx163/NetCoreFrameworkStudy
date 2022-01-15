using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConfigurationEx.CustomerConfiguration
{
    public class CustomerConfigurationBuilder : IConfigurationBuilder
    {
        public IDictionary<string, object> Properties => new Dictionary<string,object>();

        public IList<IConfigurationSource> Sources => new List<IConfigurationSource>();

        public IConfigurationBuilder Add(IConfigurationSource source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            this.Sources.Add(source);
            return this;
        }

        public IConfigurationRoot Build()
        {
            List<IConfigurationProvider> list = new List<IConfigurationProvider>();
            foreach(var source in this.Sources)
            {
                var item = source.Build(this);
                list.Add(item);
            }
        }
    }
}
