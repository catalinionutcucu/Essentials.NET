namespace Essentials.NET.Models;

#region value result

public class Result<TResult, TError>
{
    public ResultState State { get; }

    public TResult? Value { get; }

    public TError? Error { get; }

    public bool IsSuccess => State is ResultState.Success;

    public bool IsFailure => State is ResultState.Failure;

    protected Result(TResult value)
    {
        State = ResultState.Success;
        Value = value;
    }

    protected Result(TError error)
    {
        State = ResultState.Failure;
        Error = error;
    }

    /// <summary>
    /// Creates an instance of <see cref = "Result{TResult,TError}" /> representing a success result
    /// </summary>
    /// <returns>An instance of <see cref = "Result{TResult,TError}" /> representing a success result</returns>
    public static Result<TResult, TError> Success(TResult value)
    {
        return new(value);
    }

    /// <summary>
    /// Creates an instance of <see cref = "Result{TResult,TError}" /> representing a failure result
    /// </summary>
    /// <returns>An instance of <see cref = "Result{TResult,TError}" /> representing a failure result</returns>
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

#endregion

#region empty result

public class Result<TError>
{
    public ResultState State { get; }

    public TError? Error { get; }

    public bool IsSuccess => State is ResultState.Success;

    public bool IsFailure => State is ResultState.Failure;

    protected Result()
    {
        State = ResultState.Success;
    }

    protected Result(TError error)
    {
        State = ResultState.Failure;
        Error = error;
    }

    /// <summary>
    /// Creates an instance of <see cref = "Result{TError}" /> representing a success result
    /// </summary>
    /// <returns>An instance of <see cref = "Result{TError}" /> representing a success result</returns>
    public static Result<TError> Success()
    {
        return new();
    }

    /// <summary>
    /// Creates an instance of <see cref = "Result{TError}" /> representing a failure result
    /// </summary>
    /// <returns>An instance of <see cref = "Result{TError}" /> representing a failure result</returns>
    public static Result<TError> Failure(TError error)
    {
        return new(error);
    }

    public static implicit operator Result<TError>(TError error)
    {
        return Failure(error);
    }
}

#endregion

public enum ResultState
{
    Success,
    Failure
}
