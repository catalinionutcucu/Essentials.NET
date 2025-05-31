using Essentials.NET.Entities;
using Newtonsoft.Json;
using System.Reflection;

namespace Essentials.NET.Extensions;

public static class OutboxMessageExtensions
{
    /// <summary>
    /// Deserializes the content of an <see cref = "OutboxMessage" /> instance into the original message object.
    /// </summary>
    /// <returns>The original message object.</returns>
    public static object Deserialize(this OutboxMessage outboxMessage, Assembly[] assemblies)
    {
        var messageType = assemblies.Select(assembly => assembly.GetType(outboxMessage.Type, false, false)).FirstOrDefault(type => type is not null);
        var message = JsonConvert.DeserializeObject(outboxMessage.Content, messageType);

        return message;
    }
}
