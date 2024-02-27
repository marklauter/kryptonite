using System.Runtime.CompilerServices;

namespace Luthor;

/// <summary>
/// immutable reference to a string with an offset pointer to the character to be observed by the lexer
/// </summary>
/// <param name="Value"></param>
/// <param name="Offset"></param>
public readonly record struct Input(
    string Value,
    int Offset)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Input(string value)
        : this(value, 0)
    { }

    public int Length => Value.Length - Offset;
    public bool EndOfInput => Offset >= Value.Length;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Input Advance() => Advance(1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Input Advance(int length) => new(Value, Offset + length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public char Look() => EndOfInput
        ? Char.MinValue
        : Value[Offset];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryLook(out char value)
    {
        value = Char.MinValue;
        if (EndOfInput)
        {
            return false;
        }

        value = Value[Offset];
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public char LookAhead(int offset) => Offset + offset >= Value.Length || offset < 0
        ? throw new ArgumentOutOfRangeException(nameof(offset))
        : EndOfInput
            ? Char.MinValue
            : Value[Offset + offset];

    public bool TryLookAhead(int offset, out char value)
    {
        value = Char.MinValue;
        if (Offset + offset >= Value.Length || offset < 0)
        {
            return false;
        }

        if (!EndOfInput)
        {
            value = Value[Offset + offset];
        }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public char LookBack(int offset) => Offset - offset < 0 || offset < 0
        ? throw new ArgumentOutOfRangeException(nameof(offset))
        : EndOfInput
            ? Char.MinValue
            : Value[Offset - offset];

    public bool TryLookBack(int offset, out char value)
    {
        value = Char.MinValue;
        if (Offset - offset < 0 || offset < 0)
        {
            return false;
        }

        if (!EndOfInput)
        {
            value = Value[Offset - offset];
        }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Input(string value) => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator string(Input segment) => segment.ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<char> AsSpan() => Value.AsSpan(Offset);

    public override string ToString() => Value[Offset..];
}
