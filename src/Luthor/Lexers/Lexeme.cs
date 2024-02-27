using System.Runtime.CompilerServices;

namespace Luthor.Lexers;

public class Lexeme
{
    protected Lexeme(
        Input match,
        Input remainder,
        int length,
        bool matched)
    {
        Match = match;
        Remainder = remainder;
        Length = length;
        Matched = matched;
    }

    public Input Match { get; }
    public Input Remainder { get; }
    public int Length { get; }
    public bool Matched { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lexeme Miss(Input input)
    {
        ArgumentNullException.ThrowIfNull(input);

        return new(input, input, 0, false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lexeme Hit(Input match)
    {
        ArgumentNullException.ThrowIfNull(match);

        var remainder = match.Advance();
        var length = match.Length - remainder.Length;

        return length > -1
            ? new Lexeme(match, remainder, length, true)
            : throw new InvalidOperationException($"{nameof(length)} must be > 0");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lexeme Hit(Input match, Input remainder)
    {
        ArgumentNullException.ThrowIfNull(match);
        ArgumentNullException.ThrowIfNull(remainder);

        var length = match.Length - remainder.Length;

        return length > -1
            ? new Lexeme(match, remainder, length, true)
            : throw new InvalidOperationException($"{nameof(length)} must be > 0");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lexeme Hit(Input match, Input remainder, int length)
    {
        ArgumentNullException.ThrowIfNull(match);
        ArgumentNullException.ThrowIfNull(remainder);

        return length > -1
            ? new Lexeme(match, remainder, length, true)
            : throw new InvalidOperationException($"{nameof(length)} must be > 0");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator string(Lexeme lexeme) => lexeme.ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<char> AsSpan() => Match.AsSpan()[0..Length];

    public override string ToString() => ((string)Match)[0..Length];
};
