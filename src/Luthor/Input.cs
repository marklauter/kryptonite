using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Luthor;

/// <summary>
/// immutable reference to a string with an offset to the next char be observed by the lexer
/// </summary>
/// <param name="Value"></param>
/// <param name="Offset"></param>
public readonly struct Input
    : IEquatable<Input>
{
    public readonly char[] Source;
    public readonly int Offset;

    public int Length => Source.Length - Offset;
    public bool EndOfInput => Offset >= Source.Length;

    public static readonly Input Empty = new([], 0);

    public Input(char[] value, int offset)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (offset < 0 || offset > value.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(offset));
        }

        Source = value;
        Offset = offset;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Input() : this([], 0) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Input(char[] value) : this(value, 0) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Input Advance() => Advance(1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Input Advance(int length) => new(Source, Offset + length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public char Peek() => EndOfInput ? Char.MinValue : Source[Offset];

    public bool TryPeek(out char value)
    {
        value = Char.MinValue;
        if (EndOfInput)
        {
            return false;
        }

        value = Source[Offset];
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public char PeekAhead(int offset) => Offset + offset >= Source.Length || offset < 0
        ? throw new ArgumentOutOfRangeException(nameof(offset))
        : EndOfInput
            ? Char.MinValue
            : Source[Offset + offset];

    public bool TryPeekAhead(int offset, out char value)
    {
        value = Char.MinValue;
        if (offset < 0 || Offset + offset >= Source.Length)
        {
            return false;
        }

        if (!EndOfInput)
        {
            value = Source[Offset + offset];
        }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public char PeekBehind(int offsetFromEnd) => Offset - offsetFromEnd < 0 || offsetFromEnd < 0
        ? throw new ArgumentOutOfRangeException(nameof(offsetFromEnd))
        : EndOfInput
            ? Char.MinValue
            : Source[Offset - offsetFromEnd];

    public bool TryPeekBehind(int offsetFromEnd, out char value)
    {
        value = Char.MinValue;
        if (offsetFromEnd < 0 || Offset - offsetFromEnd < 0)
        {
            return false;
        }

        if (!EndOfInput)
        {
            value = Source[Offset - offsetFromEnd];
        }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Input(string value) => new([.. value]);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<char> AsSpan() => Source.AsSpan()[Offset..];

    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj is not null &&
        obj is Input input &&
        input == this;

    public override string ToString() => new(Source[Offset..]);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Input left, Input right) =>
        left.Offset == right.Offset &&
        ReferenceEquals(left.Source, right.Source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Input left, Input right) => !(left == right);

    public override int GetHashCode() => HashCode.Combine(this, Source, Offset);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Input other) => this == other;
}
