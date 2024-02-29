using Luthor.Combinators;

namespace Luthor.Classifiers;

public static class Text
{
    public static Parser<ParseResult> CIdentifier =
        Character.IsLetter
        .Or(Character.Is('_'))
        .Then(Character.IsWordCharacter.ZeroOrMore());

    public static Parser<ParseResult> IsWord =
        Character.IsWordCharacter
        .OneOrMore()
        .Then(Character.IsWordBoundary);

    public static Parser<ParseResult> Is(string pattern)
    {
        ArgumentNullException.ThrowIfNull(pattern);

        var literalLength = pattern.Length;
        return input =>
        {
            var start = input;
            var length = input.Length - input.Offset;
            var offset = start.Offset;
            var index = 0;
            var isMatch = offset < length && pattern[index] == input.PeekAhead(offset);
            if (isMatch)
            {
                while (isMatch)
                {
                    ++offset;
                    ++index;
                    isMatch = offset < length
                        && index < literalLength
                        && pattern[index] == input.PeekAhead(offset);
                }

                return pattern.Length == offset - start.Offset
                    ? ParseResult.Hit(start, offset - start.Offset)
                    : ParseResult.Miss(start);
            }

            return ParseResult.Miss(start);
        };
    }

    public static Parser<ParseResult> Is(string pattern, bool ignoreCase)
    {
        if (!ignoreCase)
        {
            return Is(pattern);
        }

        ArgumentNullException.ThrowIfNull(pattern);

        var patternLength = pattern.Length;
        pattern = pattern.ToUpperInvariant();
        return input =>
        {
            var start = input;
            var length = input.Length - input.Offset;
            var offset = start.Offset;
            var index = 0;
            var isMatch = offset < length
                && pattern[index] == Char.ToUpperInvariant(input.PeekAhead(offset));

            if (isMatch)
            {
                while (isMatch)
                {
                    ++offset;
                    ++index;
                    isMatch = offset < length
                        && index < patternLength
                        && pattern[index] == Char.ToUpperInvariant(input.PeekAhead(offset));
                }

                return pattern.Length == offset - start.Offset
                    ? ParseResult.Hit(start, offset - start.Offset)
                    : ParseResult.Miss(start);
            }

            return ParseResult.Miss(start);
        };
    }

    public static Parser<ParseResult> StringLiteral(char delimiter) =>
        Character.Is(delimiter)
        .Then(Character.NotIn('\\', delimiter)
            .Or(Character.Is('\\')
            .Then(Character.Any)).ZeroOrMore())
        .Then(Character.Is(delimiter));
}
