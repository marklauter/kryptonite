using System.Collections.Immutable;
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
    public readonly ImmutableArray<char> Source;
    public readonly int Offset;

    public int Length => Source.Length - Offset;
    public bool EndOfInput => Offset >= Source.Length;

    public static readonly Input Empty = new();

    public Input(ImmutableArray<char> source, int offset)
    {
        ArgumentNullException.ThrowIfNull(source);
        if (offset < 0 || offset > source.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(offset));
        }

        Source = source;
        Offset = offset;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Input(Input original)
        : this(original.Source, original.Offset) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Input()
        : this([], 0) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Input(ImmutableArray<char> source)
        : this(source, 0) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Input Advance() => Advance(1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Input Advance(int length) => new(Source, Offset + length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Input Backtrack() => Backtrack(1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Input Backtrack(int length) => new(Source, Offset - length);

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
    public static implicit operator Input(string source) =>
        new([.. source]);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<char> AsSpan() => Source.AsSpan(Offset..);

    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj is not null &&
        obj is Input input &&
        input == this;

    public override string ToString() => new(Source.AsSpan(Offset..));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Input left, Input right) =>
        left.Offset == right.Offset &&
        left.Source == right.Source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Input left, Input right) => !(left == right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Input operator +(Input input, int length) => input.Advance(length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Input operator -(Input input, int length) => input.Backtrack(length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Input operator ++(Input input) => input.Advance();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Input operator --(Input input) => input.Backtrack();

    public override int GetHashCode() => HashCode.Combine(this, Source, Offset);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Input other) => this == other;
}
