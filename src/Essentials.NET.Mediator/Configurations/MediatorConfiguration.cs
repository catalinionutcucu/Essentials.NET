﻿using Essentials.NET.Mediator.Abstractions.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace Essentials.NET.Mediator.Configurations;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the mediator and the request handlers implementing <see cref = "IRequestHandler{TRequest,TResult}" /> and <see cref = "IRequestHandler{TRequest}" /> to the service collection.
    /// </summary>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddMediator(this IServiceCollection serviceCollection, Assembly assembly)
    {
        serviceCollection.AddScoped<IMediator, Mediator>();

        serviceCollection.Scan(scan => scan
                                       .FromAssemblies(assembly)
                                       .AddClasses(filter => filter.AssignableTo(typeof(IRequestHandler<,>)))
                                       .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                                       .AsImplementedInterfaces()
                                       .WithScopedLifetime());

        serviceCollection.Scan(scan => scan
                                       .FromAssemblies(assembly)
                                       .AddClasses(filter => filter.AssignableTo(typeof(IRequestHandler<>)))
                                       .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                                       .AsImplementedInterfaces()
                                       .WithScopedLifetime());

        return serviceCollection;
    }
}
