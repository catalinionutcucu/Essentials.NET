using Essentials.NET.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace Essentials.NET.Configurations;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers as scoped service every implementation of <see cref = "IScopedService" /> to the service collection.
    /// </summary>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddScopedServices(this IServiceCollection serviceCollection, Assembly assembly)
    {
        return serviceCollection.Scan(scan => scan
                                              .FromAssemblies(assembly)
                                              .AddClasses(filter => filter.AssignableTo<IScopedService>(), true)
                                              .UsingRegistrationStrategy(RegistrationStrategy.Append)
                                              .As(type => type.GetInterfaces().Where(implementedInterface => implementedInterface != typeof(IScopedService)))
                                              .AsSelf()
                                              .WithScopedLifetime());
    }

    /// <summary>
    /// Registers as singleton service every implementation of <see cref = "ISingletonService" /> to the service collection.
    /// </summary>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddSingletonServices(this IServiceCollection serviceCollection, Assembly assembly)
    {
        return serviceCollection.Scan(scan => scan
                                              .FromAssemblies(assembly)
                                              .AddClasses(filter => filter.AssignableTo<ISingletonService>(), true)
                                              .UsingRegistrationStrategy(RegistrationStrategy.Append)
                                              .As(type => type.GetInterfaces().Where(implementedInterface => implementedInterface != typeof(ISingletonService)))
                                              .AsSelf()
                                              .WithSingletonLifetime());
    }

    /// <summary>
    /// Registers as transient service every implementation of <see cref = "ITransientService" /> to the service collection.
    /// </summary>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddTransientServices(this IServiceCollection serviceCollection, Assembly assembly)
    {
        return serviceCollection.Scan(scan => scan
                                              .FromAssemblies(assembly)
                                              .AddClasses(filter => filter.AssignableTo<ITransientService>(), true)
                                              .UsingRegistrationStrategy(RegistrationStrategy.Append)
                                              .As(type => type.GetInterfaces().Where(implementedInterface => implementedInterface != typeof(ITransientService)))
                                              .AsSelf()
                                              .WithTransientLifetime());
    }
}
