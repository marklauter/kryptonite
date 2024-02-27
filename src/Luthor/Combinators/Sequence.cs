using Luthor.Lexers;

namespace Luthor.Combinators;

public static class Sequence
{
    // ^
    // does not consume input
    public static Lexer Not(this Lexer lexer)
    {
        ArgumentNullException.ThrowIfNull(lexer);

        return input =>
        {
            var lexeme = lexer(input);
            return lexeme.Matched
                ? Lexeme.Miss(input)
                : Lexeme.Hit(input, input);
        };
    }

    // |
    public static Lexer Or(this Lexer left, Lexer right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        return segment =>
        {
            var lexeme = left(segment);
            return lexeme.Matched
                ? lexeme
                : right(segment);
        };
    }

    // i guess 'and' is implicit in regex
    public static Lexer And(this Lexer left, Lexer right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        return input =>
        {
            var lexeme = left(input);
            lexeme = lexeme.Matched
                ? right(input)
                : lexeme;

            return lexeme.Matched
                ? Lexeme.Hit(input, lexeme.Remainder)
                : Lexeme.Miss(input);
        };
    }

    public static Lexer Then(this Lexer first, Lexer second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        return input =>
        {
            var lexeme = first(input);
            lexeme = lexeme.Matched
                ? second(lexeme.Remainder)
                : lexeme;

            return lexeme.Matched
                ? Lexeme.Hit(input, lexeme.Remainder)
                : Lexeme.Miss(input);
        };
    }
}
