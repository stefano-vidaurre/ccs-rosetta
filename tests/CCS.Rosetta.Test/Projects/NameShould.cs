using CCS.Rosetta.Api.Projects;
using FluentAssertions;

namespace CSS.Rosetta.Test.Projects;

public class NameShould
{
    [Fact]
    public void NotBeConstructedWhenValueIsNull()
    {
        Func<Name> action = () => new Name(null);
        
        action.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void NotBeConstructedWhenValueIsEmpty()
    {
        Func<Name> action = () => new Name(string.Empty);
        
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("a")]
    [InlineData("aw")]
    [InlineData("aws")]
    public void NotBeConstructedWhenValueLengthIsLessThanFour(string value)
    {
        Func<Name> action = () => new Name(value);
        
        action.Should().Throw<FormatException>();
    }

    [Theory]
    [InlineData(" aws")]
    [InlineData("a ws")]
    [InlineData("aws ")]
    [InlineData("a w s")]
    public void NotBeConstructedWhenValueContainsWhiteSpaces(string value)
    {
        Func<Name> action = () => new Name(value);
        
        action.Should().Throw<FormatException>();
    }

    [Theory]
    [InlineData("aws*")]
    [InlineData("aws$")]
    [InlineData("aws.")]
    [InlineData("aws@")]
    public void NotBeConstructedWhenValueContainsSymbols(string value)
    {
        Func<Name> action = () => new Name(value);
        
        action.Should().Throw<FormatException>();
    }

    [Fact]
    public void BeConstructedWhenValueContainsBarSymbol()
    {
        Func<Name> action = () => new Name("aws-");

        action.Should().NotThrow();
    }
}