using Luthor.Lexers;

namespace Luthor.Tests;

public class NumericClassesTests
{
    [Theory]
    [InlineData("1", true)]
    [InlineData("12", true)]
    [InlineData("123456", true)]
    [InlineData("+1", true)]
    [InlineData("-1", true)]
    [InlineData("+123", true)]
    [InlineData("-123", true)]
    [InlineData("-", false)]
    [InlineData("+", false)]
    [InlineData("a", false)]
    [InlineData(" ", false)]
    public void MatchInteger(string input, bool expectedSuccess)
    {
        var segment = new Segment(input);
        var lexeme = NumericClasses.MatchInteger(segment);
        Assert.Equal(expectedSuccess, lexeme.Success);
        if (lexeme.Success)
        {
            Assert.Equal(input, lexeme.Match);
        }
    }
}
