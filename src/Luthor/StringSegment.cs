using System.Runtime.CompilerServices;

namespace Luthor;

/// <summary>
/// immutable reference to a string with an offset pointer to the character to be observed by the lexer
/// </summary>
/// <param name="Value"></param>
/// <param name="Offset"></param>
public sealed record StringSegment(
    string Value,
    int Offset)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public StringSegment(string segment)
        : this(segment, 0)
    { }

    public int Length => Value.Length - Offset;
    public bool EndOfSegment => Offset >= Value.Length;

    public static StringSegment Empty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    } = new(String.Empty, 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public StringSegment Advance()
    {
        return Advance(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public StringSegment Advance(int length)
    {
        return new StringSegment(Value, Offset + length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public char Look()
    {
        return EndOfSegment
            ? Char.MinValue
            : Value[Offset];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public char LookAhead(int offset)
    {
        return Offset + offset >= Value.Length || offset < 0
            ? throw new ArgumentOutOfRangeException(nameof(offset))
            : EndOfSegment
                ? Char.MinValue
                : Value[Offset + offset];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public char LookBack(int offset)
    {
        return Offset - offset < 0 || offset < 0
            ? throw new ArgumentOutOfRangeException(nameof(offset))
            : EndOfSegment
                ? Char.MinValue
                : Value[Offset - offset];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator StringSegment(string value)
    {
        return new(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator string(StringSegment segment)
    {
        return segment.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<char> AsSpan()
    {
        return Value.AsSpan(Offset);
    }

    public override string ToString()
    {
        return Value[Offset..];
    }
}
