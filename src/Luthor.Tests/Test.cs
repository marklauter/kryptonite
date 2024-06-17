using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests;

public class A
{
    public void F(int x) => Console.WriteLine($"int {x}");
}

public class B : A
{
    public void F(double x) => Console.WriteLine($"double {x}");
}

public static class StringExt
{
    public static string Fix(this string s) => s.Trim();

    public static string[] Read(this string[] items, string expected) =>
        items
            .Select(i => (I: i, Ex: expected.Fix()))
            .Where(t => t.I == t.Ex)
            .Select(t => t.I)
        .ToArray();
}

[ExcludeFromCodeCoverage]
public sealed class Test
{
    [Fact]
    public void Test1() => new B().F(1);

    [Fact]
    public void Test2()
    {
        var items = new string[] { "a", "b", "c" };
        var expected = "c";
        var result = items.Read(expected);
        var actual = Assert.Single(result);
        Assert.Equal(expected, actual);
    }
}
