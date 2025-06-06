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
    /// <exception cref = "InvalidOperationException">Thrown if a request doesn't have matching request handler.</exception>
    public static IServiceCollection AddMediator(this IServiceCollection serviceCollection, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(serviceCollection);
        ArgumentNullException.ThrowIfNull(assembly);

        CheckRequestsForMatchingRequestHandlers(assembly);

        RegisterMediator(serviceCollection, assembly);

        RegisterRequestHanders(serviceCollection, assembly);

        return serviceCollection;
    }

    private static void RegisterMediator(IServiceCollection serviceCollection, Assembly assembly)
    {
        serviceCollection.AddScoped<IMediator, Mediator>();
    }

    private static void RegisterRequestHanders(IServiceCollection serviceCollection, Assembly assembly)
    {
        serviceCollection.Scan(scan => scan
                                       .FromAssemblies(assembly)
                                       .AddClasses(filter => filter.AssignableTo(typeof(IRequestHandler<,>)))
                                       .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                                       .AsImplementedInterfaces()
                                       .WithScopedLifetime());
    }

    private static void CheckRequestsForMatchingRequestHandlers(Assembly assembly)
    {
        // Collection used to store discovered request types
        var requestTypes = new List<RequestType>();

        // Collection used to store discovered request handler types
        var requestHandlerTypes = new List<RequestHandlerType>();

        // Iterates over the types that could be a request or a request handler (non-abstract classes)
        foreach (var type in assembly.GetTypes().Where(type => type is { IsAbstract: false, IsInterface: false }))
        {
            // Iterates over the interfaces implemented by the type
            foreach (var implementedInterface in type.GetInterfaces())
            {
                var implementedInterfaceGenericArguments = implementedInterface.GetGenericArguments();

                if (implementedInterface.Matches(typeof(IRequest<>)))
                {
                    requestTypes.Add(new RequestType
                    {
                        Type = type,
                        ResultType = implementedInterfaceGenericArguments[0]
                    });
                }
                else if (implementedInterface.Matches(typeof(IRequest)))
                {
                    requestTypes.Add(new RequestType
                    {
                        Type = type,
                        ResultType = null
                    });
                }
                else if (implementedInterface.Matches(typeof(IRequestHandler<,>)))
                {
                    requestHandlerTypes.Add(new()
                    {
                        Type = type,
                        RequestType = new()
                        {
                            Type = implementedInterfaceGenericArguments[0],
                            ResultType = implementedInterfaceGenericArguments[1]
                        }
                    });
                }
                else if (implementedInterface.Matches(typeof(IRequestHandler<>)))
                {
                    requestHandlerTypes.Add(new()
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

        // Collection used to store discovered request types without matching request handler type
        var unmatchedRequestTypes = requestTypes
                                    .Where(requestType => !requestHandlerTypes.Any(requestHandlerType => requestHandlerType.RequestType.Equals(requestType)))
                                    .ToList();

        // Thrown if a request doesn't have matching request handler
        if (unmatchedRequestTypes.Any())
        {
            throw new InvalidOperationException(unmatchedRequestTypes.Count == 1 ?
                $"Request handler not found for request type '{unmatchedRequestTypes.First().Type}'." :
                $"Request handlers not found for request types {string.Join(", ", unmatchedRequestTypes.Select(requestType => $"'{requestType.Type}'"))}.");
        }
    }
}
