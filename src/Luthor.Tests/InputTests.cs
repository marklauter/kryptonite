using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests;

[ExcludeFromCodeCoverage]
public sealed class InputTests
{
    [Fact]
    public void Ctor()
    {
        var value = "1234";
        var input = (Input)value;
        Assert.Equal(0, input.Offset);
        Assert.Equal(4, input.Length);
        Assert.Equal(value[0], input.Peek());
        Assert.Equal(value[1], input.PeekAhead(1));
        Assert.Equal(value[2], input.PeekAhead(2));
        Assert.Equal(value[3], input.PeekAhead(3));
        Assert.Equal(value, input);
    }

    [Fact]
    public void Advance()
    {
        var start = (Input)"1234";
        var next = start.Advance();
        Assert.Equal(1, next.Offset);
        Assert.Equal(3, next.Length);
        Assert.Equal('2', next.Peek());
        Assert.Equal("234", next);
    }
}
