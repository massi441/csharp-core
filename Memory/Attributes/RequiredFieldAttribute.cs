namespace Core.Memory.Attributes;

/// <summary>
/// An attribute for a field that is always present and of a fixed size.
/// Use <see cref="RequiredSize{T}"/> to compute the size it contributes, which counts
/// towards both the minimum and the maximum size of the containing struct.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
internal sealed class RequiredFieldAttribute : Attribute
{

}
