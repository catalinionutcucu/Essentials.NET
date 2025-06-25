﻿using Essentials.NET.Mediator.Abstractions.Contracts;

namespace Essentials.NET.Mediator.Abstractions.Handlers;

public interface IRequestHandler<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    public Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}

public interface IRequestHandler<TRequest>
    where TRequest : IRequest
{
    public Task HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}
