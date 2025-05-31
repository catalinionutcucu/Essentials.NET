using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Essentials.NET.Extensions;

public static class ObjectExtensions
{
    public static string ToJsonString(this object source)
    {
        return JsonConvert.SerializeObject(source, Formatting.Indented);
    }

    public static JObject ToJsonObject(this object source)
    {
        return JObject.Parse(source.ToJsonString());
    }
}
