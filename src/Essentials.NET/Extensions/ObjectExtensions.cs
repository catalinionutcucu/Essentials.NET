using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Essentials.NET.Extensions;

public static class ObjectExtensions
{
    public static string ToJsonString<TObject>(this TObject? source)
        where TObject : class
    {
        return source is null ? null : JsonConvert.SerializeObject(source, Formatting.Indented);
    }

    public static JObject ToJsonObject<TObject>(this TObject? source)
        where TObject : class
    {
        return source is null ? null : JObject.Parse(source.ToJsonString());
    }
}
