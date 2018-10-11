using System;
using System.Reflection;
using Autofac;
using GameStore.Core.Abstract;
using GameStore.Core.Logo;

namespace GameStore
{
    internal static class Startup
    {
        private static void Main()
        {
            Console.WriteLine(Logo.Text);

            // Autofac Configuration Run
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            container.Resolve<IEngine>().Run();
        }
    }
}