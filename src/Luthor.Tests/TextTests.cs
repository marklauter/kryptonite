using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests;

[ExcludeFromCodeCoverage]
public sealed class TextTests
{
    [Fact]
    public void Word_EmptyString_ReturnsEmptyString()
    {
        var input = (Input)String.Empty;
        var result = Text.Word()(input);
        Assert.False(result.HasValue);
    }

    [Fact]
    public void Word_SingleLetter_ReturnsLetter()
    {
        var input = (Input)"a";
        var result = Text.Word()(input);
        Assert.True(result.HasValue);
        Assert.Equal("a", result.Value);
    }

    [Fact]
    public void Word_TwoLetters_ReturnsTwoLetters()
    {
        var input = (Input)"ab";
        var result = Text.Word()(input);
        Assert.True(result.HasValue);
        Assert.Equal("ab", result.Value);
    }

    [Fact]
    public void Word_TwoLetters_Returns_Only_TwoLetters()
    {
        var input = (Input)"ab c";
        var result = Text.Word()(input);
        Assert.True(result.HasValue);
        Assert.Equal("ab", result.Value);
    }
}
