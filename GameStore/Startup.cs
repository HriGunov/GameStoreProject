using System.Reflection;
using Autofac;
using GameStore.Core.Abstract;

namespace GameStore
{
    internal static class Startup
    {
        private static void Main()
        {
            // Autofac Configuration Run
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            container.Resolve<IEngine>().Run();
        }
    }
}