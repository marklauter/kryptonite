namespace Luthor;

public static class Result
{
    public static Result<T> Empty<T>(Input input) =>
        new(default!, input, false, 0);

    public static Result<T> WithValue<T>(T value, Input input) =>
        new(value, input, true, 0);

    public static Result<T> WithValue<T>(T value, Input input, int length) =>
        new(value, input, true, length);

    public static Result<T> New<T>(T value, Input input, bool hasValue, int length) =>
        new(value, input, hasValue, length);

    public static Result<TOut> CastEmpty<TIn, TOut>(this Result<TIn> result) =>
        Empty<TOut>(result.Remainder);
}

public sealed class Result<T>(
    T value,
    Input input,
    bool hasValue,
    int length)
{
    public Result(T value, Input input, bool hasValue)
        : this(value, input, hasValue, 0)
    {
    }

    // instead of allocating another input for location, we just track it with match offset
    public int MatchOffset { get; } = input.Offset;
    public Input Remainder { get; } =
        length > 0
            ? input.Advance(length)
            : input;

    public T Value { get; } = value;

    public bool HasValue { get; } = hasValue;

    public int Length { get; } = length;

    public static explicit operator string(Result<T> result) =>
        !result.HasValue
            ? String.Empty
            : result.Length > 0
                ? result.Remainder.Value[result.MatchOffset..result.Length]
                : result.Value?.ToString() ?? String.Empty;
}
