//using Luthor.Combinators;

//namespace Luthor.Classifiers;

//public static class Bool
//{
//    public static Parser<ParseResult> AnyBool(bool ignoreCase) => True(ignoreCase).Or(False(ignoreCase));

//    public static Parser<ParseResult> True(bool ignoreCase) => Text.Is("true", ignoreCase);

//    public static Parser<ParseResult> False(bool ignoreCase) => Text.Is("false", ignoreCase);

//    public static Parser<ParseResult> True(string value, bool ignoreCase) => Text.Is(value, ignoreCase);

//    public static Parser<ParseResult> False(string value, bool ignoreCase) => Text.Is(value, ignoreCase);
//}
