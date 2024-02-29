using Luthor.Combinators;

namespace Luthor.Classifiers;

// todo: bench MatchInteger vs MatchIntegerFast to guarantee that MatchInteger2 is faster and has fewer allocations
public static class Number
{
    public static Parser<ParseResult> IsIntegerFast => input =>
    {
        var start = input;
        var length = input.Length - input.Offset;
        var offset = start.Offset;
        if (input.Peek() is '-' or '+')
        {
            ++offset;
        }

        var isDigit = offset < length && Char.IsDigit(input.PeekAhead(offset));
        if (isDigit)
        {
            while (isDigit)
            {
                ++offset;
                isDigit = offset < length && Char.IsDigit(input.PeekAhead(offset));
            }

            return ParseResult.Hit(start, input.Advance(offset - start.Offset));
        }

        return ParseResult.Miss(start);
    };

    public static Parser<ParseResult> IsInteger =
        Character.Is('+')
        .Or(Character.Is('-'))
        .ZeroOrOne()
        .Then(Character.IsDigit.OneOrMore());

    public static Parser<ParseResult> IsFloatingPoint =
        IsInteger
        .Then(Character.Is('.'))
        .Then(Character.IsDigit.OneOrMore());

    public static Parser<ParseResult> IsScientificNotation =
        IsFloatingPoint
        .Then(Character.Is('e', true))
        .Then(IsInteger);

    public static Parser<ParseResult> IsHexNotation =
        Character.Is('0')
        .Then(Character.Is('x', true))
        .Then(Character.IsHexDigit.OneOrMore());
}
