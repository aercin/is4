using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddCoreApplication(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
