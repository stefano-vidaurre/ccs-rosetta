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
}