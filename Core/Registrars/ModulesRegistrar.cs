using System.Reflection;
using Core.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Unify.Data.Repositories;

namespace Core.Registrars;

public static class ModulesRegistrar
{
    public static IServiceCollection RegisterBackOfficeModules(this IServiceCollection services)
    {
        services.AddScoped(typeof(Repository<>));

        var assembly = Assembly.GetExecutingAssembly();
        var modules = assembly.GetTypes()
            .Where(type => type.GetCustomAttribute<BackOfficeModuleAttribute>() != null)
            .ToList();

        foreach (var module in modules)
            services.AddTransient(module);

        return services;
    }

    public static IServiceCollection RegisterOnboardingModules(this IServiceCollection services)
    {
        services.AddScoped(typeof(Repository<>));

        var assembly = Assembly.GetExecutingAssembly();
        var modules = assembly.GetTypes()
            .Where(type => type.GetCustomAttribute<OnboardingModuleAttribute>() != null)
            .ToList();

        foreach (var module in modules)
            services.AddTransient(module);

        return services;
    }
}
