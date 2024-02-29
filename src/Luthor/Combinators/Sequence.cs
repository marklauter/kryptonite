namespace Luthor.Combinators;

public static class Sequence
{
    // ^
    // does not consume input
    public static Parser<ParseResult> Not(this Parser<ParseResult> lexer)
    {
        ArgumentNullException.ThrowIfNull(lexer);

        return input =>
        {
            var lexeme = lexer(input);
            return lexeme.HasValue
                ? ParseResult.Miss(input)
                : ParseResult.Hit(input, 0);
        };
    }

    // |
    public static Parser<ParseResult> Or(this Parser<ParseResult> left, Parser<ParseResult> right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        return segment =>
        {
            var lexeme = left(segment);
            return lexeme.HasValue
                ? lexeme
                : right(segment);
        };
    }

    // i guess 'and' is implicit in regex
    public static Parser<ParseResult> And(this Parser<ParseResult> left, Parser<ParseResult> right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        return input =>
        {
            var lexeme = left(input);
            lexeme = lexeme.HasValue
                ? right(input)
                : lexeme;

            return lexeme.HasValue
                ? ParseResult.Hit(input, lexeme.Remainder)
                : ParseResult.Miss(input);
        };
    }

    public static Parser<ParseResult> Then(this Parser<ParseResult> first, Parser<ParseResult> second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        return input =>
        {
            var lexeme = first(input);
            lexeme = lexeme.HasValue
                ? second(lexeme.Remainder)
                : lexeme;

            return lexeme.HasValue
                ? ParseResult.Hit(input, lexeme.Remainder)
                : ParseResult.Miss(input);
        };
    }

    //public static Parser<TResult> Then<TLeft, TRight, TResult>(this Parser<TLeft> first, Parser<TRight> second)
    //{
    //    ArgumentNullException.ThrowIfNull(first);
    //    ArgumentNullException.ThrowIfNull(second);

    //    return input =>
    //    {
    //        var lexeme = first(input);
    //        lexeme = lexeme.Matched
    //            ? second(lexeme.Remainder)
    //            : lexeme;

    //        return lexeme.Matched
    //            ? ParseResult.Hit(input, lexeme.Remainder)
    //            : ParseResult.Miss(input);
    //    };
    //}
}
