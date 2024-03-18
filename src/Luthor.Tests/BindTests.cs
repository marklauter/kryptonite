using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests;

[ExcludeFromCodeCoverage]
public sealed class BindTests
{
    [Fact]
    public void Zero_Bind_F_Equals_Zero()
    {
        var input = (Input)"1234";
        var parser = Parser.Zero<char>()
            .Bind(ch => Parser.Item());

        var result = parser(input);
        Assert.False(result.HasValue);
    }

    [Fact]
    public void Item_Bind_Item_Consumes_Two()
    {
        var input = (Input)"1234";
        var parser = Parser.Item()
            .Bind<char, string>(ch => input =>
            {
                var result = Parser.Item()(input);
                return result.HasValue
                    ? ParseResult.Value($"{ch}{result.Value}", input, result.Remainder)
                    : ParseResult.Zero<string>(input);
            });
        var result = parser(input);
        Assert.True(result.HasValue);
        Assert.Equal("12", result.Value);
        Assert.Equal(2, result.Length);
        Assert.Equal(0, result.Offset);
        Assert.Equal(2, result.Remainder.Length);
        Assert.Equal(2, result.Remainder.Position);
    }

    [Fact]
    public void Item_Bind_Zero_Returns_Zero()
    {
        var input = (Input)"1234";
        var parser = Parser.Item()
            .Bind(ch => Parser.Zero<string>());
        var result = parser(input);
        Assert.False(result.HasValue);
        // item consumes, event if bind returns zero
        Assert.Equal(3, result.Remainder.Length);
        Assert.Equal(1, result.Remainder.Position);
    }
}
