using core_infrastructure;
using domain.Abstractions;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using infrastructure.Persistence;
using infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var migrationAssembly = typeof(is4Config).Assembly.GetName().Name;

            services.AddIdentityServer()
                     //.AddInMemoryIdentityResources(is4Config.GetIdentityResources())
                     //.AddInMemoryApiScopes(is4Config.GetApiScopes())
                     //.AddInMemoryApiResources(is4Config.GetApiResources())
                     //.AddInMemoryClients(is4Config.GetClients())
                     .AddConfigurationStore(opt =>
                     {
                         opt.ConfigureDbContext = c => c.UseNpgsql(config.GetConnectionString("Idp"),
                             x => x.MigrationsAssembly(migrationAssembly));
                     })
                    .AddOperationalStore(opt =>
                    {
                        opt.ConfigureDbContext = o => o.UseNpgsql(config.GetConnectionString("Idp"),
                             x => x.MigrationsAssembly(migrationAssembly));
                    })
                    .AddResourceOwnerValidator<UserValidator>()
                    .AddProfileService<UserProfileService>()
                    .AddDeveloperSigningCredential();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = config.GetValue<string>("IdentityServer:BaseUrl");
                        options.ApiName = "identity-server-api";
                        options.RequireHttpsMetadata = false;
                        options.SupportedTokens = SupportedTokens.Jwt;
                    });

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();

            services.AddCoreInfrastructure<MembershipDbContext>(options =>
            {
                options.ConnectionString = config.GetConnectionString("Idp");
            });
        }

        public static void AddInfrastructuralPipelines(this WebApplication webApp)
        {
            #region Migrate & Seeding Idp Databases
            using (var scope = webApp.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                using (var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>())
                {
                    try
                    {
                        context.Database.Migrate();
                        if (!context.Clients.Any())
                        {
                            foreach (var client in is4Config.GetClients())
                            {
                                context.Clients.Add(client.ToEntity());
                            }
                            context.SaveChanges();
                        }
                        if (!context.IdentityResources.Any())
                        {
                            foreach (var resource in is4Config.GetIdentityResources())
                            {
                                context.IdentityResources.Add(resource.ToEntity());
                            }
                            context.SaveChanges();
                        }
                        if (!context.ApiScopes.Any())
                        {
                            foreach (var apiScope in is4Config.GetApiScopes())
                            {
                                context.ApiScopes.Add(apiScope.ToEntity());
                            }
                            context.SaveChanges();
                        }
                        if (!context.ApiResources.Any())
                        {
                            foreach (var resource in is4Config.GetApiResources())
                            {
                                context.ApiResources.Add(resource.ToEntity());
                            }
                            context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }
            #endregion

            webApp.UseAuthentication();
            webApp.UseAuthorization();
            webApp.UseIdentityServer();
        }
    }
}
