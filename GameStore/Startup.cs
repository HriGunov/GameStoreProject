using Autofac;
using System;
using System.Reflection;

namespace GameStore
{
    static class Startup
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
        }
    }
}