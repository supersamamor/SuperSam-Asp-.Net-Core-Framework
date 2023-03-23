using ProjectNamePlaceHolder.Services.Domain.Entities;
using ProjectNamePlaceHolder.Services.Infrastructure.Configurations;
using ProjectNamePlaceHolder.Services.Infrastructure.Identity;
using ProjectNamePlaceHolder.Services.Infrastructure.Persistence;
using ProjectNamePlaceHolder.Services.Infrastructure.Persistence.Repositories;
using Cti.Core.Application.Common.Interfaces;
using Cti.Core.Application.Common.Interfaces.ValueResolver;
using Cti.Core.Application.Common.Persistence;
using Cti.Core.Domain.Common.Contracts;
using Cti.Core.Infrastructure.Services;
using Cti.Core.Infrastructure.ValueResolver;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ProjectNamePlaceHolder.Services.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();

            //Configurations
            services.AddOptions();
            services.Configure<Configurations.DbContextOptions>(configuration.GetSection(nameof(Configurations.DbContextOptions)));

            var _apiOptions = configuration.GetSection(nameof(ApiOptions)).Get<ApiOptions>();
            var _dbCtxOptions = configuration.GetSection(nameof(Configurations.DbContextOptions)).Get<Configurations.DbContextOptions>();
            var _appOptions = configuration.GetSection(nameof(AppOptions)).Get<AppOptions>();
            var _serviceAPIOpts = configuration.GetSection(nameof(ServiceAPIOptions)).Get<ServiceAPIOptions>();

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ProjectNamePlaceHolder.ServicesDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b =>
                        {
                            b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                            b.CommandTimeout(_dbCtxOptions.CommandTimeout);
                        })
                    );
            }

            //Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = _apiOptions.IdentityServerBaseUrl;
                    options.ApiName = _apiOptions.OidcApiName;
                    options.RequireHttpsMetadata = _apiOptions.RequireHttpsMetadata;
                });

            //Services
            services.AddSingleton<IApp>(_appOptions);
            services.AddTransient<ICacheProvider, CacheProviderService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IKeyGenerator, MongoKeyGenerator>();

            services.AddSingleton(typeof(INowValueResolver<,>), typeof(NowValueResolver<,>));
            services.AddSingleton(typeof(INullableNowValueResolver<,>), typeof(NullableNowValueResolver<,>));
            services.AddSingleton(typeof(IStringKeyValueResolver<,>), typeof(StringKeyValueResolver<,>));
            services.AddSingleton(typeof(ICurrentUsernameValueResolver<,>), typeof(CurrentUsernameValueResolver<,>));

            //Register API Service Clients

            //Repositories
            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Add Repositories
            services.AddScoped(typeof(IRepository<>), typeof(ApplicationDbRepository<>));

            // Add Additional/Custom repositories here

            foreach (var aggregateRootType in
                typeof(MainModulePlaceHolder).Assembly.GetExportedTypes()
                    .Where(t => typeof(IAggregateRoot).IsAssignableFrom(t) && t.IsClass)
                    .ToList())
            {
                // Add ReadRepositories.
                services.AddScoped(typeof(IReadRepository<>).MakeGenericType(aggregateRootType), sp =>
                    sp.GetRequiredService(typeof(IRepository<>).MakeGenericType(aggregateRootType)));

                // Decorate the repositories with EventAddingRepositoryDecorators and expose them as IRepositoryWithEvents.
                services.AddScoped(typeof(IRepositoryWithEvents<>).MakeGenericType(aggregateRootType), sp =>
                    Activator.CreateInstance(
                        typeof(EventAddingRepositoryDecorator<>).MakeGenericType(aggregateRootType),
                        sp.GetRequiredService(typeof(IRepository<>).MakeGenericType(aggregateRootType)))
                    ?? throw new InvalidOperationException($"Couldn't create EventAddingRepositoryDecorator for aggregateRootType {aggregateRootType.Name}"));
            }

            return services;
        }
    }
}