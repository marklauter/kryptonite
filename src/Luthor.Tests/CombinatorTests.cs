//using System.Diagnostics.CodeAnalysis;


//namespace Luthor.Tests;

//[ExcludeFromCodeCoverage]
//public sealed class CombinatorTests
//{
//    [Theory]
//    [InlineData("1", '1', '2', true)]
//    [InlineData("2", '1', '2', true)]
//    [InlineData("a", '1', '2', false)]
//    public void Or(string value, char lch, char rch, bool expectedMatch)
//    {
//        var segment = new Segment(value);
//        var patternMatcher = Character
//            .IsEqual(lch)
//            .Or(Character.IsEqual(rch));

//        var match = patternMatcher(segment);
//        Assert.Equal(expectedMatch, match.Success);
//    }

//    [Theory]
//    [InlineData("11", '1', '2', false)]
//    [InlineData("22", '1', '2', false)]
//    [InlineData("11", '1', '1', true)]
//    public void Then(string value, char lch, char rch, bool expectedMatch)
//    {
//        var segment = new Segment(value);
//        var patternMatcher = Character
//            .IsEqual(lch)
//            .Then(Character.IsEqual(rch));

//        var match = patternMatcher(segment);
//        Assert.Equal(expectedMatch, match.Success);
//    }
//}
