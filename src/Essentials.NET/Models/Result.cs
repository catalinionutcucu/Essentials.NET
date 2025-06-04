namespace Essentials.NET.Models;

public record Result<TResult, TError>
{
    public ResultState State { get; }

    public TResult? Value { get; }

    public TError? Error { get; }

    public bool IsSuccess => State is ResultState.Success;

    public bool IsFailure => State is ResultState.Failure;

    public Result(TResult value)
    {
        State = ResultState.Success;
        Value = value;
    }

    public Result(TError error)
    {
        ArgumentNullException.ThrowIfNull(error);

        State = ResultState.Failure;
        Error = error;
    }

    /// <summary>
    /// Creates a <see cref = "Result{TResult,TError}" /> instance representing a success result.
    /// </summary>
    /// <returns>A <see cref = "Result{TResult,TError}" /> instance representing a success result.</returns>
    public static Result<TResult, TError> Success(TResult value)
    {
        return new(value);
    }

    /// <summary>
    /// Creates a <see cref = "Result{TResult,TError}" /> instance representing a failure result.
    /// </summary>
    /// <returns>A <see cref = "Result{TResult,TError}" /> instance representing a failure result.</returns>
    public static Result<TResult, TError> Failure(TError error)
    {
        return new(error);
    }

    public static implicit operator Result<TResult, TError>(TResult value)
    {
        return Success(value);
    }

    public static implicit operator Result<TResult, TError>(TError error)
    {
        return Failure(error);
    }
}

public record Result<TError>
{
    public ResultState State { get; }

    public TError? Error { get; }

    public bool IsSuccess => State is ResultState.Success;

    public bool IsFailure => State is ResultState.Failure;

    public Result()
    {
        State = ResultState.Success;
    }

    public Result(TError error)
    {
        ArgumentNullException.ThrowIfNull(error);

        State = ResultState.Failure;
        Error = error;
    }

    /// <summary>
    /// Creates a <see cref = "Result{TError}" /> instance representing a success result.
    /// </summary>
    /// <returns>A <see cref = "Result{TError}" /> instance representing a success result.</returns>
    public static Result<TError> Success()
    {
        return new();
    }

    /// <summary>
    /// Creates a <see cref = "Result{TError}" /> instance representing a failure result.
    /// </summary>
    /// <returns>A <see cref = "Result{TError}" /> instance representing a failure result.</returns>
    public static Result<TError> Failure(TError error)
    {
        return new(error);
    }

    public static implicit operator Result<TError>(TError error)
    {
        return Failure(error);
    }
}

public enum ResultState
{
    Success,
    Failure
}
