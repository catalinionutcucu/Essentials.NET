using Essentials.NET.Mediator.Abstractions.Contracts;
using Essentials.NET.Mediator.Abstractions.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Essentials.NET.Mediator;

public sealed class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public async Task<TResult> SendAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        dynamic requestHandler = GetRequestHandler(request);

        return await requestHandler.HandleAsync((dynamic)request, (dynamic)cancellationToken);
    }

    /// <inheritdoc />
    public async Task SendAsync(IRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        dynamic requestHandler = GetRequestHandler(request);

        await requestHandler.HandleAsync((dynamic)request, (dynamic)cancellationToken);
    }

    private dynamic GetRequestHandler<TResult>(IRequest<TResult> request)
    {
        var requestType = request.GetType();

        var requestHandlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResult));

        try
        {
            return _serviceProvider.GetRequiredService(requestHandlerType);
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException($"Request handler not found for request type: '{requestType.Name}'.");
        }
    }

    private dynamic GetRequestHandler(IRequest request)
    {
        var requestType = request.GetType();

        var requestHandlerType = typeof(IRequestHandler<>).MakeGenericType(requestType);

        try
        {
            return _serviceProvider.GetRequiredService(requestHandlerType);
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException($"Request handler not found for request type: '{requestType.Name}'.");
        }
    }
}
