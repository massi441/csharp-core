namespace Core.Memory.Attributes;

/// <summary>
/// An attribute for a field that writes a variable number of elements, such as an enumerator.
/// Use <see cref="RequiredSize{T}"/> to compute the size it contributes: nothing towards the
/// minimum size, since the field may be empty, and <see cref="MaxRepeatCount"/> elements of
/// <see cref="Type"/> towards the maximum size.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class DynamicRepeatedFieldAttribute : Attribute
{
    /// <summary>
    /// The type of a single element the field repeats, which is measured instead of the field type
    /// </summary>
    public required Type Type { get; init; }

    /// <summary>
    /// The largest number of elements the field can write
    /// </summary>
    public required int MaxRepeatCount { get; init; }
}
