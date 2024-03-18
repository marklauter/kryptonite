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

[ExcludeFromCodeCoverage]
public sealed class Test
{
    [Fact]
    public void Test1() => new B().F(1);
}
