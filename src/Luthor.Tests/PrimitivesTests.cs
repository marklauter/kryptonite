using Luthor.Classifiers;
using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests;

[ExcludeFromCodeCoverage]
public sealed class PrimitivesTests
{
    [Theory]
    [InlineData("xyz")]
    [InlineData("")]
    public void ResultV(string source)
    {
        var input = (Input)source;
        var result = Primitives.Result('1')(input);
        Assert.True(result.HasValue);
        Assert.Equal('1', result.Value);
        Assert.Equal(input, result.Match);
        Assert.Equal(input, result.Remainder);
    }

    [Theory]
    [InlineData("xyz")]
    [InlineData("")]
    public void ZeroT(string source)
    {
        var input = (Input)source;
        var result = Primitives.Zero<char>()(input);
        Assert.False(result.HasValue);
        Assert.Equal(input, result.Match);
        Assert.Equal(input, result.Remainder);
    }

    [Theory]
    [InlineData("xyz", true)]
    [InlineData("", false)]
    public void Item(string source, bool expectedHasValue)
    {
        var input = (Input)source;
        var result = Primitives.Item(input);
        Assert.Equal(expectedHasValue, result.HasValue);
        Assert.Equal(input, result.Match);
        var expectedRemainder = expectedHasValue
            ? input.Advance()
            : input;
        Assert.Equal(expectedRemainder, result.Remainder);
    }
}
