namespace Luthor;

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

    public static Lexeme Miss(Segment match, Segment remainder)
    {
        return new Lexeme(match, remainder, 0);
    }

    public static Lexeme Hit(Segment match, Segment remainder, int length)
    {
        return length > 0
            ? new Lexeme(match, remainder, length)
            : throw new ArgumentOutOfRangeException(nameof(length));
    }
};

public sealed class Lexeme<TToken>
    : Lexeme
    where TToken : Enum
{
    private Lexeme(
        Segment match,
        Segment remainder,
        int length,
        TToken token)
        : base(match, remainder, length)
    {
        this.Token = token;
    }

    public TToken Token { get; }
    public string Value => Match.Value[Match.Offset..Length];

    public static Lexeme<TToken> Miss(
        Segment match,
        Segment remainder,
        TToken token)
    {
        return new Lexeme<TToken>(match, remainder, 0, token);
    }

    public static Lexeme<TToken> Hit(
        Segment match,
        Segment remainder,
        TToken token,
        int length)
    {
        return length > 0
            ? new Lexeme<TToken>(match, remainder, length, token)
            : throw new ArgumentOutOfRangeException(nameof(length));
    }
}
