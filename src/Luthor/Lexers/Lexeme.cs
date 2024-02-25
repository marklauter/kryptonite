using System.Runtime.CompilerServices;

namespace Luthor.Lexers;

public class Lexeme
{
    protected Lexeme(
        Segment match,
        Segment remainder,
        int length)
    {
        Match = match ?? throw new ArgumentNullException(nameof(match));
        Remainder = remainder ?? throw new ArgumentNullException(nameof(remainder));
        Length = length;
        Success = Length > 0;
    }

    public Segment Match { get; }
    public Segment Remainder { get; }
    public int Length { get; }
    public bool Success { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lexeme Miss(Segment match, Segment remainder)
    {
        return new Lexeme(match, remainder, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lexeme Hit(Segment match, Segment remainder)
    {
        return new Lexeme(match, remainder, 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lexeme Hit(Segment match, Segment remainder, int length)
    {
        return length > 0
            ? new Lexeme(match, remainder, length)
            : throw new ArgumentOutOfRangeException(nameof(length));
    }
};

public sealed class Lexeme<TToken>(Lexeme lexeme, TToken token)
    : Lexeme(
        lexeme?.Match ?? throw new ArgumentNullException(nameof(lexeme)),
        lexeme?.Remainder ?? throw new ArgumentNullException(nameof(lexeme)),
        lexeme?.Length ?? throw new ArgumentNullException(nameof(lexeme)))
    where TToken : Enum
{
    public TToken Token { get; } = token;
    public string Value => Match.Value[Match.Offset..Length];
}
