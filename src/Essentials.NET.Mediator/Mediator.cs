using Essentials.NET.Mediator.Abstractions.Contracts;
using Essentials.NET.Mediator.Abstractions.Handlers;

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

        var requestType = request.GetType();

        var requestHandlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResult));

        dynamic requestHandler = _serviceProvider.GetService(requestHandlerType);

        if (requestHandler is null)
        {
            throw new InvalidOperationException($"No request handler found for request type '{requestType.FullName}'.");
        }

        return await requestHandler.HandleAsync((dynamic)request, (dynamic)cancellationToken);
    }

    /// <inheritdoc />
    public async Task SendAsync(IRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var requestType = request.GetType();

        var requestHandlerType = typeof(IRequestHandler<>).MakeGenericType(requestType);

        dynamic requestHandler = _serviceProvider.GetService(requestHandlerType);

        if (requestHandler is null)
        {
            throw new InvalidOperationException($"No request handler found for request type '{requestType.FullName}'.");
        }

        await requestHandler.HandleAsync((dynamic)request, (dynamic)cancellationToken);
    }
}
