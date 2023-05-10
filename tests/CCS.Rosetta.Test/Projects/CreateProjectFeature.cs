using System.Text;
using System.Text.Json;
using CCS.Rosetta.Api;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CSS.Rosetta.Test.Projects;

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
        string projectProperties = GivenASetOfValidProperties();
        
        await WhenCreateANewProject(projectProperties);
        
        await ThenRetrieveTheProjectsListWithTheNewProject();
    }

    private string GivenASetOfValidProperties()
    {
        object properties = new
        {
            Name = "my-project",
            Description = "A description."
        };
        return JsonSerializer.Serialize(properties);
    }

    private async Task WhenCreateANewProject(string projectProperties)
    {
        var request = new StringContent(projectProperties, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/", request);
        response.EnsureSuccessStatusCode();
    }

    private async Task ThenRetrieveTheProjectsListWithTheNewProject()
    {
        var response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        Verify(json);
    }
}