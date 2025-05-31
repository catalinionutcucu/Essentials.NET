using Essentials.NET.Abstractions.Endpoints;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace Essentials.NET.Configurations;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the endpoints implementing <see cref = "IEndpoint" /> to the service collection.
    /// </summary>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddEndpoints(this IServiceCollection serviceCollection, Assembly assembly)
    {
        return serviceCollection.Scan(scan => scan
                                              .FromAssemblies(assembly)
                                              .AddClasses(filter => filter.AssignableTo<IEndpoint>(), true)
                                              .UsingRegistrationStrategy(RegistrationStrategy.Append)
                                              .As(type => type.GetInterfaces().Where(implementedInterface => implementedInterface == typeof(IEndpoint)))
                                              .WithSingletonLifetime());
    }
}

public static class EndpointRouteBuilderExtensions
{
    /// <summary>
    /// Maps the endpoints implementing <see cref = "IEndpoint" /> to the endpoint route builder.
    /// </summary>
    /// <returns>The endpoint route builder.</returns>
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.ServiceProvider.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }

        return app;
    }
}
