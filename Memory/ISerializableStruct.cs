namespace Core.Memory;

/// <summary>
/// An interface, or marker interface for a struct that is serialize
/// </summary>
public interface ISerializableStruct
{
    void Serialize(ref SpanWriter writer);
}
