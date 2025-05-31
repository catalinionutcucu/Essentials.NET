using Essentials.NET.Mediator.Abstractions.Contracts;
using Essentials.NET.Models;

namespace Essentials.NET.Abstractions.Requests.Contracts;

public interface ICommand<TResult> : IRequest<Result<TResult, Error>>;

public interface ICommand : IRequest<Result<Error>>;
