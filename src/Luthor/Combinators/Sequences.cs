using Luthor.Lexers;

namespace Luthor.Combinators;

public static class Sequences
{
    // |
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

    // i guess 'and' is implicit in regex
    public static Lexer And(this Lexer first, Lexer second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        return segment =>
        {
            var lexeme = first(segment);
            if (lexeme.Success)
            {
                lexeme = second(lexeme.Remainder);
                if (lexeme.Success)
                {
                    return Lexeme.Hit(segment, lexeme.Remainder);
                }
            }

            return Lexeme.Miss(segment);
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
