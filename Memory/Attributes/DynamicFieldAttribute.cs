namespace Core.Memory.Attributes;

/// <summary>
/// An attribute for a field of variable length, such as a string or span view.
/// Use <see cref="RequiredSize{T}"/> to compute the size it contributes: the fixed part of the
/// field type counts towards the minimum size, and <see cref="MaxSize"/> is added on top of it
/// for the maximum size.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
internal class DynamicFieldAttribute : Attribute
{
    /// <summary>
    /// The largest payload the field can hold in bytes, excluding the fixed part of the field type
    /// </summary>
    public required ushort MaxSize { get; init; }
}
