using Newtonsoft.Json.Linq;

namespace Essentials.NET.Extensions;

public static class JsonObjectExtensions
{
    public static bool HasValue(this JObject jsonObject, string path)
    {
        ArgumentNullException.ThrowIfNull(jsonObject);
        ArgumentNullException.ThrowIfNull(path);

        return jsonObject.SelectToken(path) is not null;
    }

    public static TValue? GetValue<TValue>(this JObject jsonObject, string path)
    {
        ArgumentNullException.ThrowIfNull(jsonObject);
        ArgumentNullException.ThrowIfNull(path);

        var jsonToken = jsonObject.SelectToken(path);

        if (jsonToken is null)
        {
            return default;
        }

        try
        {
            return jsonToken.ToObject<TValue>();
        }
        catch (Exception exception)
        {
            throw new InvalidCastException($"Value of type '{jsonToken.Type}' cannot be cast to type '{typeof(TValue).FullName}'.", exception);
        }
    }

    public static bool TryGetValue<TValue>(this JObject jsonObject, string path, out TValue? value)
    {
        ArgumentNullException.ThrowIfNull(jsonObject);
        ArgumentNullException.ThrowIfNull(path);

        var jsonToken = jsonObject.SelectToken(path);

        if (jsonToken == null)
        {
            value = default;

            return false;
        }

        try
        {
            value = jsonToken.ToObject<TValue>();

            return true;
        }
        catch
        {
            value = default;

            return false;
        }
    }
}
