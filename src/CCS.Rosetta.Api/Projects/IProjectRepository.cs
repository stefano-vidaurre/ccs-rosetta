namespace CCS.Rosetta.Api.Projects;

public interface IProjectRepository
{
    Task Add(Project project);
    Task<IEnumerable<Project>> GetAll();
}