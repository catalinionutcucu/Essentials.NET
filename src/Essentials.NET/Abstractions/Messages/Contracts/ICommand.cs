using Essentials.NET.Mediator.Abstractions.Contracts;
using Essentials.NET.Models;

namespace Essentials.NET.Abstractions.Messages.Contracts;

public interface ICommand<TResult> : IRequest<Result<TResult, Error>>;

public interface ICommand : IRequest<Result<Error>>;
