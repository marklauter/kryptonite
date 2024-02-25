using System.Runtime.CompilerServices;

namespace Luthor.Lexers;

public class Lexeme
{
    protected Lexeme(
        StringSegment match,
        StringSegment remainder,
        int length,
        bool success)
    {
        Match = match ?? throw new ArgumentNullException(nameof(match));
        Remainder = remainder ?? throw new ArgumentNullException(nameof(remainder));
        Length = length;
        Success = success;
    }

    public StringSegment Match { get; }
    public StringSegment Remainder { get; }
    public int Length { get; }
    public bool Success { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lexeme Miss(StringSegment segment) => new(segment, segment, 0, false);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lexeme Hit(StringSegment match)
    {
        var remainder = match.Advance();
        return Hit(match, remainder);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lexeme Hit(StringSegment match, StringSegment remainder) => Hit(match, remainder, match.Length - remainder.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lexeme Hit(StringSegment match, StringSegment remainder, int length) => length > -1
            ? new Lexeme(match, remainder, length, true)
            : throw new ArgumentOutOfRangeException(nameof(length));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator string(Lexeme lexeme) => lexeme.ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<char> AsSpan() => Match.AsSpan()[0..Length];

    public override string ToString() => ((string)Match)[0..Length];
};
