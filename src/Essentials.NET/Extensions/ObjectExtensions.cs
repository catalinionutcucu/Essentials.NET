using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Essentials.NET.Extensions;

public static class ObjectExtensions
{
    public static string ToJsonString<TObject>(this TObject? source)
    {
        return source is null ? null : JsonConvert.SerializeObject(source, Formatting.Indented);
    }
}
