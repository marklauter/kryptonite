namespace Luthor.Lexers;

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
