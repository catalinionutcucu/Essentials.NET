using Essentials.NET.Abstractions.Messages.Contracts;
using Essentials.NET.Mediator.Abstractions.Handlers;
using Essentials.NET.Models;

namespace Essentials.NET.Abstractions.Messages.Handlers;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, Result<TResult, Error>>
    where TQuery : IQuery<TResult>;
