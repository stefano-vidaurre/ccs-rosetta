using System.Data.Common;
using CCS.Rosetta.Api.Projects;
using FluentAssertions;

namespace CSS.Rosetta.Test.Projects;

public class ProjectRepositoryShould : IDisposable
{
    private readonly DbConnection _connection;
    private readonly IProjectRepository _repository;

    public ProjectRepositoryShould()
    {
        _connection = DataBaseConnection.CreateInMemoryConnection();
        _repository = new ProjectRepository(_connection);
    }

    public void Dispose()
    {
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task ReturnAEmptyList()
    {
        var result = await _repository.GetAll();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task InsertAndReturnANewProject()
    {
        var project = new Project(new Name("my-project"), "A description.");

        await _repository.Add(project);

        var result = await _repository.GetAll();
        result.Should().ContainEquivalentOf(project);
    }
}