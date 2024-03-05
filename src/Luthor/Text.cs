namespace Luthor;

public static class Text
{
    // word :: Parser String
    // word = neWord plus result ""
    //        where
    //            neWord = letter bind \x->
    //                     word bind \xs->
    //                     result (x:xs)
    // todo: this is the least efficient way in C# to create strings, but it proves that bind and plus work as descibed in monparsing.pdf
    private static Parser<string> RecursedWord() =>
        Character
            .Letter()
            .Bind(ch => RecursedWord()
            .Bind(s => Parser.Result(ch + s)))
            .Plus(Parser.Result(String.Empty));

    public static Parser<string> Word() =>
        RecursedWord().Bind(s => String.IsNullOrEmpty(s)
            ? Parser.Zero<string>()
            : Parser.Result(s));
}

