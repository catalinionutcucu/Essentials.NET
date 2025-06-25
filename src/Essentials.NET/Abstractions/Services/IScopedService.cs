using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Essentials.NET.Abstractions.Services;

/// <summary>
/// Marker interface for services that will be registered with scoped lifetime to the service collection.
/// </summary>
/// <remarks>
/// If the service implements other interfaces, then it's registered as the implemented interfaces (except <see cref = "IScopedService" />). <br />
/// If the service implements no other interfaces, then it's registered as the concrete type. <br />
/// To register the services, the method <see cref = "Extensions.ServiceExtensions.AddServices(IServiceCollection, Assembly)" /> must be called during application startup. <br />
/// <code>
/// public interface ISomeScopedService : IScopedService;
/// public class SomeScopedService : ISomeScopedService;
/// </code>
/// <code>
/// public interface ISomeScopedService;
/// public class SomeScopedService : ISomeScopedService, IScopedService;
/// </code>
/// </remarks>
public interface IScopedService;
