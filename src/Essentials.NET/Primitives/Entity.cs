namespace Essentials.NET.Primitives;

public abstract class Entity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
}
