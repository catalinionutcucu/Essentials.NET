using Essentials.NET.Mediator.Abstractions.Contracts;
using Essentials.NET.Mediator.Abstractions.Handlers;
using Essentials.NET.Mediator.Models;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace Essentials.NET.Mediator.Extensions;

public static class MediatorExtensions
{
    /// <summary>
    /// Registers the mediator and the request handlers implementing <see cref = "IRequestHandler{TRequest,TResult}" /> and <see cref = "IRequestHandler{TRequest}" />to the service collection.
    /// </summary>
    /// <returns>The service collection.</returns>
    /// <exception cref = "InvalidOperationException">Thrown if a request has no matching request handler.</exception>
    /// <exception cref = "InvalidOperationException">Thrown if a request has multiple matching request handlers.</exception>
    public static IServiceCollection AddMediator(this IServiceCollection serviceCollection, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(serviceCollection);
        ArgumentNullException.ThrowIfNull(assembly);

        CheckRequestsForMatchingRequestHandlers(assembly);

        RegisterMediator(serviceCollection, assembly);

        RegisterRequestHandlers(serviceCollection, assembly);

        return serviceCollection;
    }

    private static void RegisterMediator(IServiceCollection serviceCollection, Assembly assembly)
    {
        serviceCollection.AddScoped<IMediator, Mediator>();
    }

    private static void RegisterRequestHandlers(IServiceCollection serviceCollection, Assembly assembly)
    {
        serviceCollection.Scan(scan => scan
                                       .FromAssemblies(assembly)
                                       .AddClasses(filter => filter.AssignableTo(typeof(IRequestHandler<,>)))
                                       .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                                       .As(type => type
                                                   .GetInterfaces()
                                                   .Where(implementedInterface => AreTypesMatching(implementedInterface, typeof(IRequestHandler<,>), true))
                                                   .ToList())
                                       .WithScopedLifetime());

        serviceCollection.Scan(scan => scan
                                       .FromAssemblies(assembly)
                                       .AddClasses(filter => filter.AssignableTo(typeof(IRequestHandler<>)))
                                       .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                                       .As(type => type
                                                   .GetInterfaces()
                                                   .Where(implementedInterface => AreTypesMatching(implementedInterface, typeof(IRequestHandler<>), true))
                                                   .ToList())
                                       .WithScopedLifetime());
    }

    private static void CheckRequestsForMatchingRequestHandlers(Assembly assembly)
    {
        // Collection used to store discovered request types
        var requests = new List<RequestType>();

        // Collection used to store discovered request handler types
        var requestHandlers = new List<RequestHandlerType>();

        // Iterates over the types that could be a request or a request handler (non-abstract classes)
        foreach (var type in assembly.GetTypes().Where(type => type is { IsAbstract: false, IsInterface: false }))
        {
            // Iterates over the interfaces implemented by the type
            foreach (var implementedInterface in type.GetInterfaces())
            {
                var implementedInterfaceGenericArguments = implementedInterface.GetGenericArguments();

                if (AreTypesMatching(implementedInterface, typeof(IRequest<>), true))
                {
                    requests.Add(new RequestType
                    {
                        Type = type,
                        ResultType = implementedInterfaceGenericArguments[0]
                    });
                }
                else if (AreTypesMatching(implementedInterface, typeof(IRequest), false))
                {
                    requests.Add(new RequestType
                    {
                        Type = type,
                        ResultType = null
                    });
                }
                else if (AreTypesMatching(implementedInterface, typeof(IRequestHandler<,>), true))
                {
                    requestHandlers.Add(new()
                    {
                        Type = type,
                        RequestType = new()
                        {
                            Type = implementedInterfaceGenericArguments[0],
                            ResultType = implementedInterfaceGenericArguments[1]
                        }
                    });
                }
                else if (AreTypesMatching(implementedInterface, typeof(IRequestHandler<>), true))
                {
                    requestHandlers.Add(new()
                    {
                        Type = type,
                        RequestType = new()
                        {
                            Type = implementedInterfaceGenericArguments[0],
                            ResultType = null
                        }
                    });
                }
            }
        }

        // Collection used to store discovered request types with no matching request handler type
        var requestsWithNoRequestHandler = requests
                                           .Where(requestType => !requestHandlers.Any(requestHandlerType => requestHandlerType.RequestType.Equals(requestType)))
                                           .ToList();

        // Collection used to store discovered request types with multiple matching request handler type
        var requestsWithMultipleRequestHandlers = requests
                                                  .Where(requestType => requestHandlers.Count(requestHandlerType => requestHandlerType.RequestType.Equals(requestType)) > 1)
                                                  .ToList();

        // Thrown if a request has no matching request handler
        if (requestsWithNoRequestHandler.Any())
        {
            throw new InvalidOperationException(requestsWithNoRequestHandler.Count == 1 ?
                $"No request handler found for request type '{requestsWithNoRequestHandler.First().Type.FullName}'." :
                $"No request handler found for request types {string.Join(", ", requestsWithNoRequestHandler.Select(requestType => $"'{requestType.Type.FullName}'"))}.");
        }

        // Thrown if a request has multiple matching request handlers
        if (requestsWithMultipleRequestHandlers.Any())
        {
            throw new InvalidOperationException(requestsWithMultipleRequestHandlers.Count == 1 ?
                $"Multiple request handlers found for request type '{requestsWithMultipleRequestHandlers.First().Type.FullName}'." :
                $"Multiple request handlers found for request types {string.Join(", ", requestsWithMultipleRequestHandlers.Select(requestType => $"'{requestType.Type.FullName}'"))}.");
        }
    }

    private static bool AreTypesMatching(Type type1, Type type2, bool ignoreGenericTypeParameters = false)
    {
        if (ignoreGenericTypeParameters && type1.IsGenericType && type2.IsGenericType)
        {
            return type1.GetGenericTypeDefinition() == type2.GetGenericTypeDefinition();
        }

        return type1 == type2;
    }
}
