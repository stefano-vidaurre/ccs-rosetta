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

    public ProjectRepositoryShould()
    {
        _connection = DataBaseConnection.CreateInMemoryConnection();
        _repository = new ProjectRepository(_connection);
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
        var project = new Project("my-project", "A description.");
        await _connection.ExecuteAsync($"INSERT INTO Projects ('Name', 'Description') VALUES ('{project.Name}', '{project.Description}');");
        
        var result = await _repository.GetAll();
        
        result.Should().ContainEquivalentOf(project);
    }

    [Fact]
    public async Task InsertAndReturnANewProject()
    {
        var project = new Project("my-project", "A description.");

        await _repository.Add(project);
        
        var result = await _repository.GetAll();
        result.Should().ContainEquivalentOf(project);
    }
}