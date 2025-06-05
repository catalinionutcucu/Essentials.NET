using Essentials.NET.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace Essentials.NET.Extensions;

public static class ServiceExtensions
{
    /// <summary>
    /// Registers as scoped service every implementation of <see cref = "IScopedService" /> to the service collection. <br />
    /// Registers as singleton service every implementation of <see cref = "ISingletonService" /> to the service collection. <br />
    /// Registers as transient service every implementation of <see cref = "ITransientService" /> to the service collection.
    /// </summary>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(serviceCollection);
        ArgumentNullException.ThrowIfNull(assembly);

        serviceCollection.RegisterScopedServices(assembly)
                         .RegisterSingletonServices(assembly)
                         .RegisterTransientServices(assembly);

        return serviceCollection;
    }

    /// <summary>
    /// Registers as scoped service every implementation of <see cref = "IScopedService" /> to the service collection.
    /// </summary>
    /// <returns>The service collection.</returns>
    public static IServiceCollection RegisterScopedServices(this IServiceCollection serviceCollection, Assembly assembly)
    {
        serviceCollection.Scan(scan => scan
                                       .FromAssemblies(assembly)
                                       .AddClasses(filter => filter.AssignableTo<IScopedService>(), true)
                                       .UsingRegistrationStrategy(RegistrationStrategy.Append)
                                       .As(type => type.GetInterfaces().Where(implementedInterface => implementedInterface != typeof(IScopedService)))
                                       .AsSelf()
                                       .WithScopedLifetime());

        return serviceCollection;
    }

    /// <summary>
    /// Registers as singleton service every implementation of <see cref = "ISingletonService" /> to the service collection.
    /// </summary>
    /// <returns>The service collection.</returns>
    public static IServiceCollection RegisterSingletonServices(this IServiceCollection serviceCollection, Assembly assembly)
    {
        serviceCollection.Scan(scan => scan
                                       .FromAssemblies(assembly)
                                       .AddClasses(filter => filter.AssignableTo<ISingletonService>(), true)
                                       .UsingRegistrationStrategy(RegistrationStrategy.Append)
                                       .As(type => type.GetInterfaces().Where(implementedInterface => implementedInterface != typeof(ISingletonService)))
                                       .AsSelf()
                                       .WithSingletonLifetime());

        return serviceCollection;
    }

    /// <summary>
    /// Registers as transient service every implementation of <see cref = "ITransientService" /> to the service collection.
    /// </summary>
    /// <returns>The service collection.</returns>
    public static IServiceCollection RegisterTransientServices(this IServiceCollection serviceCollection, Assembly assembly)
    {
        serviceCollection.Scan(scan => scan
                                       .FromAssemblies(assembly)
                                       .AddClasses(filter => filter.AssignableTo<ITransientService>(), true)
                                       .UsingRegistrationStrategy(RegistrationStrategy.Append)
                                       .As(type => type.GetInterfaces().Where(implementedInterface => implementedInterface != typeof(ITransientService)))
                                       .AsSelf()
                                       .WithTransientLifetime());

        return serviceCollection;
    }
}
