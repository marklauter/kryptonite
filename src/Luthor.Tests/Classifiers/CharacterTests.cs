using Luthor.Classifiers;
using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests.Classifiers;

[ExcludeFromCodeCoverage]
public sealed class CharacterTests
{
    [Theory]
    [InlineData("1", true)]
    [InlineData("b", false)]
    public void AnyDigit(string source, bool expectedSuccess)
    {
        var lexeme = Character.IsDigit(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData("0", true)]
    [InlineData("1", true)]
    [InlineData("2", true)]
    [InlineData("3", true)]
    [InlineData("4", true)]
    [InlineData("5", true)]
    [InlineData("6", true)]
    [InlineData("7", true)]
    [InlineData("8", true)]
    [InlineData("9", true)]
    [InlineData("a", true)]
    [InlineData("b", true)]
    [InlineData("c", true)]
    [InlineData("d", true)]
    [InlineData("e", true)]
    [InlineData("f", true)]
    [InlineData("A", true)]
    [InlineData("B", true)]
    [InlineData("C", true)]
    [InlineData("D", true)]
    [InlineData("E", true)]
    [InlineData("F", true)]
    public void AnyHexDigit(string source, bool expectedSuccess)
    {
        var lexeme = Character.IsHexDigit(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData("1", false)]
    [InlineData("b", true)]
    [InlineData(".", true)]
    public void AnyNonDigit(string source, bool expectedSuccess)
    {
        var lexeme = Character.IsNonDigit(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData("1", false)]
    [InlineData("b", true)]
    [InlineData(".", false)]
    public void AnyLetter(string source, bool expectedSuccess)
    {
        var lexeme = Character.IsLetter(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData("1", true)]
    [InlineData("b", false)]
    [InlineData(".", true)]
    public void AnyNonLetter(string source, bool expectedSuccess)
    {
        var lexeme = Character.IsNonLetter(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData(" ", true)]
    [InlineData("\n", true)]
    [InlineData("\r", true)]
    [InlineData("a", false)]
    [InlineData("1", false)]
    [InlineData(".", false)]
    public void AnyWhitespace(string source, bool expectedSuccess)
    {
        var lexeme = Character.IsWhitespace(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData(" ", false)]
    [InlineData("\n", false)]
    [InlineData("\r", false)]
    [InlineData("a", true)]
    [InlineData("1", true)]
    [InlineData(".", true)]
    public void AnyNonWhitespace(string source, bool expectedSuccess)
    {
        var lexeme = Character.IsNonWhitespace(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData(" ", true)]
    [InlineData("\n", false)]
    [InlineData("\r", true)]
    [InlineData("a", true)]
    [InlineData("1", true)]
    [InlineData(".", true)]
    public void AnyNonNewLine(string source, bool expectedSuccess)
    {
        var lexeme = Character.IsNonNewLine(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData(" ", false)]
    [InlineData("\n", false)]
    [InlineData("\r", false)]
    [InlineData("a", true)]
    [InlineData("A", false)]
    [InlineData("1", false)]
    [InlineData(".", false)]
    public void AnyLowerCase(string source, bool expectedSuccess)
    {
        var lexeme = Character.IsLowerCase(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData(" ", false)]
    [InlineData("\n", false)]
    [InlineData("\r", false)]
    [InlineData("a", false)]
    [InlineData("A", true)]
    [InlineData("1", false)]
    [InlineData(".", false)]
    public void AnyUpperCase(string source, bool expectedSuccess)
    {
        var lexeme = Character.IsUpperCase(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData(" ", true)]
    [InlineData("\n", true)]
    [InlineData("\r", true)]
    [InlineData("a", true)]
    [InlineData("A", true)]
    [InlineData("1", true)]
    [InlineData(".", true)]
    public void AnyCharacter(string source, bool expectedSuccess)
    {
        var lexeme = Character.Any(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData(" ", false)]
    [InlineData("\n", false)]
    [InlineData("\r", false)]
    [InlineData("a", true)]
    [InlineData("A", true)]
    [InlineData("1", true)]
    [InlineData("_", true)]
    [InlineData(".", false)]
    public void AnyWordCharacter(string source, bool expectedSuccess)
    {
        var lexeme = Character.IsWordCharacter(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData(" ", true)]
    [InlineData("\n", true)]
    [InlineData("\r", true)]
    [InlineData("a", false)]
    [InlineData("A", false)]
    [InlineData("1", false)]
    [InlineData("_", false)]
    [InlineData(".", true)]
    public void AnyNonWordCharacter(string source, bool expectedSuccess)
    {
        var lexeme = Character.IsNonWordCharacter(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData(" ", true)]
    [InlineData("\u2028", true)]
    [InlineData("\u2029", true)]
    [InlineData("\n", false)]
    [InlineData("\r", false)]
    [InlineData("a", false)]
    [InlineData("A", false)]
    [InlineData("1", false)]
    [InlineData("_", false)]
    [InlineData(".", false)]
    public void AnySeparator(string source, bool expectedSuccess)
    {
        var lexeme = Character.IsSeparator(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData(" ", false)]
    [InlineData("\n", false)]
    [InlineData("\r", false)]
    [InlineData("a", false)]
    [InlineData("A", false)]
    [InlineData("1", false)]
    [InlineData("_", true)]
    [InlineData(".", true)]
    [InlineData("!", true)]
    [InlineData("?", true)]
    [InlineData(";", true)]
    [InlineData(",", true)]
    [InlineData(":", true)]
    public void AnyPunctuation(string source, bool expectedSuccess)
    {
        var lexeme = Character.IsPunctuation(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData("a", 'a', true)]
    [InlineData("A", 'A', true)]
    [InlineData("A", 'a', false)]
    [InlineData("a", 'A', false)]
    [InlineData("b", 'a', false)]
    public void Is(string source, char pattern, bool expectedSuccess)
    {
        var lexeme = Character.Is(pattern)(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData("a", 'a', true)]
    [InlineData("A", 'A', true)]
    [InlineData("A", 'a', true)]
    [InlineData("a", 'A', true)]
    [InlineData("b", 'a', false)]
    public void IsIgnoreCase(string source, char pattern, bool expectedSuccess)
    {
        var lexeme = Character.Is(pattern, true)(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData("a", 'a', false)]
    [InlineData("b", 'a', true)]
    public void IsNot(string source, char pattern, bool expectedSuccess)
    {
        var lexeme = Character.IsNot(pattern)(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData("a", 'a', false)]
    [InlineData("A", 'A', false)]
    [InlineData("A", 'a', false)]
    [InlineData("a", 'A', false)]
    [InlineData("b", 'a', true)]
    public void IsNotIgnoreCase(string source, char pattern, bool expectedSuccess)
    {
        var lexeme = Character.IsNot(pattern, true)(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Fact]
    public void IsBell()
    {
        var lexeme = Character.IsBell("\a");
        Assert.True(lexeme.HasValue);
    }

    [Fact]
    public void IsBackspace()
    {
        var lexeme = Character.IsBackspace("\b");
        Assert.True(lexeme.HasValue);
    }

    [Fact]
    public void IsNewLine()
    {
        var lexeme = Character.IsNewLine("\n");
        Assert.True(lexeme.HasValue);
    }

    [Fact]
    public void IsTab()
    {
        var lexeme = Character.IsTab("\t");
        Assert.True(lexeme.HasValue);
    }

    [Theory]
    [InlineData("a", 'a', 'z', true)]
    [InlineData("b", 'a', 'z', true)]
    [InlineData("z", 'a', 'z', true)]
    [InlineData("A", 'a', 'z', false)]
    [InlineData("A", 'A', 'Z', true)]
    [InlineData("B", 'A', 'Z', true)]
    [InlineData("Z", 'A', 'Z', true)]
    [InlineData("a", 'A', 'Z', false)]
    public void InRange(string source, char begin, char end, bool expectedSuccess)
    {
        var lexeme = Character.InRange(begin, end)(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Fact]
    public void InRange_Throws_When_Begin_Is_Greater_Than_End()
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Character.InRange('z', 'a'));
        Assert.Contains("begin", ex.Message);
    }

    [Theory]
    [InlineData("a", new[] { 'a', 'b' }, true)]
    [InlineData("b", new[] { 'a', 'b' }, true)]
    [InlineData("A", new[] { 'a', 'b' }, false)]
    [InlineData("B", new[] { 'a', 'b' }, false)]
    [InlineData("z", new[] { 'a', 'b' }, false)]
    [InlineData("Z", new[] { 'a', 'b' }, false)]
    public void In(string source, char[] set, bool expectedSuccess)
    {
        var lexeme = Character.In(set)(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData("a", new[] { 'a', 'b' }, true)]
    [InlineData("b", new[] { 'a', 'b' }, true)]
    [InlineData("A", new[] { 'a', 'b' }, true)]
    [InlineData("B", new[] { 'a', 'b' }, true)]
    [InlineData("a", new[] { 'A', 'B' }, true)]
    [InlineData("b", new[] { 'A', 'B' }, true)]
    [InlineData("A", new[] { 'A', 'B' }, true)]
    [InlineData("B", new[] { 'A', 'B' }, true)]
    [InlineData("z", new[] { 'a', 'b' }, false)]
    [InlineData("Z", new[] { 'a', 'b' }, false)]
    public void InIgnoreCase(string source, char[] set, bool expectedSuccess)
    {
        var lexeme = Character.InIgnoreCase(set)(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData("a", new[] { 'a', 'b' }, false)]
    [InlineData("b", new[] { 'a', 'b' }, false)]
    [InlineData("A", new[] { 'a', 'b' }, true)]
    [InlineData("B", new[] { 'a', 'b' }, true)]
    [InlineData("z", new[] { 'a', 'b' }, true)]
    [InlineData("Z", new[] { 'a', 'b' }, true)]
    public void NotIn(string source, char[] set, bool expectedSuccess)
    {
        var lexeme = Character.NotIn(set)(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }

    [Theory]
    [InlineData("a", new[] { 'a', 'b' }, false)]
    [InlineData("b", new[] { 'a', 'b' }, false)]
    [InlineData("A", new[] { 'a', 'b' }, false)]
    [InlineData("B", new[] { 'a', 'b' }, false)]
    [InlineData("a", new[] { 'A', 'B' }, false)]
    [InlineData("b", new[] { 'A', 'B' }, false)]
    [InlineData("A", new[] { 'A', 'B' }, false)]
    [InlineData("B", new[] { 'A', 'B' }, false)]
    [InlineData("z", new[] { 'a', 'b' }, true)]
    [InlineData("Z", new[] { 'a', 'b' }, true)]
    public void NotInIgnoreCase(string source, char[] set, bool expectedSuccess)
    {
        var lexeme = Character.NotInIgnoreCase(set)(source);
        Assert.Equal(expectedSuccess, lexeme.HasValue);
    }
}
