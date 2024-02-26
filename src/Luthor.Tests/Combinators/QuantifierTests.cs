using Luthor.Classifiers;
using Luthor.Combinators;
using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests.Combinators;

[ExcludeFromCodeCoverage]
public sealed class QuantifierTests
{
    [Theory]
    [InlineData("", "", 'a')]
    [InlineData("a", "a", 'a')]
    [InlineData("aa", "aa", 'a')]
    [InlineData("aaa", "aaa", 'a')]
    [InlineData("ab", "a", 'a')]
    [InlineData("ba", "", 'a')]
    public void ZeroOrMore(string source, string expectedMatch, char c)
    {
        var lexer = Characters.Is(c).ZeroOrMore();
        var lexeme = lexer(source);
        Assert.True(lexeme.Success);
        Assert.Equal(expectedMatch, lexeme);
    }
}
