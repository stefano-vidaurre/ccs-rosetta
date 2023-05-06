using System.Data.Common;
using Dapper;

namespace CCS.Rosetta.Api.Projects;

public class ProjectRepository : IProjectRepository
{
    private readonly DbConnection _connection;

    public ProjectRepository(DbConnection connection)
    {
        _connection = connection;
    }

    public Task Add(Project project)
    {
        return _connection.ExecuteAsync($"INSERT INTO Projects ('Name', 'Description') VALUES ('{project.Name}', '{project.Description}');");
    }

    public Task<IEnumerable<Project>> GetAll()
    {
        return _connection.QueryAsync<Project>("SELECT * FROM Projects;");
    }
}