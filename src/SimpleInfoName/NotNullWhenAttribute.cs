#if !NETCOREAPP3_0_OR_GREATER && !NETSTANDARD2_1_OR_GREATER

namespace System.Diagnostics.CodeAnalysis;

[AttributeUsage(AttributeTargets.Parameter)]
sealed class NotNullWhenAttribute :
    Attribute
{
    /// <summary>
    ///   Gets the return value condition.
    ///   If the method returns this value, the associated parameter will not be <see langword="null"/>.
    /// </summary>
    public bool ReturnValue { get; }

    /// <summary>
    ///   Initializes the attribute with the specified return value condition.
    /// </summary>
    public NotNullWhenAttribute(bool returnValue) =>
        ReturnValue = returnValue;
}
#endif