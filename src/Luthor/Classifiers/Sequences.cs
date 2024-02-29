namespace Luthor.Classifiers;

public static class Sequences
{
    // bind :: Parser a-> (a-> Parser b)-> Parser b
    // p bind f = \inp-> concat[f v inp | (v, inp) < -p inp]
    public static Parser<TSecond> Bind<TFirst, TSecond>(
        this Parser<TFirst> parser,
        Func<TFirst, Parser<TSecond>> function)
    {
        ArgumentNullException.ThrowIfNull(parser);
        ArgumentNullException.ThrowIfNull(function);

        return input =>
        {
            var firstResult = parser(input);
            if (!firstResult.HasValue)
            {
                return firstResult.CastEmpty<TFirst, TSecond>();
            }

            var secondResult =
                function(firstResult.Value)(firstResult.Remainder);

            return !secondResult.HasValue
                ? secondResult
                : Result.WithValue(secondResult.Value, input, secondResult.Remainder);
        };
    }

    // sat :: (Char-> Bool) -> Parser Char
    // sat p = item bind \x ->
    //         if p x then result x else zero
    public static Parser<char> Satisfy(Func<char, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return input =>
        {
            var result = Primitives.Item(input);
            return !result.HasValue
                ? result
                : predicate(result.Value)
                    ? Result.WithValue(result.Value, input, result.Remainder)
                    : Result.Empty<char>(input);
        };
    }
}
