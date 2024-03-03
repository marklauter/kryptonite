using System.Runtime.CompilerServices;

namespace Luthor;

public static class Character
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Char(char expectedChar) =>
        Parser.Satisfy(actualChar => actualChar == expectedChar);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Digit() =>
        Parser.Satisfy(System.Char.IsDigit);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Lower() =>
        Parser.Satisfy(System.Char.IsLower);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Upper() =>
        Parser.Satisfy(System.Char.IsUpper);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Parser<char> Any() => Parser.Item();
}
