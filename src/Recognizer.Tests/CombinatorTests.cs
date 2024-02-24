namespace Recognizer.Tests;

public class CombinatorTests
{
    [Theory]
    [InlineData("1", true)]
    [InlineData("2", true)]
    [InlineData("a", false)]
    [InlineData("b", false)]
    public void IsDigit(string value, bool expectedMatch)
    {
        var segment = new Segment(value);
        var match = Character.IsDigit(segment);
        Assert.Equal(expectedMatch, match.Success);
    }

    [Theory]
    [InlineData("1", false)]
    [InlineData("2", false)]
    [InlineData("a", true)]
    [InlineData("b", true)]
    public void IsLetter(string value, bool expectedMatch)
    {
        var segment = new Segment(value);
        var match = Character.IsLetter(segment);
        Assert.Equal(expectedMatch, match.Success);
    }

    [Theory]
    [InlineData("1", '1', true)]
    [InlineData("2", '1', false)]
    [InlineData("a", 'a', true)]
    [InlineData("b", 'a', false)]
    public void IsEqual(string value, char ch, bool expectedMatch)
    {
        var segment = new Segment(value);
        var match = Character.IsEqual(ch)(segment);
        Assert.Equal(expectedMatch, match.Success);
    }

    [Theory]
    [InlineData("1", '1', '2', true)]
    [InlineData("2", '1', '2', true)]
    [InlineData("a", '1', '2', false)]
    public void Or(string value, char lch, char rch, bool expectedMatch)
    {
        var segment = new Segment(value);
        var patternMatcher = Character
            .IsEqual(lch)
            .Or(Character.IsEqual(rch));

        var match = patternMatcher(segment);
        Assert.Equal(expectedMatch, match.Success);
    }

    [Theory]
    [InlineData("11", '1', '2', false)]
    [InlineData("22", '1', '2', false)]
    [InlineData("11", '1', '1', true)]
    public void Then(string value, char lch, char rch, bool expectedMatch)
    {
        var segment = new Segment(value);
        var patternMatcher = Character
            .IsEqual(lch)
            .Then(Character.IsEqual(rch));

        var match = patternMatcher(segment);
        Assert.Equal(expectedMatch, match.Success);
    }
}

public readonly ref struct Symbol(
    int offset,
    int length,
    int token)
{
    public const int NoMatch = -1;
    public readonly int Offset = offset;
    public readonly int Length = length;
    public readonly int Token = token;
}

public readonly ref struct MatchResult(
    Segment segment,
    Symbol symbol,
    bool success)
{
    public readonly Segment Segment = segment;
    public readonly Symbol Symbol = symbol;
    public readonly bool Success = success;

    public static MatchResult NoMatch(Segment segment)
    {
        return new(
            segment,
            new(segment.Offset, 0, Symbol.NoMatch),
            false);
    }
};

public readonly ref struct Popped(
    Segment source,
    char value)
{
    public readonly Segment Source = source;
    public readonly char Value = value;
}

public readonly ref struct Segment(
    ReadOnlySpan<char> value,
    int offset)
{
    public readonly ReadOnlySpan<char> Value = value;
    public readonly int Offset = offset;

    public Segment(string Segment)
        : this(Segment, 0)
    {
    }

    public int Length => Value.Length;
    public bool IsEnd => Offset >= Value.Length;

    public ReadOnlySpan<char> ReadSymbol(ref readonly Symbol symbol)
    {
        return symbol.Offset >= Value.Length
            ? "EOF"
            : Value[symbol.Offset..(symbol.Offset + symbol.Length)];
    }

    public Segment Advance(int length)
    {
        return new Segment(Value, Offset + length);
    }

    public char Peek()
    {
        return IsEnd
            ? '\0'
            : Value[Offset];
    }

    public Popped Pop()
    {
        return IsEnd
            ? new(this, '\0')
            : new(Advance(1), Peek());
    }
}

public static class Character
{
    public static Recognizer IsDigit => segment =>
    {
        var popped = segment.Pop();
        return Char.IsDigit(popped.Value)
            ? new(
                popped.Source,
                new(segment.Offset, 1, 1),
                true)
            : MatchResult.NoMatch(segment);
    };

    public static Recognizer IsLetter => segment =>
    {
        var popped = segment.Pop();
        return Char.IsLetter(popped.Value)
            ? new(
                popped.Source,
                new(segment.Offset, 1, 1),
                true)
            : MatchResult.NoMatch(segment);
    };

    public static Recognizer IsEqual(char c)
    {
        return IsEqual(c, false);
    }

    public static Recognizer IsEqual(char c, bool ignoreCase)
    {
        return segment =>
        {
            var popped = segment.Pop();
            return popped.Value == c ||
                ignoreCase && Char.ToUpperInvariant(popped.Value) == Char.ToUpperInvariant(c)
                ? new(
                    popped.Source,
                    new(segment.Offset, 1, 1),
                    true)
                : MatchResult.NoMatch(segment);
        };
    }
}

public delegate MatchResult Recognizer(Segment segment);

public static class Combinators
{
    public static Recognizer Or(this Recognizer left, Recognizer right)
    {
        return segment =>
        {
            var match = left(segment);
            return match.Success
                ? match
                : right(segment);
        };
    }
    public static Recognizer Then(this Recognizer first, Recognizer second)
    {
        return segment =>
        {
            var match = first(segment);
            return match.Success
                ? second(match.Segment)
                : match;
        };
    }
}
