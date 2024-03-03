using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests;

[ExcludeFromCodeCoverage]
public sealed class ResultTests
{
    [Fact]
    public void Empty()
    {
        var input = (Input)"1234";
        var result = ParseResult.Zero<int>(input);
        Assert.False(result.HasValue);
        Assert.Equal(input, result.Match);
        Assert.Equal(input, result.Remainder);
    }

    [Fact]
    public void WithValue()
    {
        var input = (Input)"1234";
        var result = ParseResult.Value(1, input);
        Assert.True(result.HasValue);
        Assert.Equal(1, result.Value);
        Assert.Equal(input, result.Match);
        Assert.Equal(input, result.Remainder);
    }

    [Fact]
    public void WithValueAndRemainder()
    {
        var input = (Input)"1234";
        var remainder = input.Advance();
        var result = ParseResult.Value(1, input, remainder);
        Assert.True(result.HasValue);
        Assert.Equal(1, result.Value);
        Assert.Equal(input, result.Match);
        Assert.Equal(remainder, result.Remainder);
    }

    [Fact]
    public void New()
    {
        var input = (Input)"1234";
        var remainder = input.Advance();
        var result = ParseResult.New(1, input, remainder, true);
        Assert.True(result.HasValue);
        Assert.Equal(1, result.Value);
        Assert.Equal(input, result.Match);
        Assert.Equal(remainder, result.Remainder);
    }

    [Fact]
    public void CastEmpty()
    {
        var input = (Input)"1234";
        var result = ParseResult.Zero<int>(input);
        var cast = result.CastEmpty<int, char>();
        Assert.False(cast.HasValue);
        Assert.Equal(input, cast.Match);
        Assert.Equal(input, cast.Remainder);
    }
}
