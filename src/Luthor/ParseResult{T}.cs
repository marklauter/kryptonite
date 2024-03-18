using System.Runtime.CompilerServices;

namespace Luthor;

public readonly struct ParseResult<T>(
    T value,
    Input match,
    Input remainder,
    bool hasValue)
{
    public readonly T Value = value;
    public readonly Input Match = match;
    public readonly Input Remainder = remainder;
    public readonly bool HasValue = hasValue;

    public int Length => Remainder.Position - Match.Position;
    public int Offset => Match.Position;

    public ReadOnlySpan<char> AsSpan() => Match.AsSpan()[..Length];

    public override string ToString() => AsSpan().ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator string(ParseResult<T> result) =>
        !result.HasValue
            ? String.Empty
            : result.Length > 0
                ? result.AsSpan().ToString()
                : result.Value?.ToString() ?? String.Empty;
}
