using Luthor.Lexers;

namespace Luthor.Combinators;

public static class Quantifiers
{
    // *
    public static Lexer ZeroOrMore(this Lexer lexer)
    {
        ArgumentNullException.ThrowIfNull(lexer);

        return input =>
        {
            var start = input;
            var lexeme = lexer(input);
            while (lexeme.Matched)
            {
                lexeme = lexer(lexeme.Remainder);
            }

            return Lexeme.Hit(start, lexeme.Remainder);
        };
    }

    // ?
    public static Lexer ZeroOrOne(this Lexer lexer)
    {
        ArgumentNullException.ThrowIfNull(lexer);

        return input =>
        {
            var lexeme = lexer(input);
            return lexeme.Matched
                ? lexeme
                : Lexeme.Hit(input, input); // a hit without consuming input
        };
    }

    // +
    public static Lexer OneOrMore(this Lexer lexer)
    {
        ArgumentNullException.ThrowIfNull(lexer);

        return input =>
        {
            var start = input;
            var lexeme = lexer(input);
            if (lexeme.Matched) // meet the match one condition correctly
            {
                while (lexeme.Matched) // match more so we get the longest match and pass lexeme.Remainder to Hit
                {
                    lexeme = lexer(lexeme.Remainder);
                }

                return Lexeme.Hit(start, lexeme.Remainder);
            }

            return Lexeme.Miss(start);
        };
    }

    // {n}
    public static Lexer Exactly(int n) => throw new NotImplementedException(nameof(Exactly));

    // {n,}
    public static Lexer AtLeast(int n) =>
        // todo: look at OneOrMore for guidance
        throw new NotImplementedException(nameof(AtLeast));

    // {n, m}
    public static Lexer AtLeastButNoMore(int n, int m) => throw new NotImplementedException(nameof(AtLeastButNoMore));
}
