using System.Runtime.CompilerServices;

namespace Luthor.Lexers;

public class Lexeme
{
    protected Lexeme(
        StringSegment match,
        StringSegment remainder,
        int length)
    {
        Match = match ?? throw new ArgumentNullException(nameof(match));
        Remainder = remainder ?? throw new ArgumentNullException(nameof(remainder));
        Length = length;
        Success = Length > 0;
    }

    public StringSegment Match { get; }
    public StringSegment Remainder { get; }
    public int Length { get; }
    public bool Success { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lexeme Miss(StringSegment segment)
    {
        return new Lexeme(segment, segment, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lexeme Hit(StringSegment match, StringSegment remainder)
    {
        return new Lexeme(match, remainder, match.Length - remainder.Length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lexeme Hit(StringSegment match, StringSegment remainder, int length)
    {
        return length > 0
            ? new Lexeme(match, remainder, length)
            : throw new ArgumentOutOfRangeException(nameof(length));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator string(Lexeme lexeme)
    {
        return lexeme.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<char> AsSpan()
    {
        return Match.AsSpan()[0..Length];
    }

    public override string ToString()
    {
        return ((string)Match)[0..Length];
    }
};
