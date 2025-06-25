using Essentials.NET.Extensions;
using Essentials.NET.Primitives;

namespace Essentials.NET.Entities;

public sealed class OutboxMessage : Entity
{
    public OutboxMessageState State { get; private set; }

    public string MessageType { get; private init; }

    public string MessagePayload { get; private init; }

    public DateTime CreatedOn { get; private init; }

    public DateTime? ProcessedOn { get; private set; }

    public string? ProcessingErrorDetails { get; private set; }

    public bool IsProcessedSuccessfully => State is OutboxMessageState.ProcessedSuccessfully;

    public bool IsProcessedUnsuccessfully => State is OutboxMessageState.ProcessedUnsuccessfully;

    private OutboxMessage() { }

    /// <summary>
    /// Serializes a message to an <see cref = "OutboxMessage" /> instance.
    /// </summary>
    /// <returns>An <see cref = "OutboxMessage" /> instance.</returns>
    public static OutboxMessage SerializeToOutbox<TMessage>(TMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);

        return new OutboxMessage
        {
            State = OutboxMessageState.Created,
            MessageType = typeof(TMessage).FullName,
            MessagePayload = message.ToJsonString(),
            CreatedOn = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Marks the outbox message as processed successfully.
    /// </summary>
    /// <exception cref = "InvalidOperationException">Thrown if the message is already processed successfully.</exception>
    public void MarkAsProcessedSuccessfully()
    {
        if (IsProcessedSuccessfully)
        {
            throw new InvalidOperationException("The outbox message is already processed successfully.");
        }

        State = OutboxMessageState.ProcessedSuccessfully;
        ProcessedOn = DateTime.UtcNow;
        ProcessingErrorDetails = null;
    }

    /// <summary>
    /// Marks the outbox message as processed unsuccessfully.
    /// </summary>
    /// <exception cref = "InvalidOperationException">Thrown if the message is already processed successfully.</exception>
    public void MarkAsProcessedUnsuccessfully(string processingErrorDetails)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(processingErrorDetails);

        if (IsProcessedSuccessfully)
        {
            throw new InvalidOperationException("The outbox message is already processed successfully.");
        }

        State = OutboxMessageState.ProcessedUnsuccessfully;
        ProcessedOn = DateTime.UtcNow;
        ProcessingErrorDetails = processingErrorDetails;
    }
}

public enum OutboxMessageState
{
    Created,
    ProcessedSuccessfully,
    ProcessedUnsuccessfully
}
