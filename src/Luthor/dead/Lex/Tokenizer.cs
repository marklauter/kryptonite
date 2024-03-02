//namespace Luthor.Lexers;

//public readonly ref struct Tokenizer<TToken>(
//    ReadOnlySpan<Lexer<TToken>> lexers)
//    where TToken : Enum
//{
//    private readonly ReadOnlySpan<Lexer<TToken>> lexers = lexers;

//    private static int CompareResponse(
//        Lexeme nextMatch,
//        Lexeme bestMatch,
//        int nextIndex,
//        int bestIndex)
//    {
//        var nextLenth = nextMatch.Length;
//        var bestLength = bestMatch.Length;

//        // longer match wins. tie goes to lowest index
//        // equal means no swap, ambiguous match, and probably an error (aka skill issue) in the vocabulary
//        // todo: probably should throw in that case, or create an error token
//        return
//            nextLenth > bestLength
//            ? 1
//            : nextLenth < bestLength
//                ? -1
//                : nextIndex < bestIndex
//                    ? 1
//                    : nextIndex > bestIndex
//                        ? -1
//                        : 0;
//    }

//    //public IEnumerable<Symbol<TToken>> Tokens(SourceSegment segment)
//    //{
//    //    var token = NextToken(segment);
//    //    while (!token.NextSegment.EndOfSource)
//    //    {
//    //        if (token.Ignored)
//    //        {
//    //            continue;
//    //        }

//    //        if (token.Success)
//    //        {
//    //            yield return token.Symbol;
//    //        }

//    //        token = NextToken(token.NextSegment);
//    //    }
//    //}

//    //private readonly ref struct TokenResult(
//    //    SourceSegment nextSegment,
//    //    Symbol<TToken> symbol,
//    //    bool ignored,
//    //    bool success)
//    //{
//    //    public readonly SourceSegment NextSegment = nextSegment;
//    //    public readonly Symbol<TToken> Symbol = symbol;
//    //    public readonly bool Ignored = ignored;
//    //    public readonly bool Success = success;
//    //}

//    //private TokenResult NextToken(SourceSegment segment)
//    //{
//    //    var bestMatch = ParseResult<TToken>.NoMatch(default!);
//    //    var bestIndex = -1;
//    //    var ignored = false;
//    //    for (var i = 0; i < lexers.Length; ++i)
//    //    {
//    //        var recognizer = lexers[i];
//    //        ParseResult nextMatch = recognizer.Match(segment);
//    //        if (nextMatch.Success && CompareResponse(
//    //            nextMatch,
//    //            bestMatch,
//    //            i,
//    //            bestIndex) > 0)
//    //        {
//    //            bestMatch = nextMatch;
//    //            ignored = recognizer.Ignored;
//    //        }
//    //    }

//    //    return bestMatch.Success
//    //        ? new TokenResult(
//    //            segment.Advance(bestMatch.Length),
//    //            new(segment, bestMatch.Token, bestMatch.Length),
//    //            ignored,
//    //            true)
//    //        : new TokenResult(
//    //            segment,
//    //            Symbol<TToken>.Empty,
//    //            false,
//    //            false);
//    //}
//}
