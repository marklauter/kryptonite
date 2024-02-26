using Luthor.Classifiers;
using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests.Classifiers;

[ExcludeFromCodeCoverage]
public sealed class NumericsTests
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

    [InlineData("1.0E1", true)]
    [InlineData("1.0e1", true)]
    [InlineData("+1.0E1", true)]
    [InlineData("-1.0e1", true)]
    [InlineData("1.0E+1", true)]
    [InlineData("1.0e-1", true)]

    [InlineData("-", false)]
    [InlineData("+", false)]
    [InlineData("a", false)]
    [InlineData(" ", false)]
    public void MatchInteger(string source, bool expectedSuccess)
    {
        var lexeme = Numerics.MatchInteger(source);
        Assert.Equal(expectedSuccess, lexeme.Success);
        if (expectedSuccess)
        {
            if (source.Contains('.'))
            {
                source = source.Split('.')[0];
            }

            Assert.Equal(source, lexeme);
        }
    }

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
    public void MatchIntegerFast(string source, bool expectedSuccess)
    {
        var lexeme = Numerics.MatchIntegerFast(source);
        Assert.Equal(expectedSuccess, lexeme.Success);
        if (expectedSuccess)
        {
            if (source.Contains('.'))
            {
                source = source.Split('.')[0];
            }

            Assert.Equal(source, lexeme);
        }
    }

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

    [InlineData("1.0E1", true)]
    [InlineData("1.0e1", true)]
    [InlineData("+1.0E1", true)]
    [InlineData("-1.0e1", true)]
    [InlineData("1.0E+1", true)]
    [InlineData("1.0e-1", true)]

    [InlineData("-", false)]
    [InlineData("+", false)]
    [InlineData("a", false)]
    [InlineData(" ", false)]
    public void MatchInteger3(string source, bool expectedSuccess)
    {
        var lexeme = Numerics.MatchInteger3(source);
        Assert.Equal(expectedSuccess, lexeme.Success);
        if (expectedSuccess)
        {
            if (source.Contains('.'))
            {
                source = source.Split('.')[0];
            }

            Assert.Equal(source, lexeme);
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

    [InlineData("1.0E1", true)]
    [InlineData("1.0e1", true)]
    [InlineData("+1.0E1", true)]
    [InlineData("-1.0e1", true)]
    [InlineData("1.0E+1", true)]
    [InlineData("1.0e-1", true)]

    [InlineData("-", false)]
    [InlineData("+", false)]
    [InlineData("a", false)]
    [InlineData(" ", false)]
    public void MatchFloatingPoint(string source, bool expectedSuccess)
    {
        var lexeme = Numerics.MatchFloatingPoint(source);
        Assert.Equal(expectedSuccess, lexeme.Success);
        if (lexeme.Success)
        {
            source = source.Contains('E')
                ? source.Split('E')[0]
                : source.Contains('e')
                    ? source.Split('e')[0]
                    : source;

            Assert.Equal(source, lexeme);
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

    [InlineData("1.0", false)]
    [InlineData("12.0", false)]
    [InlineData("123456.0", false)]
    [InlineData("+1.0", false)]
    [InlineData("-1.0", false)]
    [InlineData("+123.0", false)]
    [InlineData("-123.0", false)]

    [InlineData("1E1", false)]

    [InlineData("1.0E1", true)]
    [InlineData("1.0e1", true)]
    [InlineData("+1.0E1", true)]
    [InlineData("-1.0e1", true)]
    [InlineData("1.0E+1", true)]
    [InlineData("1.0e-1", true)]

    [InlineData("-", false)]
    [InlineData("+", false)]
    [InlineData("a", false)]
    [InlineData(" ", false)]
    public void MatchScientificNotation(string source, bool expectedSuccess)
    {
        var lexeme = Numerics.MatchScientificNotation(source);
        Assert.Equal(expectedSuccess, lexeme.Success);
        if (lexeme.Success)
        {
            Assert.Equal(source, lexeme);
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

    [InlineData("1.0", false)]
    [InlineData("12.0", false)]
    [InlineData("123456.0", false)]
    [InlineData("+1.0", false)]
    [InlineData("-1.0", false)]
    [InlineData("+123.0", false)]
    [InlineData("-123.0", false)]

    [InlineData("1E1", false)]

    [InlineData("1.0E1", false)]
    [InlineData("1.0e1", false)]
    [InlineData("+1.0E1", false)]
    [InlineData("-1.0e1", false)]
    [InlineData("1.0E+1", false)]
    [InlineData("1.0e-1", false)]

    [InlineData("-", false)]
    [InlineData("+", false)]
    [InlineData("a", false)]
    [InlineData(" ", false)]

    [InlineData("0x00", true)]
    [InlineData("0x01", true)]
    [InlineData("1x00", false)]
    [InlineData("0xFF", true)]
    [InlineData("0xff", true)]
    [InlineData("0xf", true)]
    [InlineData("0xF", true)]
    [InlineData("0xfFfffFFaaff", true)]
    public void MatchHexNotation(string source, bool expectedSuccess)
    {
        var lexeme = Numerics.MatchHexNotation(source);
        Assert.Equal(expectedSuccess, lexeme.Success);
        if (lexeme.Success)
        {
            Assert.Equal(source, lexeme);
        }
    }
}
