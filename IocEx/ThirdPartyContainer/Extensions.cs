using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocEx.ThirdPartyContainer
{
   public static class Extensions
    {
        public static Lifetime AsCatLifetime(this ServiceLifetime lifetime)
        {
            return lifetime switch
            {
                ServiceLifetime.Scoped => Lifetime.Self,
                ServiceLifetime.Singleton => Lifetime.Root,
                _ => Lifetime.Transient
            };
        }
    }
}
