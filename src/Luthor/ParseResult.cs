using System.Runtime.CompilerServices;

namespace Luthor;

public static class ParseResult
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ParseResult<T> Zero<T>(Input input) =>
        new(default!, input, input, false);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ParseResult<T> Value<T>(T value, Input input) =>
        new(value, input, input, true);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ParseResult<T> Value<T>(T value, Input match, Input remainder) =>
        new(value, match, remainder, true);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ParseResult<T> New<T>(T value, Input input, Input remainder, bool hasValue) =>
        new(value, input, remainder, hasValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ParseResult<TOut> CastEmpty<TIn, TOut>(this ParseResult<TIn> result) =>
        new(default!, result.Match, result.Remainder, false);
}
