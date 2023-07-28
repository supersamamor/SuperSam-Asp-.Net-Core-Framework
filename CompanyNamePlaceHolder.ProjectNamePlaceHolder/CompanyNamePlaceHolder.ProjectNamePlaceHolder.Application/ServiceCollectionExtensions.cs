using CompanyNamePlaceHolder.Common.Utility.Validators;
using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(CompositeValidator<>));
        return services;
    }
}
