using System.Data.Common;
using CCS.Rosetta.Api.Projects;
using Dapper;
using FluentAssertions;
using Microsoft.Data.Sqlite;

namespace CSS.Rosetta.Test.Projects;

public class ProjectRepositoryShould : IDisposable
{
    private readonly DbConnection _connection;
    private readonly IProjectRepository _repository;
    private readonly Project _project;

    public ProjectRepositoryShould()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _repository = new ProjectRepository(_connection);
        _connection.Open();
        _connection.Execute("CREATE TABLE IF NOT EXISTS Projects (Name VARCHAR(255) PRIMARY KEY, Description TEXT);");
        _project = new Project("my-project", "A description.");
    }
    
    public void Dispose()
    {
        _connection.Dispose();
    }

    [Fact]
    public async Task ReturnAEmptyList()
    {
        var result = await _repository.GetAll();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task ReturnAListWithInsertedProject()
    {
        await _connection.ExecuteAsync($"INSERT INTO Projects ('Name', 'Description') VALUES ('{_project.Name}', '{_project.Description}');");
        
        var result = await _repository.GetAll();
        
        result.Should().Contain(p => p.IsSame(_project));
    }

    [Fact]
    public async Task InsertAndReturnANewProject()
    {
        await _repository.Add(_project);
        
        var result = await _repository.GetAll();
        result.Should().Contain(p => p.IsSame(_project));
    }
}