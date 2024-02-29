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

            var secondResult = function(firstResult.Value)(firstResult.Remainder);
            if (!secondResult.HasValue)
            {
                return secondResult;
            }

            var length = secondResult.Remainder.Offset - input.Offset;
            return Result.WithValue(secondResult.Value, input, length);
        };
    }
}
