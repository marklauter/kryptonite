namespace Luthor.Lexers;

public static class CharacterClasses
{
    // [0-9]
    // \d
    public static Lexer IsDigit => segment => Char.IsDigit(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment);

    // [0-9a-fA-F]
    public static Lexer IsHexDigit => segment => Char.IsAsciiHexDigit(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment);

    // \D
    public static Lexer IsNonDigit => segment => !Char.IsDigit(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment);

    // [a-zA-Z]
    public static Lexer IsLetter => segment => Char.IsLetter(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment);

    // [^a-zA-Z]
    public static Lexer IsNonLetter => segment => !Char.IsLetter(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment);

    // [ |\t|\n|\r|\v|\f]
    // \s
    public static Lexer IsWhitespace => segment => Char.IsWhiteSpace(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment);

    // [^ |\t|\n|\r|\v|\f]
    // \S
    public static Lexer IsNonWhitespace => segment => !Char.IsWhiteSpace(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment);

    // .
    public static Lexer IsNonNewLine => segment => segment.Peek() != '\n'
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment);

    // [a-z]
    public static Lexer IsLowerCase => segment => Char.IsLower(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment);

    // [A-Z]
    public static Lexer IsUpperCase => segment => Char.IsUpper(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment);

    // [\s\S]
    public static Lexer IsCharacter => segment => Lexeme.Hit(segment, segment.Advance());

    // \w
    public static Lexer IsWordCharacter => segment =>
    {
        var value = segment.Peek();
        return Char.IsLetterOrDigit(value) || value == '_'
            ? Lexeme.Hit(segment, segment.Advance())
            : Lexeme.Miss(segment);
    };

    // \W
    public static Lexer IsNonWordCharacter => segment =>
    {
        var value = segment.Peek();
        return !(Char.IsLetterOrDigit(value) || value == '_')
            ? Lexeme.Hit(segment, segment.Advance())
            : Lexeme.Miss(segment);
    };

    // [\x0020|\x00a0]
    public static Lexer IsSeparator => segment => Char.IsSeparator(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment);

    // \p{P}
    // or something like [.,;:!?()\{\}\[\]-]
    public static Lexer IsPunctuation => segment => Char.IsPunctuation(segment.Peek())
        ? Lexeme.Hit(segment, segment.Advance())
        : Lexeme.Miss(segment);

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
            : Lexeme.Miss(segment);
    }

    // [cC]
    public static Lexer IsIgnoreCase(char c)
    {
        return segment =>
        {
            var value = segment.Peek();
            return value == c || Char.ToUpperInvariant(value) == Char.ToUpperInvariant(c)
                ? Lexeme.Hit(segment, segment.Advance())
                : Lexeme.Miss(segment);
        };
    }

    // ^c
    public static Lexer IsNot(char c)
    {
        return segment => segment.Peek() != c
                ? Lexeme.Hit(segment, segment.Advance())
                : Lexeme.Miss(segment);
    }

    // [^cC]
    public static Lexer IsNotIgnoreCase(char c)
    {
        return segment =>
        {
            var value = segment.Peek();
            return !(value == c || Char.ToUpperInvariant(value) == Char.ToUpperInvariant(c))
                ? Lexeme.Hit(segment, segment.Advance())
                : Lexeme.Miss(segment);
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
                    : Lexeme.Miss(segment);
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
                : Lexeme.Miss(segment);
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
                : Lexeme.Miss(segment);
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
                : Lexeme.Miss(segment);
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
                : Lexeme.Miss(segment);
        };
    }
}
