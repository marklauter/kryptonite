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
    public void Or(string source, char leftChar, char rightChar, bool expectedMatch)
    {
        var lexer = Character
            .Is(leftChar)
            .Or(Character.Is(rightChar));

        var lexeme = lexer(source);
        Assert.Equal(expectedMatch, lexeme.Matched);
    }

    [Theory]
    [InlineData("1", '1', '2', true)]
    [InlineData("1", '1', '1', false)]
    [InlineData("1", '2', '2', false)]
    [InlineData("1", '2', '1', false)]
    [InlineData("a", 'x', 'x', false)]
    public void And(string source, char leftChar, char rightChar, bool expectedMatch)
    {
        var lexer = Character
            .Is(leftChar)
            .And(Character.IsNot(rightChar));

        var lexeme = lexer(source);
        Assert.Equal(expectedMatch, lexeme.Matched);
    }

    [Theory]
    [InlineData("11", '1', '2', false)]
    [InlineData("22", '1', '2', false)]
    [InlineData("11", '1', '1', true)]
    [InlineData("12", '1', '2', true)]
    public void Then(string source, char leftChar, char rightChar, bool expectedMatch)
    {
        var lexer = Character
            .Is(leftChar)
            .Then(Character.Is(rightChar));

        var lexeme = lexer(source);
        Assert.Equal(expectedMatch, lexeme.Matched);
        if (expectedMatch)
        {
            Assert.Equal(source, lexeme);
        }
    }
}
