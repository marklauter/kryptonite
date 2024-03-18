using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests;

[ExcludeFromCodeCoverage]
public sealed class SatisfyTests
{
    [Fact]
    public void Satisfy_Returns_Result_With_Value_When_Predicate_Succeeds()
    {
        var input = (Input)"1234";
        var result = Parser.Satisfy(ch => ch == '1')(input);
        Assert.True(result.HasValue);
        Assert.Equal('1', result.Value);
    }

    [Fact]
    public void Satisfy_Returns_Zero_When_EOF()
    {
        var input = (Input)"1234" + 4;

        var result = Parser.Satisfy(ch => ch == '1')(input);
        Assert.False(result.HasValue);
    }
}
