using Luthor.Combinators;
using Luthor.Lexers;

namespace Luthor.Classifiers;

public static class Strings
{
    public static Lexer CSharpIdentifier =
        Characters.IsLetter
        .Or(Characters.Is('_'))
        .OneOrMore()
        .And(Characters.IsWordCharacter.ZeroOrMore());

    public static Lexer IsWord =
        Characters.IsWordCharacter
        .OneOrMore()
        .Then(Characters.IsWordBoundary);

    public static Lexer Is(string pattern)
    {
        var literalLength = pattern.Length;
        return segment =>
        {
            var start = segment;
            var length = segment.Length - segment.Offset;
            var offset = start.Offset;
            var index = 0;
            var isMatch = offset < length && pattern[index] == segment.LookAhead(offset);
            if (isMatch)
            {
                while (isMatch)
                {
                    ++offset;
                    ++index;
                    isMatch = offset < length
                        && index < literalLength
                        && pattern[index] == segment.LookAhead(offset);
                }

                return pattern.Length == offset - start.Offset
                    ? Lexeme.Hit(start, segment.Advance(offset - start.Offset))
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

        var patternLength = pattern.Length;
        pattern = pattern.ToUpperInvariant();
        return segment =>
        {
            var start = segment;
            var length = segment.Length - segment.Offset;
            var offset = start.Offset;
            var index = 0;
            var isMatch = offset < length
                && pattern[index] == Char.ToUpperInvariant(segment.LookAhead(offset));

            if (isMatch)
            {
                while (isMatch)
                {
                    ++offset;
                    ++index;
                    isMatch = offset < length
                        && index < patternLength
                        && pattern[index] == Char.ToUpperInvariant(segment.LookAhead(offset));
                }

                return pattern.Length == offset - start.Offset
                    ? Lexeme.Hit(start, segment.Advance(offset - start.Offset))
                    : Lexeme.Miss(start);
            }

            return Lexeme.Miss(start);
        };
    }

    public static Lexer IsStringLiteral(char delimiter) => segment =>
    {
        var start = segment;
        if (segment.Look() == delimiter)
        {
            segment = segment.Advance();
            var isEscaped = false;
            while (!segment.EndOfSegment)
            {
                if (segment.Look() == '\\' && !isEscaped) // Check for escape character
                {
                    isEscaped = true;
                }
                else if (segment.Look() == delimiter && !isEscaped)
                {
                    segment = segment.Advance();
                    return Lexeme.Hit(start, segment);
                }
                else
                {
                    isEscaped = false;
                }

                segment = segment.Advance();
            }
        }

        return Lexeme.Miss(start);
    };

}
