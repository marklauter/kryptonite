using Luthor.Classifiers;
using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests;

[ExcludeFromCodeCoverage]
public sealed class CombinatorTests
{
    [Theory]
    [InlineData("1", '1', '2', true)]
    [InlineData("2", '1', '2', true)]
    [InlineData("a", '1', '2', false)]
    public void Or(string source, char lch, char rch, bool expectedMatch)
    {
        var lexer = CharacterClasses
            .Is(lch)
            .Or(CharacterClasses.Is(rch));

        var lexeme = lexer(source);
        Assert.Equal(expectedMatch, lexeme.Success);
    }

    [Theory]
    [InlineData("11", '1', '2', false)]
    [InlineData("22", '1', '2', false)]
    [InlineData("11", '1', '1', true)]
    public void Then(string source, char lch, char rch, bool expectedMatch)
    {
        var lexer = CharacterClasses
            .Is(lch)
            .Then(CharacterClasses.Is(rch));

        var lexeme = lexer(source);
        Assert.Equal(expectedMatch, lexeme.Success);
    }
}
