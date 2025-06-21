using Essentials.NET.Mediator.Abstractions;
using Essentials.NET.Models;

namespace Essentials.NET.Abstractions.Messages;

public interface IQuery<TResult> : IRequest<Result<TResult, Error>>;
