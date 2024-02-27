using Luthor.Classifiers;
using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests.Classifiers;

[ExcludeFromCodeCoverage]
public sealed class TextTests
{
    [Theory]
    [InlineData("abcd", "abcd", true)]
    [InlineData("abcd", "xxxx", false)]
    [InlineData("abcd", "abc", true)]
    [InlineData("abc", "abcd", false)]
    public void Is(string source, string pattern, bool expectedSuccess)
    {
        var lexeme = Text.Is(pattern)(source);
        Assert.Equal(expectedSuccess, lexeme.Matched);
        if (lexeme.Matched)
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
        var lexeme = Text.Is(pattern, true)(source);
        Assert.Equal(expectedSuccess, lexeme.Matched);
        if (lexeme.Matched)
        {
            Assert.Equal(pattern, lexeme, true);
        }
    }

    [Theory]
    [InlineData("_", "_", true)]
    [InlineData("_ = 1", "_", true)]
    [InlineData("_a", "_a", true)]
    [InlineData("_a = 2", "_a", true)]
    [InlineData("_a_b_c_d_", "_a_b_c_d_", true)]
    [InlineData("_a_b_c_d_1", "_a_b_c_d_1", true)]
    [InlineData("abc", "abc", true)]
    [InlineData("abc = 3", "abc", true)]
    [InlineData("1xxx", "1xxx", false)]
    public void CSharpIdentifier(string source, string expectedMatch, bool expectedSuccess)
    {
        var lexeme = Text.CIdentifier(source);
        Assert.Equal(expectedSuccess, lexeme.Matched);
        if (lexeme.Matched)
        {
            Assert.Equal(expectedMatch, lexeme, true);
        }
    }

    [Theory]
    [InlineData(@"""hello, world.""", "hello, world.", true)]
    [InlineData(@"""hello, \""world.\""""", @"hello, \""world.\""", true)]
    [InlineData(@"""hello, \""world.\"""" ""this is string two""", @"hello, \""world.\""", true)]
    public void StringLiteral(string source, string expectedMatch, bool expectedSuccess)
    {
        var lexeme = Text.StringLiteral('"')(source);
        Assert.Equal(expectedSuccess, lexeme.Matched);
        if (lexeme.Matched)
        {
            Assert.Equal(expectedMatch, lexeme.AsSpan()[1..^1]);
        }
    }
}
