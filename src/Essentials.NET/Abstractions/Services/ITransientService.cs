using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Essentials.NET.Abstractions.Services;

/// <summary>
/// Marker interface for services that will be registered with transient lifetime to the service collection.
/// </summary>
/// <remarks>
/// If the service implements other interfaces, then it's registered as the implemented interfaces (except <see cref = "ITransientService" />). <br />
/// If the service implements no other interfaces, then it's registered as the concrete type. <br />
/// To register the services, the method <see cref = "Extensions.ServiceExtensions.AddServices(IServiceCollection, Assembly)" /> must be called during application startup. <br />
/// <code>
/// public interface ISomeTransientService : ITransientService;
/// public class SomeTransientService : ISomeTransientService;
/// </code>
/// <code>
/// public interface ISomeTransientService;
/// public class SomeTransientService : ISomeTransientService, ITransientService;
/// </code>
/// </remarks>
public interface ITransientService;
