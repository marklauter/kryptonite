using Luthor.Classifiers;
using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests.Classifiers;

[ExcludeFromCodeCoverage]
public sealed class StringsTests
{
    [Theory]
    [InlineData("abcd", "abcd", true)]
    [InlineData("abcd", "xxxx", false)]
    [InlineData("abcd", "abc", true)]
    [InlineData("abc", "abcd", false)]
    public void Is(string source, string pattern, bool expectedSuccess)
    {
        var lexeme = Strings.Is(pattern)(source);
        Assert.Equal(expectedSuccess, lexeme.Success);
        if (lexeme.Success)
        {
            Assert.Equal(pattern, lexeme);
        }
    }

    [Theory]
    [InlineData("abcd", "abcd", true)]
    [InlineData("abcd", "ABCD", true)]
    [InlineData("ABCD", "abcd", true)]
    [InlineData("xxx", "xxxx", false)]
    public void IsIgnoreCase(string source, string pattern, bool expectedSuccess)
    {
        var lexeme = Strings.Is(pattern, true)(source);
        Assert.Equal(expectedSuccess, lexeme.Success);
        if (lexeme.Success)
        {
            Assert.Equal(pattern, lexeme, true);
        }
    }
}
