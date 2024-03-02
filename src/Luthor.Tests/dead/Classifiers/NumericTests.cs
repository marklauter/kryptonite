//using Luthor.Classifiers;
//using System.Diagnostics.CodeAnalysis;

//namespace Luthor.Tests.dead.Classifiers;

//[ExcludeFromCodeCoverage]
//public sealed class NumericTests
//{
//    [Theory]
//    [InlineData("1", true)]
//    [InlineData("12", true)]
//    [InlineData("123456", true)]
//    [InlineData("+1", true)]
//    [InlineData("-1", true)]
//    [InlineData("+123", true)]
//    [InlineData("-123", true)]

//    [InlineData("1.0", true)]
//    [InlineData("12.0", true)]
//    [InlineData("123456.0", true)]
//    [InlineData("+1.0", true)]
//    [InlineData("-1.0", true)]
//    [InlineData("+123.0", true)]
//    [InlineData("-123.0", true)]

//    [InlineData("1.0E1", true)]
//    [InlineData("1.0e1", true)]
//    [InlineData("+1.0E1", true)]
//    [InlineData("-1.0e1", true)]
//    [InlineData("1.0E+1", true)]
//    [InlineData("1.0e-1", true)]

//    [InlineData("-", false)]
//    [InlineData("+", false)]
//    [InlineData("a", false)]
//    [InlineData(" ", false)]
//    public void IsInteger(string source, bool expectedSuccess)
//    {
//        var lexeme = Number.IsInteger(source);
//        Assert.Equal(expectedSuccess, lexeme.HasValue);
//        if (expectedSuccess)
//        {
//            if (source.Contains('.'))
//            {
//                source = source.Split('.')[0];
//            }

//            Assert.Equal(source, lexeme);
//        }
//    }

//    [Theory]
//    [InlineData("1", true)]
//    [InlineData("12", true)]
//    [InlineData("123456", true)]
//    [InlineData("+1", true)]
//    [InlineData("-1", true)]
//    [InlineData("+123", true)]
//    [InlineData("-123", true)]

//    [InlineData("1.0", true)]
//    [InlineData("12.0", true)]
//    [InlineData("123456.0", true)]
//    [InlineData("+1.0", true)]
//    [InlineData("-1.0", true)]
//    [InlineData("+123.0", true)]
//    [InlineData("-123.0", true)]

//    [InlineData("-", false)]
//    [InlineData("+", false)]
//    [InlineData("a", false)]
//    [InlineData(" ", false)]
//    public void IsIntegerFast(string source, bool expectedSuccess)
//    {
//        var lexeme = Number.IsIntegerFast(source);
//        Assert.Equal(expectedSuccess, lexeme.HasValue);
//        if (expectedSuccess)
//        {
//            if (source.Contains('.'))
//            {
//                source = source.Split('.')[0];
//            }

//            Assert.Equal(source, lexeme);
//        }
//    }

//    [Theory]
//    [InlineData("1", false)]
//    [InlineData("12", false)]
//    [InlineData("123456", false)]
//    [InlineData("+1", false)]
//    [InlineData("-1", false)]
//    [InlineData("+123", false)]
//    [InlineData("-123", false)]

//    [InlineData("1.0", true)]
//    [InlineData("12.0", true)]
//    [InlineData("123456.0", true)]
//    [InlineData("+1.0", true)]
//    [InlineData("-1.0", true)]
//    [InlineData("+123.0", true)]
//    [InlineData("-123.0", true)]

//    [InlineData("1.0E1", true)]
//    [InlineData("1.0e1", true)]
//    [InlineData("+1.0E1", true)]
//    [InlineData("-1.0e1", true)]
//    [InlineData("1.0E+1", true)]
//    [InlineData("1.0e-1", true)]

//    [InlineData("-", false)]
//    [InlineData("+", false)]
//    [InlineData("a", false)]
//    [InlineData(" ", false)]
//    public void IsFloatingPoint(string source, bool expectedSuccess)
//    {
//        var lexeme = Number.IsFloatingPoint(source);
//        Assert.Equal(expectedSuccess, lexeme.HasValue);
//        if (lexeme.HasValue)
//        {
//            source = source.Contains('E')
//                ? source.Split('E')[0]
//                : source.Contains('e')
//                    ? source.Split('e')[0]
//                    : source;

//            Assert.Equal(source, lexeme);
//        }
//    }

//    [Theory]
//    [InlineData("1", false)]
//    [InlineData("12", false)]
//    [InlineData("123456", false)]
//    [InlineData("+1", false)]
//    [InlineData("-1", false)]
//    [InlineData("+123", false)]
//    [InlineData("-123", false)]

//    [InlineData("1.0", false)]
//    [InlineData("12.0", false)]
//    [InlineData("123456.0", false)]
//    [InlineData("+1.0", false)]
//    [InlineData("-1.0", false)]
//    [InlineData("+123.0", false)]
//    [InlineData("-123.0", false)]

//    [InlineData("1E1", false)]

//    [InlineData("1.0E1", true)]
//    [InlineData("1.0e1", true)]
//    [InlineData("+1.0E1", true)]
//    [InlineData("-1.0e1", true)]
//    [InlineData("1.0E+1", true)]
//    [InlineData("1.0e-1", true)]

//    [InlineData("-", false)]
//    [InlineData("+", false)]
//    [InlineData("a", false)]
//    [InlineData(" ", false)]
//    public void IsScientificNotation(string source, bool expectedSuccess)
//    {
//        var lexeme = Number.IsScientificNotation(source);
//        Assert.Equal(expectedSuccess, lexeme.HasValue);
//        if (lexeme.HasValue)
//        {
//            Assert.Equal(source, lexeme);
//        }
//    }

//    [Theory]
//    [InlineData("1", false)]
//    [InlineData("12", false)]
//    [InlineData("123456", false)]
//    [InlineData("+1", false)]
//    [InlineData("-1", false)]
//    [InlineData("+123", false)]
//    [InlineData("-123", false)]

//    [InlineData("1.0", false)]
//    [InlineData("12.0", false)]
//    [InlineData("123456.0", false)]
//    [InlineData("+1.0", false)]
//    [InlineData("-1.0", false)]
//    [InlineData("+123.0", false)]
//    [InlineData("-123.0", false)]

//    [InlineData("1E1", false)]

//    [InlineData("1.0E1", false)]
//    [InlineData("1.0e1", false)]
//    [InlineData("+1.0E1", false)]
//    [InlineData("-1.0e1", false)]
//    [InlineData("1.0E+1", false)]
//    [InlineData("1.0e-1", false)]

//    [InlineData("-", false)]
//    [InlineData("+", false)]
//    [InlineData("a", false)]
//    [InlineData(" ", false)]

//    [InlineData("0x00", true)]
//    [InlineData("0x01", true)]
//    [InlineData("1x00", false)]
//    [InlineData("0xFF", true)]
//    [InlineData("0xff", true)]
//    [InlineData("0xf", true)]
//    [InlineData("0xF", true)]
//    [InlineData("0xfFfffFFaaff", true)]
//    public void IsHexNotation(string source, bool expectedSuccess)
//    {
//        var lexeme = Number.IsHexNotation(source);
//        Assert.Equal(expectedSuccess, lexeme.HasValue);
//        if (lexeme.HasValue)
//        {
//            Assert.Equal(source, lexeme);
//        }
//    }
//}
