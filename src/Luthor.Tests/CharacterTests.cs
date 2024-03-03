using System.Diagnostics.CodeAnalysis;

namespace Luthor.Tests;

[ExcludeFromCodeCoverage]
public sealed class CharacterTests
{
    [Theory]
    [InlineData("abc", 'a', true)]
    [InlineData("abc", 'x', false)]
    [InlineData("", 'x', false)]
    public void Char_Asserts_Equality_Of_Input_To_ExpectedChar(string source, char expectedChar, bool hasValue)
    {
        var input = source;
        var result = Character.Char(expectedChar)(input);

        Assert.Equal(hasValue, result.HasValue);
        if (hasValue)
        {
            Assert.Equal(expectedChar, result.Value);
            Assert.Equal(expectedChar.ToString(), (string)result);
            Assert.Equal(source[1..], result.Remainder.ToString());
        }
    }

    [Theory]
    [InlineData("123", '1', true)]
    [InlineData("abc", Char.MinValue, false)]
    [InlineData("", Char.MinValue, false)]
    public void Digit_Asserts_Input_Char_Is_Digit(string source, char expectedChar, bool hasValue)
    {
        var input = source;
        var result = Character.Digit()(input);

        Assert.Equal(hasValue, result.HasValue);
        if (hasValue)
        {
            Assert.Equal(expectedChar, result.Value);
            Assert.Equal(expectedChar.ToString(), (string)result);
            Assert.Equal(source[1..], result.Remainder.ToString());
        }
    }

    [Theory]
    [InlineData("abc", 'a', true)]
    [InlineData("123", Char.MinValue, false)]
    [InlineData("ABC", Char.MinValue, false)]
    [InlineData("", Char.MinValue, false)]
    public void Lower_Asserts_Input_Char_Is_Lower(string source, char expectedChar, bool hasValue)
    {
        var input = source;
        var result = Character.Lower()(input);

        Assert.Equal(hasValue, result.HasValue);
        if (hasValue)
        {
            Assert.Equal(expectedChar, result.Value);
            Assert.Equal(expectedChar.ToString(), (string)result);
            Assert.Equal(source[1..], result.Remainder.ToString());
        }
    }

    [Theory]
    [InlineData("ABC", 'A', true)]
    [InlineData("123", Char.MinValue, false)]
    [InlineData("abc", Char.MinValue, false)]
    [InlineData("", Char.MinValue, false)]
    public void Upper_Asserts_Input_Char_Is_Upper(string source, char expectedChar, bool hasValue)
    {
        var input = source;
        var result = Character.Upper()(input);

        Assert.Equal(hasValue, result.HasValue);
        if (hasValue)
        {
            Assert.Equal(expectedChar, result.Value);
            Assert.Equal(expectedChar.ToString(), (string)result);
            Assert.Equal(source[1..], result.Remainder.ToString());
        }
    }

    [Theory]
    [InlineData("ABC", 'A', true)]
    [InlineData("123", '1', true)]
    [InlineData("abc", 'a', true)]
    [InlineData("", Char.MinValue, false)]
    public void Any_Consumes_Any_Char(string source, char expectedChar, bool hasValue)
    {
        var input = source;
        var result = Character.Any()(input);

        Assert.Equal(hasValue, result.HasValue);
        if (hasValue)
        {
            Assert.Equal(expectedChar, result.Value);
            Assert.Equal(expectedChar.ToString(), (string)result);
            Assert.Equal(source[1..], result.Remainder.ToString());
        }
    }
}
