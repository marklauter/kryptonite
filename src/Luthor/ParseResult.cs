using System.Runtime.CompilerServices;

namespace Luthor;

public sealed class ParseResult
{
    private ParseResult() { }

    private ParseResult(
        Input match,
        Input remainder,
        bool hasValue)
    {
        Match = match;
        Remainder = remainder;
        HasValue = hasValue;
    }

    public Input Match { get; }
    public Input Remainder { get; }
    public bool HasValue { get; }
    public int Length => Remainder.Offset - Match.Offset;

    /// <summary>
    /// returns a ParseResult with Matched = false and does not consume input
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ParseResult Miss(Input input) =>
        new(input, input, false);

    /// <summary>
    /// returns a ParseResult with Matched = true and consumes a single character of input
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ParseResult Hit(Input match) =>
        new(match, match.Advance(1), true);

    /// <summary>
    /// returns a ParseResult with Matched = true and consumes a single character of input
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ParseResult Hit(Input match, Input remainder) =>
        new(match, remainder, true);

    /// <summary>
    /// returns a ParseResult with Matched = true and consumes {length} characters of input
    /// </summary>
    /// <param name="match"></param>
    /// <param name="remainder"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ParseResult Hit(Input match, int length) => length > -1
        ? new ParseResult(match, match.Advance(length), true)
        : throw new InvalidOperationException($"{nameof(length)} must be > 0");

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator string(ParseResult lexeme) => lexeme.ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<char> AsSpan() => Match.AsSpan()[0..Length];

    public override string ToString() => ((string)Match)[0..Length];
};
