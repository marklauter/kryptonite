using System.Runtime.CompilerServices;

namespace Luthor;

public sealed record Segment(
    string Value,
    int Offset)
{
    public Segment(string Segment)
        : this(Segment, 0)
    {
    }

    public int Length => Value.Length;
    public bool EndOfSource => Offset >= Value.Length;

    public static Segment Empty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    } = new(String.Empty, 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Segment Advance()
    {
        return Advance(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Segment Advance(int length)
    {
        return new Segment(Value, Offset + length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public char Peek()
    {
        return EndOfSource
            ? Char.MinValue
            : Value[Offset];
    }
}
