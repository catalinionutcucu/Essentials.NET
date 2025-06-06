using Newtonsoft.Json;

namespace Essentials.NET.Extensions;

public static class ObjectExtensions
{
    public static string ToJsonString<TObject>(this TObject? source)
    {
        return source is null ? null : JsonConvert.SerializeObject(source, Formatting.Indented);
    }
}
