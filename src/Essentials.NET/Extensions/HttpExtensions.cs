using Newtonsoft.Json;
using System.Text;

namespace Essentials.NET.Extensions;

public static class HttpExtensions
{
    /// <summary>
    /// Deserializes the content of an HTTP response content to a value of type <typeparamref name = "TValue" />.
    /// </summary>
    /// <returns>A value of type <typeparamref name = "TValue" />.</returns>
    /// <exception cref = "InvalidCastException">Thrown if the HTTP response content cannot be deserialized to type <typeparamref name = "TValue" />.</exception>
    /// <remarks>
    /// This method is intended for HTTP responses with the header <c>Content-Type</c> set to <c>application/json</c> .
    /// </remarks>
    public static async Task<TValue?> ReadAsAsync<TValue>(this HttpContent httpContent)
    {
        var jsonString = await httpContent.ReadAsStringAsync();

        try
        {
            return JsonConvert.DeserializeObject<TValue>(jsonString);
        }
        catch (JsonException exception)
        {
            throw new InvalidCastException($"HTTP response content cannot be deserialized to type '{typeof(TValue).FullName}'.", exception);
        }
    }

    /// <summary>
    /// Sends asynchronously a POST request to the specified URI with request content as plain text.
    /// </summary>
    /// <returns>The response of the HTTP POST request.</returns>
    /// <remarks>
    /// This method sets the header <c>Content-Type</c> to <c>text/plain</c> and uses <c>UTF-8</c> encoding for the request content. <br />
    /// This method is a convenience wrapper around <see cref = "HttpClient.PostAsync(string?, HttpContent?, CancellationToken)" />.
    /// </remarks>
    public static async Task<HttpResponseMessage> PostStringAsync(this HttpClient httpClient, string? requestUri, string? requestContent, CancellationToken cancellationToken = default)
    {
        return await httpClient.PostAsync(requestUri, new StringContent(requestContent, Encoding.UTF8, "text/plain"), cancellationToken);
    }

    /// <summary>
    /// Sends asynchronously a POST request to the specified URI with request content as JSON.
    /// </summary>
    /// <returns>The response of the HTTP POST request.</returns>
    /// <remarks>
    /// This method sets the header <c>Content-Type</c> to <c>application/json</c> and uses <c>UTF-8</c> encoding for the request content. <br />
    /// This method is a convenience wrapper around <see cref = "HttpClient.PostAsync(string?, HttpContent?, CancellationToken)" />.
    /// </remarks>
    public static async Task<HttpResponseMessage> PostJsonAsync<TRequestContent>(this HttpClient httpClient, string? requestUri, TRequestContent? requestContent, CancellationToken cancellationToken = default)
    {
        return await httpClient.PostAsync(requestUri, new StringContent(requestContent.ToJsonString(), Encoding.UTF8, "application/json"), cancellationToken);
    }

    /// <summary>
    /// Sends asynchronously a PUT request to the specified URI with string request content.
    /// </summary>
    /// <returns>The response of the HTTP PUT request.</returns>
    /// <remarks>
    /// This method sets the header <c>Content-Type</c> to <c>text/plain</c> and uses <c>UTF-8</c> encoding for the request content. <br />
    /// This method is a convenience wrapper around <see cref = "HttpClient.PutAsync(string?, HttpContent?, CancellationToken)" />.
    /// </remarks>
    public static async Task<HttpResponseMessage> PutStringAsync(this HttpClient httpClient, string? requestUri, string? requestContent, CancellationToken cancellationToken = default)
    {
        return await httpClient.PutAsync(requestUri, new StringContent(requestContent, Encoding.UTF8, "text/plain"), cancellationToken);
    }

    /// <summary>
    /// Sends asynchronously a PUT request to the specified URI with JSON request content.
    /// </summary>
    /// <returns>The response of the HTTP PUT request.</returns>
    /// <remarks>
    /// This method sets the header <c>Content-Type</c> to <c>application/json</c> and uses <c>UTF-8</c> encoding for the request content. <br />
    /// This method is a convenience wrapper around <see cref = "HttpClient.PutAsync(string?, HttpContent?, CancellationToken)" />.
    /// </remarks>
    public static async Task<HttpResponseMessage> PutJsonAsync<TRequestContent>(this HttpClient httpClient, string? requestUri, TRequestContent? requestContent, CancellationToken cancellationToken = default)
    {
        return await httpClient.PutAsync(requestUri, new StringContent(requestContent.ToJsonString(), Encoding.UTF8, "application/json"), cancellationToken);
    }
}
