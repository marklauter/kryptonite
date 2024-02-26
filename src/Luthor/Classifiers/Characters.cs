using Luthor.Lexers;

namespace Luthor.Classifiers;

/// <summary>
/// matches the first character of a string segment
/// </summary>
public static class Characters
{
    // [0-9]
    // \d
    public static Lexer IsDigit => segment => Char.IsDigit(segment.Look())
        ? Lexeme.Hit(segment)
        : Lexeme.Miss(segment);

    // [0-9a-fA-F]
    public static Lexer IsHexDigit => segment => Char.IsAsciiHexDigit(segment.Look())
        ? Lexeme.Hit(segment)
        : Lexeme.Miss(segment);

    // \D
    public static Lexer IsNonDigit => segment => !Char.IsDigit(segment.Look())
        ? Lexeme.Hit(segment)
        : Lexeme.Miss(segment);

    // [a-zA-Z]
    public static Lexer IsLetter => segment => Char.IsLetter(segment.Look())
        ? Lexeme.Hit(segment)
        : Lexeme.Miss(segment);

    // [^a-zA-Z]
    public static Lexer IsNonLetter => segment => !Char.IsLetter(segment.Look())
        ? Lexeme.Hit(segment)
        : Lexeme.Miss(segment);

    // [ |\t|\n|\r|\v|\f]
    // \s
    public static Lexer IsWhitespace => segment => Char.IsWhiteSpace(segment.Look())
        ? Lexeme.Hit(segment)
        : Lexeme.Miss(segment);

    // [^ |\t|\n|\r|\v|\f]
    // \S
    public static Lexer IsNonWhitespace => segment => !Char.IsWhiteSpace(segment.Look())
        ? Lexeme.Hit(segment)
        : Lexeme.Miss(segment);

    // .
    public static Lexer IsNonNewLine => segment => segment.Look() != '\n'
        ? Lexeme.Hit(segment)
        : Lexeme.Miss(segment);

    // [a-z]
    public static Lexer IsLowerCase => segment => Char.IsLower(segment.Look())
        ? Lexeme.Hit(segment)
        : Lexeme.Miss(segment);

    // [A-Z]
    public static Lexer IsUpperCase => segment => Char.IsUpper(segment.Look())
        ? Lexeme.Hit(segment)
        : Lexeme.Miss(segment);

    // [\s\S]
    public static Lexer IsCharacter => Lexeme.Hit;

    // \w
    public static Lexer IsWordCharacter => segment =>
    {
        var value = segment.Look();
        return Char.IsLetterOrDigit(value) || value == '_'
            ? Lexeme.Hit(segment)
            : Lexeme.Miss(segment);
    };

    // \W
    public static Lexer IsNonWordCharacter => segment =>
    {
        var value = segment.Look();
        return !(Char.IsLetterOrDigit(value) || value == '_')
            ? Lexeme.Hit(segment)
            : Lexeme.Miss(segment);
    };

    // [\x0020|\x00a0]
    public static Lexer IsSeparator => segment => Char.IsSeparator(segment.Look())
        ? Lexeme.Hit(segment)
        : Lexeme.Miss(segment);

    // \p{P}
    // or something like [.,;:!?()\{\}\[\]-]
    public static Lexer IsPunctuation => segment => Char.IsPunctuation(segment.Look())
        ? Lexeme.Hit(segment)
        : Lexeme.Miss(segment);

    // [\a|\u0007]
    public static Lexer IsBell => Is('\a');

    // [\b|\u0008]
    public static Lexer IsBackspace => Is('\b');

    // \n
    public static Lexer IsNewLine => Is('\n');

    // \t
    // \u0009
    public static Lexer IsTab => Is('\t');

    // for example an identifier would look something like this [\w]+\b where \b is word boundary
    public static Lexer IsWordBoundary => segment =>
    {
        var current = segment.Look();
        var next = segment.LookAhead(1);

        var isCurrentWordChar = Char.IsLetterOrDigit(current) || current == '_';
        var isNextWordChar = Char.IsLetterOrDigit(next) || next == '_';

        return isCurrentWordChar != isNextWordChar
            ? Lexeme.Hit(segment)
            : Lexeme.Miss(segment);
    };

    // c
    /// <summary>
    /// case sensitive comparison
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static Lexer Is(char c) => segment =>
        segment.Look() == c
            ? Lexeme.Hit(segment)
            : Lexeme.Miss(segment);

    // [cC]
    /// <summary>
    /// optional case insensitive comparison
    /// </summary>
    /// <param name="c"></param>
    /// <param name="ignoreCase"></param>
    /// <returns></returns>
    public static Lexer Is(char c, bool ignoreCase) => segment =>
    {
        var value = segment.Look();
        return value == c || ignoreCase && Char.ToUpperInvariant(value) == Char.ToUpperInvariant(c)
            ? Lexeme.Hit(segment)
            : Lexeme.Miss(segment);
    };

    // ^c
    public static Lexer IsNot(char c) => segment =>
        segment.Look() != c
            ? Lexeme.Hit(segment)
            : Lexeme.Miss(segment);

    // [^cC]
    public static Lexer IsNot(char c, bool ignoreCase) => segment =>
    {
        var value = segment.Look();
        return !(value == c || ignoreCase && Char.ToUpperInvariant(value) == Char.ToUpperInvariant(c))
            ? Lexeme.Hit(segment)
            : Lexeme.Miss(segment);
    };

    // [begin-end]
    public static Lexer InRange(char begin, char end) =>
        begin < end
        ? (segment =>
        {
            var value = segment.Look();
            return Char.IsBetween(value, begin, end)
                ? Lexeme.Hit(segment)
                : Lexeme.Miss(segment);
        })
        : throw new ArgumentOutOfRangeException(nameof(begin));

    // [set]
    public static Lexer In(params char[] set) => segment => set.AsSpan().IndexOf(segment.Look()) > -1
            ? Lexeme.Hit(segment)
            : Lexeme.Miss(segment);

    // [set|SET]
    public static Lexer InIgnoreCase(params char[] set)
    {
        var upperSet = Array.ConvertAll(set, Char.ToUpperInvariant);
        return segment =>
        {
            var upperValue = Char.ToUpperInvariant(segment.Look());
            return Array.FindIndex(upperSet, c => c == upperValue) > -1
                ? Lexeme.Hit(segment)
                : Lexeme.Miss(segment);
        };
    }

    // [^set]
    public static Lexer NotIn(params char[] set) => segment => Array.IndexOf(set, segment.Look()) == -1
            ? Lexeme.Hit(segment)
            : Lexeme.Miss(segment);

    // [^set|SET]
    public static Lexer NotInIgnoreCase(params char[] set)
    {
        var upperSet = Array.ConvertAll(set, Char.ToUpperInvariant);
        return segment =>
        {
            var value = Char.ToUpperInvariant(segment.Look());
            return Array.FindIndex(upperSet, c => c == value) == -1
                ? Lexeme.Hit(segment)
                : Lexeme.Miss(segment);
        };
    }
}
