using System.Runtime.CompilerServices;

namespace Luthor;

public static class Parser
{
    // result :: a-> Parser a
    // result v = \inp-> [(v,inp)]
    // does not consume
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<TValue> Result<TValue>(TValue value) =>
        input => ParseResult.Value(value, input);

    // zero :: Parser a
    // zero = \inp-> []
    // does not consume
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<T> Zero<T>() => ParseResult.Zero<T>;

    // item :: Parser Char
    // item = \inp-> case inp of
    //  []      -> []var
    //  (x:xs)  -> [(x, xs)]
    // always consumes one char unless end of input
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Item() =>
        input => input.EndOfInput
            ? ParseResult.Zero<char>(input)
            : ParseResult.Value(input.Peek(), input, input.Advance());

    // bind :: Parser a-> (a-> Parser b)-> Parser b
    // p bind f = \inp-> concat[f v inp | (v, inp) < -p inp]
    public static Parser<TRight> Bind<TLeft, TRight>(
        this Parser<TLeft> leftParser,
        Func<TLeft, Parser<TRight>> rightParser)
    {
        ArgumentNullException.ThrowIfNull(leftParser);
        ArgumentNullException.ThrowIfNull(rightParser);

        return input =>
        {
            var leftResult = leftParser(input);
            if (!leftResult.HasValue)
            {
                return leftResult.CastEmpty<TLeft, TRight>();
            }

            var rightResult =
                rightParser(leftResult.Value)(leftResult.Remainder);

            return !rightResult.HasValue
                ? rightResult
                : ParseResult.Value(rightResult.Value, input, rightResult.Remainder);
        };
    }

    // p seq q = p bind \x->
    //           q bind \y->
    //           result(x, y)
    public static Parser<(TLeft LeftValue, TRight RightValue)> Sequence<TLeft, TRight>(
        this Parser<TLeft> leftParser,
        Parser<TRight> rightParser)
    {
        ArgumentNullException.ThrowIfNull(leftParser);
        ArgumentNullException.ThrowIfNull(rightParser);

        return
            leftParser.Bind(leftResult =>
                rightParser.Bind(rightResult =>
                    new Parser<(TLeft, TRight)>(input =>
                        ParseResult.Value<(TLeft, TRight)>((leftResult, rightResult), input))));
    }

    // sat :: (Char-> Bool) -> Parser Char
    // sat p = item bind \x ->
    //         if p x then result x else zero
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Satisfy(Func<char, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return Item().Bind(ch =>
            predicate(ch)
                ? Result(ch)
                : Zero<char>());
    }
}
