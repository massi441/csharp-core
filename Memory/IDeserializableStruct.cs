namespace Core.Memory;

/// <summary>
/// An interface, or a marker interface for a struct that is deserializable
/// </summary>
public interface IDeserializableStruct
{
    void Deserialize(ref SpanReader reader);
}
