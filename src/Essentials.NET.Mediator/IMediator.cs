using Essentials.NET.Mediator.Abstractions.Contracts;
using Essentials.NET.Mediator.Abstractions.Handlers;

namespace Essentials.NET.Mediator;

public interface IMediator
{
    /// <summary>
    /// Sends a request of type <see cref = "IRequest{TResult}" /> to the corresponding handler of type <see cref = "IRequestHandler{TRequest,TResult}" />.
    /// </summary>
    /// <returns>A value of type <typeparamref name = "TResult" /> representing the result of the request.</returns>
    /// <exception cref = "InvalidOperationException">Thrown if a request has no matching request handler.</exception>
    public Task<TResult> SendAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a request of type <see cref = "IRequest" /> to the corresponding handler of type <see cref = "IRequestHandler{IRequest}" />.
    /// </summary>
    /// <exception cref = "InvalidOperationException">Thrown if a request has no matching request handler.</exception>
    public Task SendAsync(IRequest request, CancellationToken cancellationToken = default);
}
