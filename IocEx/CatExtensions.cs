﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IocEx
{
    public static class CatExtensions
    {
        public static Cat Register(this Cat cat, Type from, Type to, Lifetime lifetime)
        {
            Func<Cat, Type[], object> factory = (_, arguments) => Create(_, to, arguments);
            cat.Register(new ServiceRegistry(from, lifetime, factory));
            return cat;
        }
        public static Cat Register<TFrom, TTo>(this Cat cat, Lifetime lifetime) where TTo : TFrom
            => cat.Register(typeof(TFrom), typeof(TTo), lifetime);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="serviceType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static Cat Register(this Cat cat, Type serviceType, object instance)
        {
            Func<Cat, Type[], object> factory = (_, arguments) => instance;
            cat.Register(new ServiceRegistry(serviceType, Lifetime.Root, factory));
            return cat;
        }
        
        /// <summary>
        /// 泛型实例注入
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="instance"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static Cat Register<TService>(this Cat cat, TService instance)
        {
            Func<Cat, Type[], object> factory = (_, arguments) => instance;
            cat.Register(new ServiceRegistry(typeof(TService), Lifetime.Root, factory));
            return cat;
        }
        
        /// <summary>
        /// 工厂模式的注入
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="serviceType"></param>
        /// <param name="factory"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static Cat Register(this Cat cat, Type serviceType,
            Func<Cat, object> factory, Lifetime lifetime)
        {
            cat.Register(new ServiceRegistry(serviceType, lifetime, (_, arguments) => factory(_)));
            return cat;
        }
        
        /// <summary>
        /// 泛型委托工厂注入
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="cat"></param>
        /// <param name="factory"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static Cat Register<TService>(this Cat cat,
            Func<Cat, TService> factory, Lifetime lifetime)
        {
            cat.Register(new ServiceRegistry(typeof(TService), lifetime, (_, arguments) => factory(_)));
            return cat;
        }

        /// <summary>
        /// MapTo attribute注入使用
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static Cat Register(this Cat cat, Assembly assembly)
        {
            var typeAttributes = from type in assembly.GetExportedTypes()
                                 let attribute = type.GetCustomAttribute<MapToAttribute>()
                                 where attribute != null
                                 select new { ServiceType = type, Attribute = attribute };
            foreach(var typeAttribute in typeAttributes)
            {
                cat.Register(typeAttribute.Attribute.ServiceType, typeAttribute.ServiceType, typeAttribute.Attribute.Lifetime);
            }
            return cat;
        }

        public static Cat CreateChild(this Cat cat) => new Cat(cat);
        public static T GetService<T>(this Cat cat) => (T)cat.GetService(typeof(T));
        public static IEnumerable<T> GetServices<T>(this Cat cat) => cat.GetService<IEnumerable<T>>();
        private static object Create(Cat cat, Type type, Type[] genericArguments)
        {
            if (genericArguments.Length > 0)
            {
                type = type.MakeGenericType(genericArguments);
            }
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
            {
                throw new InvalidOperationException($"Cannot create the instance of {type} which does not have an public constructor.");
            }
            var constructor = constructors.FirstOrDefault(it => it.GetCustomAttributes(false).OfType<InjectionAttribute>().Any());
            constructor ??= constructors.First();
            var parameters = constructor.GetParameters();
            if (parameters.Length == 0)
            {
                return Activator.CreateInstance(type);
            }
            var arguments = new object[parameters.Length];
            for (int index = 0; index < arguments.Length; index++)
            {
                arguments[index] = cat.GetService(parameters[index].ParameterType);
            }
            return constructor.Invoke(arguments);
        }
    }
}