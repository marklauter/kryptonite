using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests;

[ExcludeFromCodeCoverage]
public sealed class SequenceTests
{
    [Fact]
    public void Zero_Sequence_Item_Returns_Zero()
    {
        var input = (Input)"1234";
        var parser = Parser.Zero<char>()
            .Sequence(Parser.Item());

        var result = parser(input);

        Assert.False(result.HasValue);
    }

    [Fact]
    public void Item_Sequence_Item_Consumes_Two()
    {
        var input = (Input)"1234";
        var parser =
            Parser.Item().Sequence(Parser.Item());

        var result = parser.Invoke(input);

        Assert.True(result.HasValue);
        Assert.Equal('1', result.Value.LeftValue);
        Assert.Equal('2', result.Value.RightValue);
        Assert.Equal(2, result.Length);
        Assert.Equal(0, result.Offset);
        Assert.Equal(2, result.Remainder.Length);
        Assert.Equal(2, result.Remainder.Offset);
    }
}
