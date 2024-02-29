namespace Luthor;

public static class Result
{
    public static Result<T> Empty<T>(Input input) =>
        new(default!, input, input, false);

    public static Result<T> WithValue<T>(T value, Input input) =>
        new(value, input, input, true);

    public static Result<T> WithValue<T>(T value, Input match, Input remainder) =>
        new(value, match, remainder, true);

    public static Result<T> New<T>(T value, Input input, Input remainder, bool hasValue) =>
        new(value, input, remainder, hasValue);

    public static Result<TOut> CastEmpty<TIn, TOut>(this Result<TIn> result) =>
        Empty<TOut>(result.Remainder);
}

public sealed record Result<T>(
    T Value,
    Input Match,
    Input Remainder,
    bool HasValue)
{
    public int Length => Remainder.Offset - Match.Offset;

    public static explicit operator string(Result<T> result) =>
        !result.HasValue
            ? String.Empty
            : result.Length > 0
                ? result.Remainder.Value[result.MatchOffset..result.Length]
                : result.Value?.ToString() ?? String.Empty;
}
