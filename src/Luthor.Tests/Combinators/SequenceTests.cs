using Luthor.Classifiers;
using Luthor.Combinators;
using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests.Combinators;

[ExcludeFromCodeCoverage]
public sealed class SequenceTests
{
    [Theory]
    [InlineData("1", '1', '2', true)]
    [InlineData("1", '2', '1', true)]
    [InlineData("a", 'x', 'x', false)]
    public void Or(string source, char lch, char rch, bool expectedMatch)
    {
        var lexer = Characters
            .Is(lch)
            .Or(Characters.Is(rch));

        var lexeme = lexer(source);
        Assert.Equal(expectedMatch, lexeme.Success);
    }

    [Theory]
    [InlineData("12", '1', '2', true)]
    [InlineData("12", '1', '1', false)]
    [InlineData("12", '2', '2', false)]
    [InlineData("12", '2', '1', false)]
    [InlineData("ab", 'x', 'x', false)]
    public void And(string source, char lch, char rch, bool expectedMatch)
    {
        var lexer = Characters
            .Is(lch)
            .And(Characters.Is(rch));

        var lexeme = lexer(source);
        Assert.Equal(expectedMatch, lexeme.Success);
    }

    [Theory]
    [InlineData("11", '1', '2', false)]
    [InlineData("22", '1', '2', false)]
    [InlineData("11", '1', '1', true)]
    public void Then(string source, char lch, char rch, bool expectedMatch)
    {
        var lexer = Characters
            .Is(lch)
            .Then(Characters.Is(rch));

        var lexeme = lexer(source);
        Assert.Equal(expectedMatch, lexeme.Success);
    }
}
