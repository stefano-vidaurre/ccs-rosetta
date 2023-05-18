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

    [Fact]
    public void NotBeConstructedWhenValueLengthIsLessThanFour()
    {
        Func<Name> action = () => new Name("aws");
        
        action.Should().Throw<FormatException>();
    }

    [Fact]
    public void NotBeConstructedWhenValueStartsWithWhiteSpace()
    {
        Func<Name> action = () => new Name(" aws");
        
        action.Should().Throw<FormatException>();
    }
}