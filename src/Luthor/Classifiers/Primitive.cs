namespace Luthor.Classifiers;

public readonly struct Empty
{
    public static Empty Default;
}

public static class Primitives
{
    // result :: a-> Parser a
    // result v = \inp-> [(v,inp)]
    public static Parser<V> Result<V>(V v) => input =>
        Luthor.Result.WithValue(v, input);

    // zero :: Parser a
    // zero = \inp-> []
    public static Parser<Empty> Zero => Luthor.Result.Empty<Empty>;

    // item :: Parser Char
    // item = \inp-> case inp of
    //  []      -> []var
    //  (x:xs)  -> [(x, xs)]
    public static Parser<char> Item => input =>
        Luthor.Result.New(
            input.Peek(),
            input,
            input.EndOfInput ? input : input.Advance(),
            input.EndOfInput);
}
