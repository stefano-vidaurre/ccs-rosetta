using System.Text;
using System.Text.Json;
using CCS.Rosetta.Api;
using CCS.Rosetta.Api.Projects;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CSS.Rosetta.Test.Projects;

[UsesVerify]
public class CreateProjectFeature : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public CreateProjectFeature(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task CreateProjectWithValidProperties()
    {
        var projectProperties = GivenASetOfValidProperties();

        await WhenCreateANewProject(projectProperties);

        await ThenRetrieveTheProjectsListWithTheNewProject();
    }

    private static ProjectCreateDto GivenASetOfValidProperties()
    {
        return new ProjectCreateDto()
        {
            Name = "my-project",
            Description = "A description."
        };
    }

    private async Task WhenCreateANewProject(ProjectCreateDto projectProperties)
    {
        var json = JsonSerializer.Serialize(projectProperties);
        var request = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/", request);
        response.EnsureSuccessStatusCode();
    }

    private async Task ThenRetrieveTheProjectsListWithTheNewProject()
    {
        var response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        await Verify(json);
    }
}