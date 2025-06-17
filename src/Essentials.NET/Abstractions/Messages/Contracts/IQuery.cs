using Essentials.NET.Mediator.Abstractions.Contracts;
using Essentials.NET.Models;

namespace Essentials.NET.Abstractions.Messages.Contracts;

public interface IQuery<TResult> : IRequest<Result<TResult, Error>>;
