using Essentials.NET.Abstractions.Requests.Contracts;
using Essentials.NET.Mediator.Abstractions.Handlers;
using Essentials.NET.Models;

namespace Essentials.NET.Abstractions.Requests.Handlers;

public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, Result<TResult, Error>>
    where TCommand : ICommand<TResult>;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result<Error>>
    where TCommand : ICommand;
