namespace Luthor;

public delegate Lexeme Lexer(Segment segment);

public sealed class Lexer<TToken>(
    Lexer lexer,
    TToken token,
    bool ignored)
    where TToken : Enum
{
    private readonly Lexer lexer = lexer ?? throw new ArgumentNullException(nameof(lexer));
    private readonly TToken token = token;
    private readonly bool ignored = ignored;

    public Lexer(Lexer lexer, TToken token)
        : this(lexer, token, false)
    { }

    public sealed record MatchResult(
        TToken Token,
        bool Ignored,
        bool Success,
        int Offset,
        int Length)
    {
        internal static MatchResult Hit(
            TToken token,
            bool ignored,
            int offset,
            int length)
        {
            return new MatchResult(
                token,
                ignored,
                true,
                offset,
                length);
        }

        internal static MatchResult Miss(
            TToken token)
        {
            return new MatchResult(
                token,
                false,
                false,
                0,
                0);
        }
    }

    public MatchResult Match(Segment segment)
    {
        var result = lexer.Invoke(segment);
        return result.Success
            ? MatchResult.Hit(token, ignored, result.Offset, result.Length)
            : MatchResult.Miss(token);
    }
}
