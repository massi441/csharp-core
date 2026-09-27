using System.Numerics;
using System.Runtime.CompilerServices;
using Core.Memory.Attributes;

namespace Core.Memory;

/// <summary>
/// Represents a length-prefixed view of contiguous elements in a raw memory stream
/// </summary>
/// <typeparam name="TLengthPrefix">The type of the length prefix (must be of intergral type)</typeparam>
/// <typeparam name="T">The type of element stored in the stream</typeparam>
internal ref struct StreamSpanView<TLengthPrefix, T> 
    where TLengthPrefix : unmanaged, IBinaryInteger<TLengthPrefix>
    where T : unmanaged
{
    [RequiredField]
    private TLengthPrefix _length;
    private ReadOnlySpan<T> _span;

    public readonly TLengthPrefix Length => _length;
    public readonly ReadOnlySpan<T> Span => _span;

    public StreamSpanView(TLengthPrefix length, Span<T> span)
    {
        _length = length;
        _span = span;
    }

    public readonly void Serialize(Span<byte> destination)
    {
        SpanWriter writer = new SpanWriter(destination);

        writer.Write(Length);
        writer.WriteSpan(Span);
    }

    public readonly void Serialize(ref SpanWriter writer)
    {
        writer.Write(Length);
        writer.WriteSpan(Span);
    }

    public void Deserialize(ref SpanReader reader, TLengthPrefix maxReadLength)
    {
        _length = reader.Read<TLengthPrefix>();
        if (_length > maxReadLength)
        {
            throw new InvalidDataException($"The span length prefix ({_length}) was bigger than the maximum size allowed ({maxReadLength})");
        }

        int bytesToRead = int.CreateChecked(_length) * Unsafe.SizeOf<T>();
        if (bytesToRead > reader.RemainingByteCount)
        {
            throw new InvalidDataException($"The span length prefix ({bytesToRead}) was bigger than the remaining bytes ({reader.RemainingByteCount}) in the reader");
        }

        _span = reader.ReadView<T>(int.CreateChecked(Length));
    }

    public readonly bool HasData()
    {
        return int.CreateChecked(_length) > 0;
    }
}
