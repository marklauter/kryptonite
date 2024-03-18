using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

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
    public readonly int Position;

    public int Length => Source.Length - Position;
    public bool EndOfInput => Position >= Source.Length;

    public static readonly Input Empty = new();

    public Input(ImmutableArray<char> source, int offset)
    {
        ArgumentNullException.ThrowIfNull(source);
        if (offset < 0 || offset > source.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(offset));
        }

        Source = source;
        Position = offset;
    }

    public Input(Input original)
        : this(original.Source, original.Position) { }

    public Input()
        : this([], 0) { }

    private Input(ImmutableArray<char> source)
        : this(source, 0) { }

    public char Peek() => EndOfInput ? Char.MinValue : Source[Position];

    public bool TryPeek(out char value)
    {
        value = Char.MinValue;
        if (EndOfInput)
        {
            return false;
        }

        value = Source[Position];
        return true;
    }

    public char PeekAhead(int offset) => Position + offset >= Source.Length || offset < 0
        ? throw new ArgumentOutOfRangeException(nameof(offset))
        : EndOfInput
            ? Char.MinValue
            : Source[Position + offset];

    public bool TryPeekAhead(int offset, out char value)
    {
        value = Char.MinValue;
        if (offset < 0 || Position + offset >= Source.Length)
        {
            return false;
        }

        if (!EndOfInput)
        {
            value = Source[Position + offset];
        }

        return true;
    }

    public char PeekBehind(int offsetFromEnd) => Position - offsetFromEnd < 0 || offsetFromEnd < 0
        ? throw new ArgumentOutOfRangeException(nameof(offsetFromEnd))
        : EndOfInput
            ? Char.MinValue
            : Source[Position - offsetFromEnd];

    public bool TryPeekBehind(int offsetFromEnd, out char value)
    {
        value = Char.MinValue;
        if (offsetFromEnd < 0 || Position - offsetFromEnd < 0)
        {
            return false;
        }

        if (!EndOfInput)
        {
            value = Source[Position - offsetFromEnd];
        }

        return true;
    }

    public static implicit operator Input(string source) => new([.. source]);

    public ReadOnlySpan<char> AsSpan() => Source.AsSpan(Position..);

    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj is not null &&
        obj is Input input &&
        input == this;

    public override string ToString() => new(Source.AsSpan(Position..));

    public static bool operator ==(Input left, Input right) =>
        left.Position == right.Position &&
        left.Source == right.Source;

    public static bool operator !=(Input left, Input right) => !(left == right);

    public static Input operator +(Input input, int count) => new(input.Source, input.Position + count);

    public static Input operator ++(Input input) => input + 1;

    public static Input operator -(Input input, int count) => new(input.Source, input.Position - count);

    public static Input operator --(Input input) => input - 1;

    public override int GetHashCode() => HashCode.Combine(this, Source, Position);

    public bool Equals(Input other) => this == other;
}
