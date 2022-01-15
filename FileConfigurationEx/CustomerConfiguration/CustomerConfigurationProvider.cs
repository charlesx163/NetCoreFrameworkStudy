using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConfigurationEx.CustomerConfiguration
{
    public class CustomerConfigurationProvider : FileConfigurationProvider
    {
        public override void Load(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
