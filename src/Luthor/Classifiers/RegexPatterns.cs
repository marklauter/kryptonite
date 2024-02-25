using Luthor.Lexers;
using System.Text.RegularExpressions;

namespace Luthor.Classifiers;

public static class RegexPatterns
{
    // todo: patterns require wrap in \G(?:{pattern})
    public static Lexer MatchRegex(Regex pattern) => segment =>
    {
        var match = pattern.Match(segment);
        return match.Success
            ? Lexeme.Hit(segment, segment.Advance(match.Length), match.Length)
            : Lexeme.Miss(segment);
    };
}
