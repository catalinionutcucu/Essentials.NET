using Essentials.NET.Mediator.Abstractions;
using Essentials.NET.Models;

namespace Essentials.NET.Abstractions.Messages;

public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, Result<TResult, Error>>
    where TCommand : ICommand<TResult>;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result<Error>>
    where TCommand : ICommand;
