namespace Luthor.Classifiers;

/// <summary>
/// matches against first character of input
/// </summary>
public static class Character
{
    // [\s\S]
    public static Parser<ParseResult> Any => ParseResult.Hit;

    // [0-9]
    // \d
    public static Parser<ParseResult> IsDigit => input => Char.IsDigit(input.Peek())
        ? ParseResult.Hit(input)
        : ParseResult.Miss(input);

    // [0-9a-fA-F]
    public static Parser<ParseResult> IsHexDigit => input => Char.IsAsciiHexDigit(input.Peek())
        ? ParseResult.Hit(input)
        : ParseResult.Miss(input);

    // \D
    public static Parser<ParseResult> IsNonDigit => input => !Char.IsDigit(input.Peek())
        ? ParseResult.Hit(input)
        : ParseResult.Miss(input);

    // [a-zA-Z]
    public static Parser<ParseResult> IsLetter => input => Char.IsLetter(input.Peek())
        ? ParseResult.Hit(input)
        : ParseResult.Miss(input);

    // [^a-zA-Z]
    public static Parser<ParseResult> IsNonLetter => input => !Char.IsLetter(input.Peek())
        ? ParseResult.Hit(input)
        : ParseResult.Miss(input);

    // [ |\t|\n|\r|\v|\f]
    // \s
    public static Parser<ParseResult> IsWhitespace => input => Char.IsWhiteSpace(input.Peek())
        ? ParseResult.Hit(input)
        : ParseResult.Miss(input);

    // [^ |\t|\n|\r|\v|\f]
    // \S
    public static Parser<ParseResult> IsNonWhitespace => input => !Char.IsWhiteSpace(input.Peek())
        ? ParseResult.Hit(input)
        : ParseResult.Miss(input);

    // .
    public static Parser<ParseResult> IsNonNewLine => input => input.Peek() != '\n'
        ? ParseResult.Hit(input)
        : ParseResult.Miss(input);

    // [a-z]
    public static Parser<ParseResult> IsLowerCase => input => Char.IsLower(input.Peek())
        ? ParseResult.Hit(input)
        : ParseResult.Miss(input);

    // [A-Z]
    public static Parser<ParseResult> IsUpperCase => input => Char.IsUpper(input.Peek())
        ? ParseResult.Hit(input)
        : ParseResult.Miss(input);

    // \w
    public static Parser<ParseResult> IsWordCharacter => input =>
    {
        var value = input.Peek();
        return Char.IsLetterOrDigit(value) || value == '_'
            ? ParseResult.Hit(input)
            : ParseResult.Miss(input);
    };

    // \W
    public static Parser<ParseResult> IsNonWordCharacter => input =>
    {
        var value = input.Peek();
        return !(Char.IsLetterOrDigit(value) || value == '_')
            ? ParseResult.Hit(input)
            : ParseResult.Miss(input);
    };

    // [\x0020|\x00a0]
    public static Parser<ParseResult> IsSeparator => input => Char.IsSeparator(input.Peek())
        ? ParseResult.Hit(input)
        : ParseResult.Miss(input);

    // \p{P}
    // or something like [.,;:!?()\{\}\[\]-]
    public static Parser<ParseResult> IsPunctuation => input => Char.IsPunctuation(input.Peek())
        ? ParseResult.Hit(input)
        : ParseResult.Miss(input);

    // [\a|\u0007]
    public static Parser<ParseResult> IsBell => Is('\a');

    // [\b|\u0008]
    public static Parser<ParseResult> IsBackspace => Is('\b');

    // \n
    public static Parser<ParseResult> IsNewLine => Is('\n');

    // \t
    // \u0009
    public static Parser<ParseResult> IsTab => Is('\t');

    // for example an identifier would look something like this [\w]+\b where \b is word boundary
    public static Parser<ParseResult> IsWordBoundary => input =>
    {
        var current = input.Peek();
        if (!input.TryPeekAhead(1, out var next))
        {
            // if we are at the end of the segment, then we are at a word boundary
            return ParseResult.Hit(input);
        }

        var isCurrentWordChar = Char.IsLetterOrDigit(current) || current == '_';
        var isNextWordChar = Char.IsLetterOrDigit(next) || next == '_';

        return isCurrentWordChar != isNextWordChar
            ? ParseResult.Hit(input)
            : ParseResult.Miss(input);
    };

    public static Parser<ParseResult> IsEndOfinput => input =>
        input.EndOfInput || input.Peek() == Char.MinValue
            ? ParseResult.Hit(input)
            : ParseResult.Miss(input);

    // c
    public static Parser<ParseResult> Is(char c) => input =>
        input.Peek() == c
            ? ParseResult.Hit(input)
            : ParseResult.Miss(input);

    // ^c
    public static Parser<ParseResult> IsNot(char c) => input =>
        input.Peek() != c
            ? ParseResult.Hit(input)
            : ParseResult.Miss(input);

    // [cC]
    /// <summary>
    /// optional case insensitive comparison
    /// </summary>
    /// <param name="c"></param>
    /// <param name="ignoreCase"></param>
    /// <returns></returns>
    public static Parser<ParseResult> Is(char c, bool ignoreCase) =>
        !ignoreCase
            ? Is(c)
            : (input =>
            {
                var value = input.Peek();
                return value == c || Char.ToUpperInvariant(value) == Char.ToUpperInvariant(c)
                    ? ParseResult.Hit(input)
                    : ParseResult.Miss(input);
            });

    // [^cC]
    public static Parser<ParseResult> IsNot(char c, bool ignoreCase) =>
        !ignoreCase
            ? IsNot(c)
            : (input =>
            {
                var value = input.Peek();
                return !(value == c || Char.ToUpperInvariant(value) == Char.ToUpperInvariant(c))
                    ? ParseResult.Hit(input)
                    : ParseResult.Miss(input);
            });

    // [begin-end]
    public static Parser<ParseResult> InRange(char begin, char end) =>
        begin < end
        ? (input =>
        {
            var value = input.Peek();
            return Char.IsBetween(value, begin, end)
                ? ParseResult.Hit(input)
                : ParseResult.Miss(input);
        })
        : throw new ArgumentOutOfRangeException(nameof(begin));

    // [set]
    public static Parser<ParseResult> In(params char[] set)
    {
        ArgumentNullException.ThrowIfNull(set);

        return input => set.AsSpan().IndexOf(input.Peek()) > -1
            ? ParseResult.Hit(input)
            : ParseResult.Miss(input);
    }

    // [set|SET]
    public static Parser<ParseResult> InIgnoreCase(params char[] set)
    {
        ArgumentNullException.ThrowIfNull(set);

        var upperSet = Array.ConvertAll(set, Char.ToUpperInvariant);
        return input =>
        {
            var upperValue = Char.ToUpperInvariant(input.Peek());
            return Array.FindIndex(upperSet, c => c == upperValue) > -1
                ? ParseResult.Hit(input)
                : ParseResult.Miss(input);
        };
    }

    // [^set]
    public static Parser<ParseResult> NotIn(params char[] set)
    {
        ArgumentNullException.ThrowIfNull(set);

        return input => Array.IndexOf(set, input.Peek()) == -1
            ? ParseResult.Hit(input)
            : ParseResult.Miss(input);
    }

    // [^set|SET]
    public static Parser<ParseResult> NotInIgnoreCase(params char[] set)
    {
        ArgumentNullException.ThrowIfNull(set);

        var upperSet = Array.ConvertAll(set, Char.ToUpperInvariant);
        return input =>
        {
            var value = Char.ToUpperInvariant(input.Peek());
            return Array.FindIndex(upperSet, c => c == value) == -1
                ? ParseResult.Hit(input)
                : ParseResult.Miss(input);
        };
    }
}
