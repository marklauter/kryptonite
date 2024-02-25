using System.Runtime.CompilerServices;

namespace Luthor.Lexers;

public sealed class Lexer<TToken>(
    Lexer lexer,
    TToken token,
    bool ignored)
    where TToken : Enum
{
    private readonly Lexer lexer = lexer ?? throw new ArgumentNullException(nameof(lexer));
    private readonly TToken token = token;
    private readonly bool ignored = ignored;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Lexer(Lexer lexer, TToken token)
        : this(lexer, token, false)
    { }

    public sealed record MatchResult(
        Lexeme Lexeme,
        TToken Token,
        bool Ignored)
    {
        public bool Success => Lexeme.Success;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MatchResult Match(Segment segment)
    {
        return new MatchResult(
            lexer.Invoke(segment),
            token,
            ignored);
    }
}
