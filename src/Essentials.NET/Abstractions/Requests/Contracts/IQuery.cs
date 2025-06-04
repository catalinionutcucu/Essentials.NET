using Essentials.NET.Mediator.Abstractions.Contracts;
using Essentials.NET.Models;

namespace Essentials.NET.Abstractions.Requests.Contracts;

public interface IQuery<TResult> : IRequest<Result<TResult, Error>>;
