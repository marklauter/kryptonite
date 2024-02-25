namespace Luthor.Lexers;

public static class CharacterClasses
{
    // [0-9]
    // \d
    public static Lexer AnyDigit => segment => Char.IsDigit(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment, segment.Advance());

    // [0-9a-fA-F]
    public static Lexer AnyHexDigit => segment => Char.IsAsciiHexDigit(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment, segment.Advance());

    // \D
    public static Lexer AnyNonDigit => segment => !Char.IsDigit(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment, segment.Advance());

    // [a-zA-Z]
    public static Lexer AnyLetter => segment => Char.IsLetter(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment, segment.Advance());

    // [^a-zA-Z]
    public static Lexer AnyNonLetter => segment => !Char.IsLetter(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment, segment.Advance());

    // [ |\t|\n|\r|\v|\f]
    // \s
    public static Lexer AnyWhitespace => segment => Char.IsWhiteSpace(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment, segment.Advance());

    // [^ |\t|\n|\r|\v|\f]
    // \S
    public static Lexer AnyNonWhitespace => segment => !Char.IsWhiteSpace(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment, segment.Advance());

    // .
    public static Lexer AnyNonNewLine => segment => segment.Peek() != '\n'
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment, segment.Advance());

    // [a-z]
    public static Lexer AnyLowerCase => segment => Char.IsLower(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment, segment.Advance());

    // [A-Z]
    public static Lexer AnyUpperCase => segment => Char.IsUpper(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment, segment.Advance());

    // [\s\S]
    public static Lexer AnyCharacter => segment => Lexeme.Hit(segment, segment.Advance());

    // \w
    public static Lexer AnyWordCharacter => segment =>
    {
        var value = segment.Peek();
        return Char.IsLetterOrDigit(value) || value == '_'
            ? Lexeme.Hit(segment, segment.Advance())
            : Lexeme.Miss(segment, segment.Advance());
    };

    // \W
    public static Lexer AnyNonWordCharacter => segment =>
    {
        var value = segment.Peek();
        return !(Char.IsLetterOrDigit(value) || value == '_')
            ? Lexeme.Hit(segment, segment.Advance())
            : Lexeme.Miss(segment, segment.Advance());
    };

    // [\x0020|\x00a0]
    public static Lexer AnySeparator => segment => Char.IsSeparator(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment, segment.Advance());

    // \p{P}
    // or something like [.,;:!?()\{\}\[\]-]
    public static Lexer AnyPunctuation => segment => Char.IsPunctuation(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment, segment.Advance());

    // \a 
    // \u0007
    public static Lexer IsBell => Is('\a');

    // \b
    // \u0008
    public static Lexer IsBackspace => Is('\b');

    // \n
    public static Lexer IsNewLine => Is('\n');

    // \t
    // \u0009
    public static Lexer IsTab => Is('\t');

    // c
    public static Lexer Is(char c)
    {
        return segment => segment.Peek() == c
            ? Lexeme.Hit(segment, segment.Advance())
            : Lexeme.Miss(segment, segment.Advance());
    }

    // [cC]
    public static Lexer IsIgnoreCase(char c)
    {
        return segment =>
        {
            var value = segment.Peek();
            return value == c || Char.ToUpperInvariant(value) == Char.ToUpperInvariant(c)
                ? Lexeme.Hit(segment, segment.Advance())
                : Lexeme.Miss(segment, segment.Advance());
        };
    }

    // ^c
    public static Lexer IsNot(char c)
    {
        return segment => segment.Peek() != c
                ? Lexeme.Hit(segment, segment.Advance())
                : Lexeme.Miss(segment, segment.Advance());
    }

    // [^cC]
    public static Lexer IsNotIgnoreCase(char c)
    {
        return segment =>
        {
            var value = segment.Peek();
            return !(value == c || Char.ToUpperInvariant(value) == Char.ToUpperInvariant(c))
                ? Lexeme.Hit(segment, segment.Advance())
                : Lexeme.Miss(segment, segment.Advance());
        };
    }

    // [begin-end]
    public static Lexer InRange(char begin, char end)
    {
        return begin < end
            ? (segment =>
            {
                var value = segment.Peek();
                return Char.IsBetween(value, begin, end)
                    ? Lexeme.Hit(segment, segment.Advance())
                    : Lexeme.Miss(segment, segment.Advance());
            })
            : throw new ArgumentOutOfRangeException(nameof(begin));
    }

    // [set]
    public static Lexer In(params char[] set)
    {
        return segment =>
        {
            var value = segment.Peek();
            return Array.IndexOf(set, value) > -1
                ? Lexeme.Hit(segment, segment.Advance())
                : Lexeme.Miss(segment, segment.Advance());
        };
    }

    // [set|SET]
    public static Lexer InIgnoreCase(params char[] set)
    {
        return segment =>
        {
            var value = Char.ToUpperInvariant(segment.Peek());
            return Array.FindIndex(set, c => Char.ToUpperInvariant(c) == value) > -1
                ? Lexeme.Hit(segment, segment.Advance())
                : Lexeme.Miss(segment, segment.Advance());
        };
    }

    // [^set]
    public static Lexer NotIn(char[] set)
    {
        return segment =>
        {
            var value = segment.Peek();
            return Array.IndexOf(set, value) == -1
                ? Lexeme.Hit(segment, segment.Advance())
                : Lexeme.Miss(segment, segment.Advance());
        };
    }

    // [^set|SET]
    public static Lexer NotInIgnoreCase(char[] set)
    {
        return segment =>
        {
            var value = Char.ToUpperInvariant(segment.Peek());
            return Array.FindIndex(set, c => Char.ToUpperInvariant(c) == value) == -1
                ? Lexeme.Hit(segment, segment.Advance())
                : Lexeme.Miss(segment, segment.Advance());
        };
    }
}
