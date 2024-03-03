using System.Runtime.CompilerServices;

namespace Luthor;

public static class Parser
{
    // result :: a-> Parser a
    // result v = \inp-> [(v,inp)]
    // does not consume
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<V> ResultV<V>(V v) => input =>
        Luthor.Result.WithValue(v, input);

    // zero :: Parser a
    // zero = \inp-> []
    // does not consume
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<T> Zero<T>() =>
        Luthor.Result.Empty<T>;

    // item :: Parser Char
    // item = \inp-> case inp of
    //  []      -> []var
    //  (x:xs)  -> [(x, xs)]
    // always consumes one char unless end of input
    public static Parser<char> Item = input =>
    {
        var eof = input.EndOfInput;
        return eof
            ? Luthor.Result.Empty<char>(input)
            : Luthor.Result.New(input.Peek(), input, input.Advance(), !eof);
    };

    // bind :: Parser a-> (a-> Parser b)-> Parser b
    // p bind f = \inp-> concat[f v inp | (v, inp) < -p inp]
    public static Parser<TRight> Bind<TLeft, TRight>(
        this Parser<TLeft> parser,
        Func<TLeft, Parser<TRight>> function)
    {
        ArgumentNullException.ThrowIfNull(parser);
        ArgumentNullException.ThrowIfNull(function);

        return input =>
        {
            var leftResult = parser(input);
            if (!leftResult.HasValue)
            {
                return leftResult.CastEmpty<TLeft, TRight>();
            }

            var rightResult =
                function(leftResult.Value)(leftResult.Remainder);

            return !rightResult.HasValue
                ? rightResult
                : Result.WithValue(rightResult.Value, input, rightResult.Remainder);
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
                        Result.WithValue<(TLeft, TRight)>((leftResult, rightResult), input))));
    }

    // sat :: (Char-> Bool) -> Parser Char
    // sat p = item bind \x ->
    //         if p x then result x else zero
    public static Parser<char> Satisfy(Func<char, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return input =>
        {
            var result = Item(input);
            return !result.HasValue
                ? result
                : predicate(result.Value)
                    ? Result.WithValue(result.Value, input, result.Remainder)
                    : Result.Empty<char>(input);
        };
    }
}
