using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Essentials.NET.Abstractions.Services;

/// <summary>
/// Marker interface for services that will be registered with singleton lifetime to the service collection.
/// </summary>
/// <remarks>
/// If the service implements other interfaces, then it's registered as the implemented interfaces (except <see cref = "ISingletonService" />). <br />
/// If the service implements no other interfaces, then it's registered as the concrete type. <br />
/// To register the services, the method <see cref = "Extensions.ServiceExtensions.AddServices(IServiceCollection, Assembly)" /> must be called during application startup. <br />
/// <code>
/// public interface ISomeSingletonService : ISingletonService;
/// public class SomeSingletonService : ISomeSingletonService;
/// </code>
/// <code>
/// public interface ISomeSingletonService;
/// public class SomeSingletonService : ISomeSingletonService, ISingletonService;
/// </code>
/// </remarks>
public interface ISingletonService;
