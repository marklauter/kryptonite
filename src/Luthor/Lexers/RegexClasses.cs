using System.Text.RegularExpressions;

namespace Luthor.Lexers;

public static class RegexClasses
{
    // todo: patterns require wrap in \G(?:{pattern})
    public static Lexer MatchRegex(Regex pattern)
    {
        return segment =>
        {
            var match = pattern.Match(segment);
            return match.Success
                ? Lexeme.Hit(segment, segment.Advance(match.Length), match.Length)
                : Lexeme.Miss(segment);
        };
    }
}
