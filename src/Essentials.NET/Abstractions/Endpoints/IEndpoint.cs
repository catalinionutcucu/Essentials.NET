using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Essentials.NET.Abstractions.Endpoints;

/// <summary>
/// Interface for services that will be used to register minimal API endpoints.
/// </summary>
/// <remarks>
/// To register the minimal API endpoints, the methods <see cref = "Extensions.EndpointExtensions.AddEndpoints(IServiceCollection, Assembly)" /> and <see cref = "Extensions.EndpointExtensions.MapEndpoints(IEndpointRouteBuilder)" /> must be called during application startup. <br />
/// <code>
/// public interface SomeEndpoint : IEndpoint
/// {
///     public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
///     {
///         endpointRouteBuilder.MapGet("/", () =>
///         {
///             // implementation
///         });
///     }
/// }
/// </code>
/// <code>
/// public interface SomeEndpoint : IEndpoint
/// {
///     public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
///     {
///         endpointRouteBuilder.MapGet("/", async () =>
///         {
///             // implementation
///         });
///     }
/// }
/// </code>
/// </remarks>
public interface IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder);
}
