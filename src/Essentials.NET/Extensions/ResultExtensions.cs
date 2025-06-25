using Essentials.NET.Models;
using Microsoft.AspNetCore.Http;

namespace Essentials.NET.Extensions;

public static partial class ResultExtensions
{
    /// <summary>
    /// Maps a <see cref = "Result{TResult,TError}" /> instance to a value of type <typeparamref name = "TMap" /> based on the result state.
    /// </summary>
    /// <returns>A value of type <typeparamref name = "TMap" />.</returns>
    public static TMap Map<TMap, TResult, TError>(this Result<TResult, TError> result, Func<TResult, TMap> success, Func<TError, TMap> failure)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(success);
        ArgumentNullException.ThrowIfNull(failure);

        return result.IsSuccess ? success(result.Value) : failure(result.Error);
    }

    /// <summary>
    /// Maps asynchronously a <see cref = "Result{TResult,TError}" /> instance to a value of type <typeparamref name = "TMap" /> based on the result state.
    /// </summary>
    /// <returns>A value of type <typeparamref name = "TMap" />.</returns>
    public static async Task<TMap> MapAsync<TMap, TResult, TError>(this Result<TResult, TError> result, Func<TResult, Task<TMap>> success, Func<TError, Task<TMap>> failure)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(success);
        ArgumentNullException.ThrowIfNull(failure);

        return result.IsSuccess ? await success(result.Value) : await failure(result.Error);
    }

    /// <summary>
    /// Executes an action based on the result state of a <see cref = "Result{TResult,TError}" /> instance.
    /// </summary>
    public static void Execute<TResult, TError>(this Result<TResult, TError> result, Action<TResult> success, Action<TError> failure)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(success);
        ArgumentNullException.ThrowIfNull(failure);

        if (result.IsSuccess)
        {
            success(result.Value);
        }
        else
        {
            failure(result.Error);
        }
    }

    /// <summary>
    /// Executes an asynchronous action based on the result state of a <see cref = "Result{TResult,TError}" /> instance.
    /// </summary>
    public static async Task ExecuteAsync<TResult, TError>(this Result<TResult, TError> result, Func<TResult, Task> success, Func<TError, Task> failure)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(success);
        ArgumentNullException.ThrowIfNull(failure);

        if (result.IsSuccess)
        {
            await success(result.Value);
        }
        else
        {
            await failure(result.Error);
        }
    }

    /// <summary>
    /// Maps a <see cref = "Result{TResult,Error}" /> instance to an HTTP response of type <see cref = "IResult" /> based on the result state.
    /// </summary>
    /// <returns>An HTTP response of type <see cref = "IResult" />.</returns>
    public static IResult ToHttpResponse<TResult>(this Result<TResult, Error> result)
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Map(
            value => value is null ? Results.NoContent() : Results.Ok(value),
            error => error.ToHttpResponse());
    }
}

public static partial class ResultExtensions
{
    /// <summary>
    /// Maps a <see cref = "Result{TError}" /> instance to a value of type <typeparamref name = "TMap" /> based on the result state.
    /// </summary>
    /// <returns>A value of type <typeparamref name = "TMap" />.</returns>
    public static TMap Map<TMap, TError>(this Result<TError> result, Func<TMap> success, Func<TError, TMap> failure)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(success);
        ArgumentNullException.ThrowIfNull(failure);

        return result.IsSuccess ? success() : failure(result.Error);
    }

    /// <summary>
    /// Maps asynchronously a <see cref = "Result{TError}" /> instance to a value of type <typeparamref name = "TMap" /> based on the result state.
    /// </summary>
    /// <returns>A value of type <typeparamref name = "TMap" />.</returns>
    public static async Task<TMap> MapAsync<TMap, TError>(this Result<TError> result, Func<Task<TMap>> success, Func<TError, Task<TMap>> failure)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(success);
        ArgumentNullException.ThrowIfNull(failure);

        return result.IsSuccess ? await success() : await failure(result.Error);
    }

    /// <summary>
    /// Executes an action based on the result state of a <see cref = "Result{TError}" /> instance.
    /// </summary>
    public static void Execute<TError>(this Result<TError> result, Action success, Action<TError> failure)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(success);
        ArgumentNullException.ThrowIfNull(failure);

        if (result.IsSuccess)
        {
            success();
        }
        else
        {
            failure(result.Error);
        }
    }

    /// <summary>
    /// Executes an asynchronous action based on the result state of a <see cref = "Result{TError}" /> instance.
    /// </summary>
    public static async Task ExecuteAsync<TError>(this Result<TError> result, Func<Task> success, Func<TError, Task> failure)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(success);
        ArgumentNullException.ThrowIfNull(failure);

        if (result.IsSuccess)
        {
            await success();
        }
        else
        {
            await failure(result.Error);
        }
    }

    /// <summary>
    /// Maps a <see cref = "Result{Error}" /> instance to an HTTP response of type <see cref = "IResult" /> based on the result state.
    /// </summary>
    /// <returns>An HTTP response of type <see cref = "IResult" />.</returns>
    public static IResult ToHttpResponse(this Result<Error> result)
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Map(
            () => Results.NoContent(),
            error => error.ToHttpResponse());
    }
}
