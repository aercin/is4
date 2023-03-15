using core_application.Abstractions;
using core_domain.Abstractions;
using core_infrastructure.persistence;
using core_infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace core_infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreInfrastructure<T>(this IServiceCollection services, Action<DependencyOptions> options) where T : DbContext
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddOptions<DependencyOptions>().Configure(options);

            var dependencyOptions = services.BuildServiceProvider().GetService<IOptions<DependencyOptions>>();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddDbContext<T>(options => options.UseNpgsql(dependencyOptions.Value.ConnectionString));

            services.AddCoreInfrastructure();

            return services;
        }

        public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IHttpContextService, HttpContextService>();
            services.AddHttpContextAccessor();

            return services;
        }

        public sealed class DependencyOptions
        {
            public string ConnectionString { get; set; }
        }
    }
}
