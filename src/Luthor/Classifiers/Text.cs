using Luthor.Combinators;
using Luthor.Lexers;

namespace Luthor.Classifiers;

public static class Text
{
    public static Lexer CIdentifier =
        Character.IsLetter
        .Or(Character.Is('_'))
        .Then(Character.IsWordCharacter.ZeroOrMore());

    public static Lexer IsWord =
        Character.IsWordCharacter
        .OneOrMore()
        .Then(Character.IsWordBoundary);

    public static Lexer Is(string pattern)
    {
        ArgumentNullException.ThrowIfNull(pattern);

        var literalLength = pattern.Length;
        return input =>
        {
            var start = input;
            var length = input.Length - input.Offset;
            var offset = start.Offset;
            var index = 0;
            var isMatch = offset < length && pattern[index] == input.LookAhead(offset);
            if (isMatch)
            {
                while (isMatch)
                {
                    ++offset;
                    ++index;
                    isMatch = offset < length
                        && index < literalLength
                        && pattern[index] == input.LookAhead(offset);
                }

                return pattern.Length == offset - start.Offset
                    ? Lexeme.Hit(start, input.Advance(offset - start.Offset))
                    : Lexeme.Miss(start);
            }

            return Lexeme.Miss(start);
        };
    }

    public static Lexer Is(string pattern, bool ignoreCase)
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
                && pattern[index] == Char.ToUpperInvariant(input.LookAhead(offset));

            if (isMatch)
            {
                while (isMatch)
                {
                    ++offset;
                    ++index;
                    isMatch = offset < length
                        && index < patternLength
                        && pattern[index] == Char.ToUpperInvariant(input.LookAhead(offset));
                }

                return pattern.Length == offset - start.Offset
                    ? Lexeme.Hit(start, input.Advance(offset - start.Offset))
                    : Lexeme.Miss(start);
            }

            return Lexeme.Miss(start);
        };
    }

    public static Lexer StringLiteral(char delimiter)
    {
        var body =
            Character.NotIn('\\', delimiter)
            .Or(Character.Is('\\')
            .Then(Character.Any));

        var stringLiteral =
            Character.Is(delimiter)
            .Then(body.ZeroOrMore())
            .Then(Character.Is(delimiter));

        return segment => stringLiteral(segment);
    }
}
