//namespace Luthor.Combinators;

//public static class Quantifiers
//{
//    // *
//    public static Parser<ParseResult> ZeroOrMore(this Parser<ParseResult> lexer)
//    {
//        ArgumentNullException.ThrowIfNull(lexer);

//        return input =>
//        {
//            var start = input;
//            var lexeme = lexer(input);
//            while (lexeme.HasValue)
//            {
//                lexeme = lexer(lexeme.Remainder);
//            }

//            return ParseResult.Hit(start, lexeme.Remainder);
//        };
//    }

//    // ?
//    public static Parser<ParseResult> ZeroOrOne(this Parser<ParseResult> lexer)
//    {
//        ArgumentNullException.ThrowIfNull(lexer);

//        return input =>
//        {
//            var lexeme = lexer(input);
//            return lexeme.HasValue
//                ? lexeme
//                : ParseResult.Hit(input, input); // a hit without consuming input
//        };
//    }

//    // +
//    public static Parser<ParseResult> OneOrMore(this Parser<ParseResult> lexer)
//    {
//        ArgumentNullException.ThrowIfNull(lexer);

//        return input =>
//        {
//            var start = input;
//            var lexeme = lexer(input);
//            if (lexeme.HasValue) // meet the match one condition correctly
//            {
//                while (lexeme.HasValue) // match more so we get the longest match and pass lexeme.Remainder to Hit
//                {
//                    lexeme = lexer(lexeme.Remainder);
//                }

//                return ParseResult.Hit(start, lexeme.Remainder);
//            }

//            return ParseResult.Miss(start);
//        };
//    }

//    // {n}
//    public static Parser<ParseResult> Exactly(int n) => throw new NotImplementedException(nameof(Exactly));

//    // {n,}
//    public static Parser<ParseResult> AtLeast(int n) =>
//        // todo: look at OneOrMore for guidance
//        throw new NotImplementedException(nameof(AtLeast));

//    // {n, m}
//    public static Parser<ParseResult> AtLeastButNoMore(int n, int m) => throw new NotImplementedException(nameof(AtLeastButNoMore));
//}
