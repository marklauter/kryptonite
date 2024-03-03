using System.Runtime.CompilerServices;

namespace Luthor;

public static class Result
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> Empty<T>(Input input) =>
        new(default!, input, input, false);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> WithValue<T>(T value, Input input) =>
        new(value, input, input, true);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> WithValue<T>(T value, Input match, Input remainder) =>
        new(value, match, remainder, true);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> New<T>(T value, Input input, Input remainder, bool hasValue) =>
        new(value, input, remainder, hasValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOut> CastEmpty<TIn, TOut>(this Result<TIn> result) =>
        new(default!, result.Match, result.Remainder, false);
}
