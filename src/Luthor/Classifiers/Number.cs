using Luthor.Combinators;
using Luthor.Lexers;

namespace Luthor.Classifiers;

// todo: bench MatchInteger vs MatchIntegerFast to guarantee that MatchInteger2 is faster and has fewer allocations
public static class Number
{
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

    public static Lexer IsInteger =
        Character.Is('+')
        .Or(Character.Is('-'))
        .ZeroOrOne()
        .Then(Character.IsDigit.OneOrMore());

    public static Lexer IsFloatingPoint =
        IsInteger
        .Then(Character.Is('.'))
        .Then(Character.IsDigit.OneOrMore());

    public static Lexer IsScientificNotation =
        IsFloatingPoint
        .Then(Character.Is('e', true))
        .Then(IsInteger);

    public static Lexer IsHexNotation =
        Character.Is('0')
        .Then(Character.Is('x', true))
        .Then(Character.IsHexDigit.OneOrMore());
}
