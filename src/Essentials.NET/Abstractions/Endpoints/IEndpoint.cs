using Microsoft.AspNetCore.Routing;

namespace Essentials.NET.Abstractions.Endpoints;

public interface IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder);
}
