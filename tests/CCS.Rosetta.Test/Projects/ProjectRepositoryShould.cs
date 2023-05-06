using System.Data.Common;
using CCS.Rosetta.Api.Projects;
using FluentAssertions;
using Microsoft.Data.Sqlite;

namespace CSS.Rosetta.Test.Projects;

public class ProjectRepositoryShould
{
    private readonly DbConnection _connection;
    private readonly IProjectRepository _repository;

    public ProjectRepositoryShould()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _repository = new ProjectRepository(_connection);
    }

    [Fact]
    public async Task ReturnAEmptyList()
    {
        var result = await _repository.GetAll();
        result.Should().BeEmpty();
    }
}