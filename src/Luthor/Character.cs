using System.Runtime.CompilerServices;

namespace Luthor;

public static class Character
{
    // [\s\S]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Any() => Parser.Item();

    // [cC]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Char(char expectedChar) =>
        Parser.Satisfy(actualChar => actualChar == expectedChar);

    // [^cC]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> NotChar(char expectedChar) =>
        Parser.Satisfy(actualChar => actualChar != expectedChar);

    // [0-9]
    // \d
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Digit() =>
        Parser.Satisfy(System.Char.IsDigit);

    // [0-9a-fA-F]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> HexDigit() =>
        Parser.Satisfy(System.Char.IsAsciiHexDigit);

    // [^0-9]
    // \D
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> NonDigit() =>
        Parser.Satisfy(ch => !System.Char.IsDigit(ch));

    // [a-zA-Z]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Letter() =>
        Parser.Satisfy(System.Char.IsLetter);

    // [^a-zA-Z]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> NonLetter() =>
        Parser.Satisfy(ch => !System.Char.IsLetter(ch));

    // [ |\t|\n|\r|\v|\f]
    // \s
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Whitespace() =>
        Parser.Satisfy(System.Char.IsWhiteSpace);

    // [^ |\t|\n|\r|\v|\f]
    // \S
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> NonWhitespace() =>
        Parser.Satisfy(ch => !System.Char.IsWhiteSpace(ch));

    // [\n]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> NewLine() =>
        Parser.Satisfy(ch => ch == '\n');

    // \.
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> NonNewLine() =>
        Parser.Satisfy(ch => ch != '\n');

    // [\x0020|\x00a0]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Seperator() =>
        Parser.Satisfy(System.Char.IsSeparator);

    // \p{P}
    // or something like [.,;:!?()\{\}\[\]-]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Punctuation() =>
        Parser.Satisfy(System.Char.IsPunctuation);

    // [\a|\u0007]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Bell() =>
        Parser.Satisfy(ch => ch == '\a');

    // [\b|\u0008]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Backspace() =>
        Parser.Satisfy(ch => ch == '\b');

    // [\t|\u0009]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Tab() =>
        Parser.Satisfy(ch => ch == '\t');

    // [a-z]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Lower() =>
        Parser.Satisfy(System.Char.IsLower);

    // [A-Z]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Upper() =>
        Parser.Satisfy(System.Char.IsUpper);

    // \w
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Word() =>
        Parser.Satisfy(ch => System.Char.IsLetterOrDigit(ch) || ch == '_');

    // \W
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> NonWord() =>
        Parser.Satisfy(ch => !System.Char.IsLetterOrDigit(ch) && ch != '_');

    // []
    // EOF
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Eof() =>
        Parser.Satisfy(ch => ch == System.Char.MinValue);

    // [chars]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> In(params char[] chars) =>
        Parser.Satisfy(ch => Array.IndexOf(chars, ch) > -1);

    // [charsCHARS]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> InIgnoreCase(params char[] chars)
    {
        chars = Array.ConvertAll(chars, System.Char.ToUpperInvariant);
        return Parser.Satisfy(ch => Array.IndexOf(chars, System.Char.ToUpperInvariant(ch)) > -1);
    }

    // [^chars]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> NotIn(params char[] chars) =>
        Parser.Satisfy(ch => Array.IndexOf(chars, ch) == -1);

    // [^charsCHARS]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> NotInIgnoreCase(params char[] chars)
    {
        chars = Array.ConvertAll(chars, System.Char.ToUpperInvariant);
        return Parser.Satisfy(ch => Array.IndexOf(chars, System.Char.ToUpperInvariant(ch)) == -1);
    }

    // [chars]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Between(char minInclusive, char maxInclusive) =>
        Parser.Satisfy(ch => System.Char.IsBetween(ch, minInclusive, maxInclusive));
}

