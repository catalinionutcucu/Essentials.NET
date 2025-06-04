using Essentials.NET.Abstractions.Requests.Contracts;
using Essentials.NET.Mediator.Abstractions.Handlers;
using Essentials.NET.Models;

namespace Essentials.NET.Abstractions.Requests.Handlers;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, Result<TResult, Error>>
    where TQuery : IQuery<TResult>;
