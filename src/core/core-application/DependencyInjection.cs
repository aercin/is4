using AutoMapper;
using core_application.Features.CrossCuttings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreApplication(this IServiceCollection services, Assembly assembly)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(assembly);
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
