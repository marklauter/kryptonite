using System.Runtime.CompilerServices;

namespace Luthor.Classifiers;

public static class Primitives
{
    // result :: a-> Parser a
    // result v = \inp-> [(v,inp)]
    // does not consume
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<V> Result<V>(V v) => input =>
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
}
