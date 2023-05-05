﻿using CCS.Rosetta.Api.Projects;
using NSubstitute;

namespace CSS.Rosetta.Test;

public class ProjectsControllerShould
{
    private readonly IProjectRepository _repository;
    private readonly ProjectsController _controller;

    public ProjectsControllerShould()
    {
        _repository = Substitute.For<IProjectRepository>();
        _controller = new ProjectsController(_repository);
    }

    [Fact]
    public async Task CreateANewProject()
    {
        var request = new ProjectCrateDto()
        {
            Name = "my-project",
            Description = "A description."
        };

        await _controller.Post(request);

        var project = new Project("my-project", "A description.");
        await _repository.Received().Add(project);
    }
}