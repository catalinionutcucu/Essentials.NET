namespace Essentials.NET.Mediator.Extensions;

internal static class TypeExtensions
{
    internal static bool Matches(this Type type, Type expectedType)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == expectedType || type == expectedType;
    }
}
