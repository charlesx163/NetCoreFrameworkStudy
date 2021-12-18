using IocEx.ThirdPartyContainer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace IocEx
{
    class Program
    {
        static void Main(string[] args)
        {
            #region
            //var root = new Cat()
            //.Register<IFoo,Foo>(Lifetime.Transient)
            //.Register<IBar>(_ => new Bar(), Lifetime.Self)//工厂模式直接注册实例
            //.Register<IBaz, Baz>(Lifetime.Root)//from to 
            //.Register(Assembly.GetEntryAssembly());//Assembly
            //var cat1 = root.CreateChild();
            //var cat2 = root.CreateChild();
            //GetServices<IFoo>(cat1);
            //GetServices<IBar>(cat1);
            //GetServices<IBaz>(cat1);
            //GetServices<IQux>(cat1);
            //Console.WriteLine();
            //GetServices<IFoo>(cat2);
            //GetServices<IBar>(cat2);
            //GetServices<IBaz>(cat2);
            //GetServices<IQux>(cat2);
            #endregion
            #region
            //============
            //var rootGeneric = new Cat()
            //.Register<IFoo, Foo>(Lifetime.Transient)
            //.Register<IBar, Bar>(Lifetime.Transient)
            //.Register(typeof(IFoobar<,>), typeof(Foobar<,>), Lifetime.Transient);
            //var foobar = (Foobar<IFoo, IBar>)rootGeneric.GetService<IFoobar<IFoo, IBar>>();
            //Debug.Assert(foobar.Foo is Foo);
            //Debug.Assert(foobar.Bar is Bar);
            //Console.WriteLine("Hello World!");
            #endregion
            #region
            //var services = new Cat()
            //    .Register<Base, Foo>(Lifetime.Transient)
            //    .Register<Base, Bar>(Lifetime.Transient)
            //    .Register<Base, Baz>(Lifetime.Transient)
            //    .GetServices<Base>();
            //Debug.Assert(services.OfType<Foo>().Any());
            #endregion
            #region 如何适配第三方容器
            var services = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IBar>(_ => new Bar())
                .AddSingleton<IBaz>(new Baz());
            var factory = new CatServiceProviderFactory();
            var builder = factory.CreateBuilder(services);
            var container = factory.CreateServiceProvider(builder);
            GetServices();
            GetServices();
            Console.WriteLine("\nRoot container is disposed.");
            (container as IDisposable)?.Dispose();

            void GetServices()
            {
                using (var scope = container.CreateScope())
                {
                    Console.WriteLine("\nService scope is created.");
                    var child = scope.ServiceProvider;

                    child.GetService<IFoo>();
                    child.GetService<IBar>();
                    child.GetService<IBaz>();
                    //child.GetService<IQux>();

                    child.GetService<IFoo>();
                    child.GetService<IBar>();
                    child.GetService<IBaz>();
                    //child.GetService<IQux>();
                    Console.WriteLine("\nService scope is disposed.");
                }
            }
            #endregion
        }

        public static void GetServices<TService>(Cat cat)
        {
            var instance = cat.GetService<TService>();
            Console.WriteLine($"instance type:{instance.GetType()}");
        }
    }
}