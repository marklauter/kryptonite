using Luthor.Combinators;
using Luthor.Lexers;

namespace Luthor.Classifiers;

public static class Booleans
{
    public static Lexer IsBool => IsTrue.Or(IsFalse);
    public static Lexer IsTrue => Strings.Is("true");
    public static Lexer IsFalse => Strings.Is("false");
}
