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
    /// <exception cref = "InvalidOperationException">Thrown if original message type not found.</exception>
    /// <exception cref = "InvalidCastException">Thrown if the outbox message cannot be deserialized to the original message type.</exception>
    public static object DeserializeFromOutbox(this OutboxMessage outboxMessage, params IEnumerable<Assembly> assemblies)
    {
        ArgumentNullException.ThrowIfNull(outboxMessage);
        ArgumentNullException.ThrowIfNull(assemblies);

        var messageType = assemblies
                          .Where(assembly => assembly is not null)
                          .Select(assembly => assembly.GetType(outboxMessage.MessageType, false, true))
                          .FirstOrDefault(type => type is not null);

        if (messageType is null)
        {
            throw new InvalidOperationException($"No message found for type '{outboxMessage.MessageType}'.");
        }

        try
        {
            return JsonConvert.DeserializeObject(outboxMessage.MessagePayload, messageType);
        }
        catch (JsonException exception)
        {
            throw new InvalidCastException($"Outbox message cannot be deserialized to message type '{outboxMessage.MessageType}'.", exception);
        }
    }
}
