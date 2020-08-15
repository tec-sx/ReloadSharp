using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Reload.Core
{
    /// <summary>
    /// The reload Inversion of control system.
    /// </summary>
    internal static class ReloadIOC
    {
        public static DefaultServiceProviderFactory Factory { get; } = new DefaultServiceProviderFactory();
        
        public static IServiceCollection SubSystemsCollection { get; } = new ServiceCollection();
        
        public static IServiceCollection ExtensionSystemsCollection { get; } = new ServiceCollection();
        
        public static IServiceProvider BuildServiceProvider()
        {
            IServiceCollection systemsCollection = SubSystemsCollection.Concat(ExtensionSystemsCollection) as IServiceCollection;

            return Factory.CreateServiceProvider(systemsCollection);
        }



    }
}
