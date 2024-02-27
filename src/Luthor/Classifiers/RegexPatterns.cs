using Luthor.Lexers;
using System.Text.RegularExpressions;

namespace Luthor.Classifiers;

public static class RegexPatterns
{
    // todo: patterns require wrap in \G(?:{pattern}) and require special regex.options
    public static Lexer MatchRegex(Regex pattern)
    {
        ArgumentNullException.ThrowIfNull(pattern);

        return input =>
        {
            var match = pattern.Match(input);
            return match.Success
                ? Lexeme.Hit(input, input.Advance(match.Length), match.Length)
                : Lexeme.Miss(input);
        };
    }
}
