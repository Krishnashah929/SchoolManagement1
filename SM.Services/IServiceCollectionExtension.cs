using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace SM.Services
{
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// Adds the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The service.</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetExecutingAssembly())
                    .Where(x => x.Name.EndsWith("Service"))
                    .AsPublicImplementedInterfaces();
            return services;
        }
    }
}
