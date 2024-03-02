namespace Luthor;

public readonly struct Result<T>(
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

    public static explicit operator string(Result<T> result) =>
        !result.HasValue
            ? String.Empty
            : result.Length > 0
                ? result.Remainder.Value[result.Match.Offset..result.Length].ToString() ?? String.Empty
                : result.Value?.ToString() ?? String.Empty;
}
