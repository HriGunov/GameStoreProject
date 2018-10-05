using System;
using System.Reflection;
using Autofac;
using GameStore.Core;
using GameStore.Data.Context;

namespace GameStore
{
    internal static class Startup
    {
        private static void Main()
        {
            Console.WriteLine("Hello World!");

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var container = builder.Build();             
            container.Resolve<IEngine>().Run();
        }
    }
}