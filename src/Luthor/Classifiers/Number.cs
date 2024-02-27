using Luthor.Combinators;
using Luthor.Lexers;

namespace Luthor.Classifiers;

// todo: bench MatchInteger vs MatchIntegerFast to guarantee that MatchInteger2 is faster and has fewer allocations
public static class Number
{
    public static Lexer IsInteger => input =>
    {
        var start = input;
        if (input.Look() is '-' or '+')
        {
            input = input.Advance();
        }

        var lexeme = Character.IsDigit(input);
        if (lexeme.Matched)
        {
            while (lexeme.Matched)
            {
                input = input.Advance();
                lexeme = Character.IsDigit(input);
            }

            return Lexeme.Hit(start, input);
        }

        return Lexeme.Miss(start);
    };

    public static Lexer IsIntegerFast => input =>
    {
        var start = input;
        var length = input.Length - input.Offset;
        var offset = start.Offset;
        if (input.Look() is '-' or '+')
        {
            ++offset;
        }

        var isDigit = offset < length && Char.IsDigit(input.LookAhead(offset));
        if (isDigit)
        {
            while (isDigit)
            {
                ++offset;
                isDigit = offset < length && Char.IsDigit(input.LookAhead(offset));
            }

            return Lexeme.Hit(start, input.Advance(offset - start.Offset));
        }

        return Lexeme.Miss(start);
    };

    public static Lexer IsInteger3 =
        Character.Is('+')
        .Or(Character.Is('-'))
        .ZeroOrOne()
        .Then(Character.IsDigit.OneOrMore());

    public static Lexer IsFloatingPoint => input =>
    {
        var start = input;
        var lexeme = IsInteger(input);
        if (lexeme.Matched)
        {
            input = lexeme.Remainder;
            if (input.Look() == '.')
            {
                input = input.Advance();
                lexeme = Character.IsDigit(input);
                if (lexeme.Matched)
                {
                    while (lexeme.Matched)
                    {
                        input = input.Advance();
                        lexeme = Character.IsDigit(input);
                    }

                    return Lexeme.Hit(start, input);
                }
            }
        }

        return Lexeme.Miss(start);
    };

    public static Lexer IsFloatingPoint2 =
        IsInteger3
        .Then(Character.Is('.'))
        .Then(Character.IsDigit.OneOrMore());

    public static Lexer IsScientificNotation => input =>
    {
        var start = input;
        var lexeme = IsFloatingPoint(input);
        if (lexeme.Matched)
        {
            input = lexeme.Remainder;
            if (input.Look() is 'e' or 'E')
            {
                input = input.Advance();
                lexeme = IsInteger(input);
                if (lexeme.Matched)
                {
                    return Lexeme.Hit(start, lexeme.Remainder);
                }
            }
        }

        return Lexeme.Miss(start);
    };

    public static Lexer IsScientificNotation2 =
        IsFloatingPoint2
        .Then(Character.Is('e', true))
        .Then(IsInteger3);

    public static Lexer IsHexNotation => input =>
    {
        var start = input;
        if (input.Look() == '0')
        {
            input = input.Advance();
            if (input.Look() is 'x' or 'X')
            {
                input = input.Advance();
                var lexeme = Character.IsHexDigit(input);
                if (lexeme.Matched)
                {
                    while (lexeme.Matched)
                    {
                        input = input.Advance();
                        lexeme = Character.IsHexDigit(input);
                    }
                    // todo: seems like tests like this need to terminated on a word boundary, like \G\d\b, otherwise you could lex 0xFFZZZ as 0xFF and miss the syntax error
                    return Lexeme.Hit(start, input);
                }
            }
        }

        return Lexeme.Miss(start);
    };

    public static Lexer IsHexNotation2 =
        Character.Is('0')
        .Then(Character.Is('x', true))
        .Then(Character.IsHexDigit.OneOrMore());
}
