namespace Luthor.Classifiers;

public static class Sequences
{
    // bind :: Parser a-> (a-> Parser b)-> Parser b
    // p bind f = \inp-> concat[f v inp | (v, inp) < -p inp]
    public static Parser<TSecond> Bind<TFirst, TSecond>(
        this Parser<TFirst> first,
        Func<TFirst?, Parser<TSecond>> second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        return input =>
        {
            var firstResult = first(input);
            if (!firstResult.HasValue)
            {
                return firstResult.CastEmpty<TFirst, TSecond>();
            }

            var secondResult = second(firstResult.Value)(firstResult.Remainder);
            if (!secondResult.HasValue)
            {
                return secondResult;
            }

            var length = secondResult.Remainder.Offset - input.Offset;
            return Result.WithValue(secondResult.Value, input, length);
        };
    }
}
