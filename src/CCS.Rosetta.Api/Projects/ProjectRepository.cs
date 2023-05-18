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
        return _connection.ExecuteAsync(
            $"INSERT INTO Projects ('Name', 'Description') VALUES ('{project.Name.Value}', '{project.Description}');");
    }

    public async Task<IEnumerable<Project>> GetAll()
    {
        IEnumerable<dynamic> results = await _connection.QueryAsync<dynamic>("SELECT * FROM Projects;");
        return results.Select(result => new Project(new Name(result.Name), result.Description));
    }
}