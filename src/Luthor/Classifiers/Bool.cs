using Luthor.Combinators;
using Luthor.Lexers;

namespace Luthor.Classifiers;

public static class Bool
{
    public static Lexer AnyBool(bool ignoreCase) => True(ignoreCase).Or(False(ignoreCase));

    public static Lexer True(bool ignoreCase) => Text.Is("true", ignoreCase);

    public static Lexer False(bool ignoreCase) => Text.Is("false", ignoreCase);
}
