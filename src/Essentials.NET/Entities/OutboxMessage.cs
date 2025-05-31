using Essentials.NET.Extensions;
using Essentials.NET.Primitives;

namespace Essentials.NET.Entities;

public sealed class OutboxMessage : Entity
{
    public OutboxMessageState State { get; private set; }

    public string Type { get; private init; }

    public string Content { get; private init; }

    public DateTime CreatedOn { get; private init; }

    public DateTime? ModifiedOn { get; private set; }

    public string? ErrorDetails { get; private set; }

    public bool IsProcessedSuccessfully => State is OutboxMessageState.ProcessedSuccessfully;

    public bool IsProcessedUnsuccessfully => State is OutboxMessageState.ProcessedUnsuccessfully;

    private OutboxMessage() { }

    /// <summary>
    /// Creates an <see cref = "OutboxMessage" /> instance by serializing message object for database persistence.
    /// </summary>
    /// <returns>An <see cref = "OutboxMessage" /> instance.</returns>
    public static OutboxMessage Create<TMessage>(TMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);

        return new OutboxMessage
        {
            State = OutboxMessageState.Created,
            Type = typeof(TMessage).FullName,
            Content = message.ToJsonString(),
            CreatedOn = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Marks the outbox message as processed successfully.
    /// </summary>
    /// <exception cref = "InvalidOperationException">Thrown if the message is already processed.</exception>
    public void MarkAsProcessedSuccessfully()
    {
        if (IsProcessedSuccessfully)
        {
            throw new InvalidOperationException("The outbox message is already processed.");
        }

        State = OutboxMessageState.ProcessedSuccessfully;
        ModifiedOn = DateTime.UtcNow;
        ErrorDetails = null;
    }

    /// <summary>
    /// Marks the outbox message as processed unsuccessfully.
    /// </summary>
    /// <exception cref = "InvalidOperationException">Thrown if the message is already processed.</exception>
    public void MarkAsProcessedUnsuccessfully(string errorDetails)
    {
        if (IsProcessedSuccessfully)
        {
            throw new InvalidOperationException("The outbox message is already processed.");
        }

        State = OutboxMessageState.ProcessedUnsuccessfully;
        ModifiedOn = DateTime.UtcNow;
        ErrorDetails = errorDetails;
    }
}

public enum OutboxMessageState
{
    Created,
    ProcessedSuccessfully,
    ProcessedUnsuccessfully
}
