using CCS.Rosetta.Api.Projects;
using FluentAssertions;
using NSubstitute;

namespace CSS.Rosetta.Test.Projects;

public class ProjectsControllerShould
{
    private readonly IProjectRepository _repository;
    private readonly ProjectsController _controller;

    public ProjectsControllerShould()
    {
        _repository = Substitute.For<IProjectRepository>();
        _controller = new ProjectsController(_repository);
    }

    [Fact]
    public async Task CreateANewProject()
    {
        var request = new ProjectCrateDto()
        {
            Name = "my-project",
            Description = "A description."
        };

        await _controller.Post(request);

        var expected = new Project("my-project", "A description.");
        
        await _repository.Received().Add(Arg.Is<Project>(project =>
            project.Name == expected.Name && project.Description == expected.Description));
    }

    [Fact]
    public async Task ReturnAllProjects()
    {
        _repository.GetAll().Returns(new List<Project>
        {
            new ("A name", "A description.")
        });
        
        var result = await _controller.Get();

        var expected = new List<ProjectReadDto>
        {
            new()
            {
                Name = "A name",
                Description = "A description."
            }
        };
        result.Should().BeEquivalentTo(expected);
    }
}