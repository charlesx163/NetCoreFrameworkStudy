﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocEx.ThirdPartyContainer
{
    public class CatServiceProviderFactory : IServiceProviderFactory<CatBuilder>
    {
        public CatBuilder CreateBuilder(IServiceCollection services)
        {
            var cat = new Cat();
            foreach(var service in services)
            {
                if (service.ImplementationFactory != null)
                {
                    cat.Register(service.ServiceType, catProvider => service.ImplementationFactory(catProvider), service.Lifetime.AsCatLifetime());
                }
                else if (service.ImplementationInstance != null)
                {
                    cat.Register(service.ServiceType, service.ImplementationInstance);
                }
                else
                {
                    cat.Register(service.ServiceType, service.ImplementationType, service.Lifetime.AsCatLifetime());
                }
            }
            return new CatBuilder(cat);
        }

        public IServiceProvider CreateServiceProvider(CatBuilder containerBuilder)
            => containerBuilder.BuildServiceProvider();
    }
}
