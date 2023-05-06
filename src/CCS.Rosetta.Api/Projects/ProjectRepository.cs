using System.Data.Common;

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
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Project>> GetAll()
    {
        return Task.FromResult(Enumerable.Empty<Project>());
    }
}