using Microsoft.AspNetCore.Mvc;

namespace CCS.Rosetta.Api.Projects;

[ApiController]
[Route("/")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectRepository _repository;

    public ProjectsController(IProjectRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task Post(ProjectCreateDto request)
    {
        var project = new Project(new Name(request!.Name), request.Description);

        await _repository.Add(project);
    }

    [HttpGet]
    public async Task<IEnumerable<ProjectReadDto>> Get()
    {
        var projects = await _repository.GetAll();

        return projects.Select(project => new ProjectReadDto
            { Name = project.Name, Description = project.Description });
    }
}