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
    /// <remarks>
    /// If the service implements other interfaces, then it's registered as the implemented interfaces (except <see cref = "IScopedService" />, <see cref = "ISingletonService" />, and <see cref = "ITransientService" />). <br />
    /// If the service implements no other interfaces, then it's registered as the concrete type.
    /// </remarks>
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(serviceCollection);
        ArgumentNullException.ThrowIfNull(assembly);

        RegisterScopedServices(serviceCollection, assembly);
        RegisterSingletonServices(serviceCollection, assembly);
        RegisterTransientServices(serviceCollection, assembly);

        return serviceCollection;
    }

    private static void RegisterScopedServices(IServiceCollection serviceCollection, Assembly assembly)
    {
        serviceCollection.Scan(scan => scan
                                       .FromAssemblies(assembly)
                                       .AddClasses(filter => filter.AssignableTo<IScopedService>(), true)
                                       .UsingRegistrationStrategy(RegistrationStrategy.Append)
                                       .As(type => type
                                                   .GetInterfaces()
                                                   .Where(implementedInterface => implementedInterface != typeof(IScopedService))
                                                   .DefaultIfEmpty(type)
                                                   .ToList())
                                       .WithScopedLifetime());
    }

    private static void RegisterSingletonServices(IServiceCollection serviceCollection, Assembly assembly)
    {
        serviceCollection.Scan(scan => scan
                                       .FromAssemblies(assembly)
                                       .AddClasses(filter => filter.AssignableTo<ISingletonService>(), true)
                                       .UsingRegistrationStrategy(RegistrationStrategy.Append)
                                       .As(type => type
                                                   .GetInterfaces()
                                                   .Where(implementedInterface => implementedInterface != typeof(ISingletonService))
                                                   .DefaultIfEmpty(type)
                                                   .ToList())
                                       .WithSingletonLifetime());
    }

    private static void RegisterTransientServices(IServiceCollection serviceCollection, Assembly assembly)
    {
        serviceCollection.Scan(scan => scan
                                       .FromAssemblies(assembly)
                                       .AddClasses(filter => filter.AssignableTo<ITransientService>(), true)
                                       .UsingRegistrationStrategy(RegistrationStrategy.Append)
                                       .As(type => type
                                                   .GetInterfaces()
                                                   .Where(implementedInterface => implementedInterface != typeof(ITransientService))
                                                   .DefaultIfEmpty(type)
                                                   .ToList())
                                       .WithTransientLifetime());
    }
}
