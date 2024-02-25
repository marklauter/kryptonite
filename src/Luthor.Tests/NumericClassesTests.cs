using Luthor.Lexers;
using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests;

[ExcludeFromCodeCoverage]
public sealed class NumericClassesTests
{
    [Theory]
    [InlineData("1", true)]
    [InlineData("12", true)]
    [InlineData("123456", true)]
    [InlineData("+1", true)]
    [InlineData("-1", true)]
    [InlineData("+123", true)]
    [InlineData("-123", true)]

    [InlineData("1.0", true)]
    [InlineData("12.0", true)]
    [InlineData("123456.0", true)]
    [InlineData("+1.0", true)]
    [InlineData("-1.0", true)]
    [InlineData("+123.0", true)]
    [InlineData("-123.0", true)]

    [InlineData("-", false)]
    [InlineData("+", false)]
    [InlineData("a", false)]
    [InlineData(" ", false)]
    public void MatchInteger(string input, bool expectedSuccess)
    {
        var segment = new Segment(input);
        var lexeme = NumericClasses.MatchInteger(segment);
        Assert.Equal(expectedSuccess, lexeme.Success);
        if (expectedSuccess)
        {
            if (input.Contains('.'))
            {
                input = input.Split('.')[0];
            }

            Assert.Equal(input, lexeme);
        }
    }

    [Theory]
    [InlineData("1", false)]
    [InlineData("12", false)]
    [InlineData("123456", false)]
    [InlineData("+1", false)]
    [InlineData("-1", false)]
    [InlineData("+123", false)]
    [InlineData("-123", false)]

    [InlineData("1.0", true)]
    [InlineData("12.0", true)]
    [InlineData("123456.0", true)]
    [InlineData("+1.0", true)]
    [InlineData("-1.0", true)]
    [InlineData("+123.0", true)]
    [InlineData("-123.0", true)]

    [InlineData("-", false)]
    [InlineData("+", false)]
    [InlineData("a", false)]
    [InlineData(" ", false)]
    public void MatchFloatingPoint(string input, bool expectedSuccess)
    {
        var segment = new Segment(input);
        var lexeme = NumericClasses.MatchFloatingPoint(segment);
        Assert.Equal(expectedSuccess, lexeme.Success);
        if (lexeme.Success)
        {
            Assert.Equal(input, lexeme);
        }
    }
}
