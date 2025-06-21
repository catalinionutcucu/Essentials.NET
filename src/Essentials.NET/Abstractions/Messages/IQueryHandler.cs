using Essentials.NET.Mediator.Abstractions;
using Essentials.NET.Models;

namespace Essentials.NET.Abstractions.Messages;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, Result<TResult, Error>>
    where TQuery : IQuery<TResult>;
