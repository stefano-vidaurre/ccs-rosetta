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

        await _repository.Received().Add(Arg.Is<Project>(project =>
            project.Name == "my-project" && project.Description == "A description."));
    }

    [Fact]
    public void ReturnAllProjects()
    {
        _repository.GetAll().Returns(new List<Project>
        {
            new ("A name", "A description.")
        });
        
        IEnumerable<ProjectReadDto> result = _controller.Get();

        IEnumerable<ProjectReadDto> expected = new List<ProjectReadDto>
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