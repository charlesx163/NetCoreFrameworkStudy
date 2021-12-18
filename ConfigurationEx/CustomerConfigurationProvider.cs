using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationEx
{
    public class CustomerConfigurationProvider : ConfigurationProvider
    {
        public override void Load()
        {
            base.Load();
        }
    }
}
