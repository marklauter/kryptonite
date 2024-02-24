namespace Luthor;

public static class Combinators
{
    public static Lexer Or(this Lexer left, Lexer right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        return segment =>
        {
            var lexeme = left(segment);
            return lexeme.Success
                ? lexeme
                : right(segment);
        };
    }

    public static Lexer Then(this Lexer first, Lexer second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        return segment =>
        {
            var lexeme = first(segment);
            return lexeme.Success
                ? second(lexeme.Remainder)
                : lexeme;
        };
    }
}
