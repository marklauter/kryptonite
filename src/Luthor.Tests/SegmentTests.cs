using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests;

[ExcludeFromCodeCoverage]
public sealed class SegmentTests
{
    [Fact]
    public void Ctor()
    {
        var value = "1234";
        var start = new Segment(value);
        Assert.Equal(0, start.Offset);
        Assert.Equal(4, start.Length);
        Assert.Equal(value[0], start.Peek());
        Assert.Equal(value, start);
    }

    [Fact]
    public void Advance()
    {
        var start = new Segment("1234");
        var next = start.Advance();
        Assert.Equal(1, next.Offset);
        Assert.Equal(3, next.Length);
        Assert.Equal('2', next.Peek());
        Assert.Equal("234", next);
    }
}
