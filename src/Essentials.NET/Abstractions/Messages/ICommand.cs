using Essentials.NET.Mediator.Abstractions;
using Essentials.NET.Models;

namespace Essentials.NET.Abstractions.Messages;

public interface ICommand<TResult> : IRequest<Result<TResult, Error>>;

public interface ICommand : IRequest<Result<Error>>;
