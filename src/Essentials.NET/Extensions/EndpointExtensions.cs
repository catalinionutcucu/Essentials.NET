﻿using Essentials.NET.Abstractions.Endpoints;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace Essentials.NET.Extensions;

public static class EndpointExtensions
{
    /// <summary>
    /// Registers the endpoints implementing <see cref = "IEndpoint" /> to the service collection.
    /// </summary>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddEndpoints(this IServiceCollection serviceCollection, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(serviceCollection);
        ArgumentNullException.ThrowIfNull(assembly);

        serviceCollection.Scan(scan => scan
                                       .FromAssemblies(assembly)
                                       .AddClasses(filter => filter.AssignableTo<IEndpoint>(), true)
                                       .UsingRegistrationStrategy(RegistrationStrategy.Append)
                                       .As(typeof(IEndpoint))
                                       .WithSingletonLifetime());

        return serviceCollection;
    }

    /// <summary>
    /// Maps the endpoints implementing <see cref = "IEndpoint" /> to the endpoint route builder.
    /// </summary>
    /// <returns>The endpoint route builder.</returns>
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var endpoints = app.ServiceProvider.GetServices<IEndpoint>();

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }

        return app;
    }
}
