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
