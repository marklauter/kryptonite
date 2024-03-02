//using System.Text.RegularExpressions;

//namespace Luthor.Classifiers;

//public static class RegexPatterns
//{
//    // todo: patterns require wrap in \G(?:{pattern}) and require special regex.options
//    public static Parser<ParseResult> MatchRegex(Regex pattern)
//    {
//        ArgumentNullException.ThrowIfNull(pattern);

//        return input =>
//        {
//            var match = pattern.Match(input);
//            return match.Success
//                ? ParseResult.Hit(input, match.Length)
//                : ParseResult.Miss(input);
//        };
//    }
//}
