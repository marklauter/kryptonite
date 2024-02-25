namespace Luthor.Lexers;

public sealed class Vocabulary<TToken>
    where TToken : Enum
{
    private readonly List<Lexer<TToken>> lexers = [];

    public Tokenizer<TToken> Build()
    {
        return new Tokenizer<TToken>(lexers.ToArray());
    }


    //public Vocabulary Match(
    //    string pattern,
    //    int id)
    //{
    //    matchers.Add(new(
    //        pattern,
    //        id));

    //    return this;
    //}


    //public Vocabulary Match(
    //    string pattern,
    //    int id,
    //    RegexOptions options)
    //{
    //    matchers.Add(new(
    //        pattern,
    //        id,
    //        options));

    //    return this;
    //}
}
