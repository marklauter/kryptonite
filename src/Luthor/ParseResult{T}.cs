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

    public int Length => Remainder.Offset - Match.Offset;
    public int Offset => Match.Offset;

    public override string ToString() => (string)this;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator string(ParseResult<T> result) =>
        !result.HasValue
            ? String.Empty
            : result.Length > 0
                ? new string(result.Match.Source[result.Match.Offset..result.Remainder.Offset])
                : result.Value?.ToString() ?? String.Empty;
}
