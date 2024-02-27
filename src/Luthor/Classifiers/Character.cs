using Luthor.Lexers;

namespace Luthor.Classifiers;

/// <summary>
/// matches against first character of input
/// </summary>
public static class Character
{
    // [\s\S]
    public static Lexer Any => Lexeme.Hit;

    // [0-9]
    // \d
    public static Lexer IsDigit => input => Char.IsDigit(input.Look())
        ? Lexeme.Hit(input)
        : Lexeme.Miss(input);

    // [0-9a-fA-F]
    public static Lexer IsHexDigit => input => Char.IsAsciiHexDigit(input.Look())
        ? Lexeme.Hit(input)
        : Lexeme.Miss(input);

    // \D
    public static Lexer IsNonDigit => input => !Char.IsDigit(input.Look())
        ? Lexeme.Hit(input)
        : Lexeme.Miss(input);

    // [a-zA-Z]
    public static Lexer IsLetter => input => Char.IsLetter(input.Look())
        ? Lexeme.Hit(input)
        : Lexeme.Miss(input);

    // [^a-zA-Z]
    public static Lexer IsNonLetter => input => !Char.IsLetter(input.Look())
        ? Lexeme.Hit(input)
        : Lexeme.Miss(input);

    // [ |\t|\n|\r|\v|\f]
    // \s
    public static Lexer IsWhitespace => input => Char.IsWhiteSpace(input.Look())
        ? Lexeme.Hit(input)
        : Lexeme.Miss(input);

    // [^ |\t|\n|\r|\v|\f]
    // \S
    public static Lexer IsNonWhitespace => input => !Char.IsWhiteSpace(input.Look())
        ? Lexeme.Hit(input)
        : Lexeme.Miss(input);

    // .
    public static Lexer IsNonNewLine => input => input.Look() != '\n'
        ? Lexeme.Hit(input)
        : Lexeme.Miss(input);

    // [a-z]
    public static Lexer IsLowerCase => input => Char.IsLower(input.Look())
        ? Lexeme.Hit(input)
        : Lexeme.Miss(input);

    // [A-Z]
    public static Lexer IsUpperCase => input => Char.IsUpper(input.Look())
        ? Lexeme.Hit(input)
        : Lexeme.Miss(input);

    // \w
    public static Lexer IsWordCharacter => input =>
    {
        var value = input.Look();
        return Char.IsLetterOrDigit(value) || value == '_'
            ? Lexeme.Hit(input)
            : Lexeme.Miss(input);
    };

    // \W
    public static Lexer IsNonWordCharacter => input =>
    {
        var value = input.Look();
        return !(Char.IsLetterOrDigit(value) || value == '_')
            ? Lexeme.Hit(input)
            : Lexeme.Miss(input);
    };

    // [\x0020|\x00a0]
    public static Lexer IsSeparator => input => Char.IsSeparator(input.Look())
        ? Lexeme.Hit(input)
        : Lexeme.Miss(input);

    // \p{P}
    // or something like [.,;:!?()\{\}\[\]-]
    public static Lexer IsPunctuation => input => Char.IsPunctuation(input.Look())
        ? Lexeme.Hit(input)
        : Lexeme.Miss(input);

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
    public static Lexer IsWordBoundary => input =>
    {
        var current = input.Look();
        if (!input.TryLookAhead(1, out var next))
        {
            // if we are at the end of the segment, then we are at a word boundary
            return Lexeme.Hit(input);
        }

        var isCurrentWordChar = Char.IsLetterOrDigit(current) || current == '_';
        var isNextWordChar = Char.IsLetterOrDigit(next) || next == '_';

        return isCurrentWordChar != isNextWordChar
            ? Lexeme.Hit(input)
            : Lexeme.Miss(input);
    };

    public static Lexer IsEndOfinput => input =>
        input.EndOfInput || input.Look() == Char.MinValue
            ? Lexeme.Hit(input)
            : Lexeme.Miss(input);

    // c
    public static Lexer Is(char c) => input =>
        input.Look() == c
            ? Lexeme.Hit(input)
            : Lexeme.Miss(input);

    // ^c
    public static Lexer IsNot(char c) => input =>
        input.Look() != c
            ? Lexeme.Hit(input)
            : Lexeme.Miss(input);

    // [cC]
    /// <summary>
    /// optional case insensitive comparison
    /// </summary>
    /// <param name="c"></param>
    /// <param name="ignoreCase"></param>
    /// <returns></returns>
    public static Lexer Is(char c, bool ignoreCase) =>
        !ignoreCase
            ? Is(c)
            : (input =>
            {
                var value = input.Look();
                return value == c || Char.ToUpperInvariant(value) == Char.ToUpperInvariant(c)
                    ? Lexeme.Hit(input)
                    : Lexeme.Miss(input);
            });

    // [^cC]
    public static Lexer IsNot(char c, bool ignoreCase) =>
        !ignoreCase
            ? IsNot(c)
            : (input =>
            {
                var value = input.Look();
                return !(value == c || Char.ToUpperInvariant(value) == Char.ToUpperInvariant(c))
                    ? Lexeme.Hit(input)
                    : Lexeme.Miss(input);
            });

    // [begin-end]
    public static Lexer InRange(char begin, char end) =>
        begin < end
        ? (input =>
        {
            var value = input.Look();
            return Char.IsBetween(value, begin, end)
                ? Lexeme.Hit(input)
                : Lexeme.Miss(input);
        })
        : throw new ArgumentOutOfRangeException(nameof(begin));

    // [set]
    public static Lexer In(params char[] set)
    {
        ArgumentNullException.ThrowIfNull(set);

        return input => set.AsSpan().IndexOf(input.Look()) > -1
            ? Lexeme.Hit(input)
            : Lexeme.Miss(input);
    }

    // [set|SET]
    public static Lexer InIgnoreCase(params char[] set)
    {
        ArgumentNullException.ThrowIfNull(set);

        var upperSet = Array.ConvertAll(set, Char.ToUpperInvariant);
        return input =>
        {
            var upperValue = Char.ToUpperInvariant(input.Look());
            return Array.FindIndex(upperSet, c => c == upperValue) > -1
                ? Lexeme.Hit(input)
                : Lexeme.Miss(input);
        };
    }

    // [^set]
    public static Lexer NotIn(params char[] set)
    {
        ArgumentNullException.ThrowIfNull(set);

        return input => Array.IndexOf(set, input.Look()) == -1
            ? Lexeme.Hit(input)
            : Lexeme.Miss(input);
    }

    // [^set|SET]
    public static Lexer NotInIgnoreCase(params char[] set)
    {
        ArgumentNullException.ThrowIfNull(set);

        var upperSet = Array.ConvertAll(set, Char.ToUpperInvariant);
        return input =>
        {
            var value = Char.ToUpperInvariant(input.Look());
            return Array.FindIndex(upperSet, c => c == value) == -1
                ? Lexeme.Hit(input)
                : Lexeme.Miss(input);
        };
    }
}
