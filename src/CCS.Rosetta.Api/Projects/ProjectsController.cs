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
    public async Task Post(ProjectCrateDto request)
    {
        throw new NotImplementedException();
    }
}