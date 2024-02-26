using Luthor.Combinators;
using Luthor.Lexers;

namespace Luthor.Classifiers;

// todo: bench MatchInteger vs MatchIntegerFast to guarantee that MatchInteger2 is faster and has fewer allocations
public static class Numerics
{
    public static Lexer IsInteger => segment =>
    {
        var start = segment;
        if (segment.Look() is '-' or '+')
        {
            segment = segment.Advance();
        }

        var lexeme = Characters.IsDigit(segment);
        if (lexeme.Success)
        {
            while (lexeme.Success)
            {
                segment = segment.Advance();
                lexeme = Characters.IsDigit(segment);
            }

            return Lexeme.Hit(start, segment);
        }

        return Lexeme.Miss(start);
    };

    public static Lexer IsIntegerFast => segment =>
    {
        var start = segment;
        var length = segment.Length - segment.Offset;
        var offset = start.Offset;
        if (segment.Look() is '-' or '+')
        {
            ++offset;
        }

        var isDigit = offset < length && Char.IsDigit(segment.LookAhead(offset));
        if (isDigit)
        {
            while (isDigit)
            {
                ++offset;
                isDigit = offset < length && Char.IsDigit(segment.LookAhead(offset));
            }

            return Lexeme.Hit(start, segment.Advance(offset - start.Offset));
        }

        return Lexeme.Miss(start);
    };

    public static Lexer IsInteger3 =
        Characters.Is('+')
        .Or(Characters.Is('-'))
        .ZeroOrOne()
        .And(Characters.IsDigit.OneOrMore());

    public static Lexer IsFloatingPoint => segment =>
    {
        var start = segment;
        var lexeme = IsInteger(segment);
        if (lexeme.Success)
        {
            segment = lexeme.Remainder;
            if (segment.Look() == '.')
            {
                segment = segment.Advance();
                lexeme = Characters.IsDigit(segment);
                if (lexeme.Success)
                {
                    while (lexeme.Success)
                    {
                        segment = segment.Advance();
                        lexeme = Characters.IsDigit(segment);
                    }

                    return Lexeme.Hit(start, segment);
                }
            }
        }

        return Lexeme.Miss(start);
    };

    public static Lexer IsFloatingPoint2 =
        IsInteger3
        .And(Characters.Is('.'))
        .And(Characters.IsDigit.OneOrMore());

    public static Lexer IsScientificNotation => segment =>
    {
        var start = segment;
        var lexeme = IsFloatingPoint(segment);
        if (lexeme.Success)
        {
            segment = lexeme.Remainder;
            if (segment.Look() is 'e' or 'E')
            {
                segment = segment.Advance();
                lexeme = IsInteger(segment);
                if (lexeme.Success)
                {
                    return Lexeme.Hit(start, lexeme.Remainder);
                }
            }
        }

        return Lexeme.Miss(start);
    };

    public static Lexer IsScientificNotation2 =
        IsFloatingPoint2
        .And(Characters.Is('e', true))
        .And(IsInteger3);

    public static Lexer IsHexNotation => segment =>
    {
        var start = segment;
        if (segment.Look() == '0')
        {
            segment = segment.Advance();
            if (segment.Look() is 'x' or 'X')
            {
                segment = segment.Advance();
                var lexeme = Characters.IsHexDigit(segment);
                if (lexeme.Success)
                {
                    while (lexeme.Success)
                    {
                        segment = segment.Advance();
                        lexeme = Characters.IsHexDigit(segment);
                    }
                    // todo: seems like tests like this need to terminated on a word boundary, like \G\d\b, otherwise you could lex 0xFFZZZ as 0xFF and miss the syntax error
                    return Lexeme.Hit(start, segment);
                }
            }
        }

        return Lexeme.Miss(start);
    };

    public static Lexer IsHexNotation2 =
        Characters.Is('0')
        .And(Characters.Is('x', true))
        .And(Characters.IsHexDigit.OneOrMore());
}
