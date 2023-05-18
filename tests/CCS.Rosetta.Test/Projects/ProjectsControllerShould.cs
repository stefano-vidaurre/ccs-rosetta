using System.Data.Common;
using CCS.Rosetta.Api.Projects;
using FluentAssertions;

namespace CSS.Rosetta.Test.Projects;

public class ProjectsControllerShould : IDisposable
{
    private readonly ProjectsController _controller;
    private readonly IProjectRepository _repository;
    private readonly DbConnection _connection;

    public ProjectsControllerShould()
    {
        _connection = DataBaseConnection.CreateInMemoryConnection();
        _repository = new ProjectRepository(_connection);
        _controller = new ProjectsController(_repository);
    }

    public void Dispose()
    {
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task CreateANewProject()
    {
        ProjectCreateDto request = new()
        {
            Name = "my-project",
            Description = "A description."
        };

        await _controller.Post(request);

        Project expected = new(new Name("my-project"), "A description.");

        IEnumerable<Project> result = await _repository.GetAll();
        result.Should().ContainEquivalentOf(expected);
    }

    [Fact]
    public async Task ReturnAllProjects()
    {
        _repository.Add(new Project(new Name("A name"), "A description."));

        IEnumerable<ProjectReadDto> result = await _controller.Get();

        List<ProjectReadDto> expected = new()
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