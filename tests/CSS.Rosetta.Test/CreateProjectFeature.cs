using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CSS.Rosetta.Test;

public class CreateProjectFeature : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public CreateProjectFeature(WebApplicationFactory<Program> factory)
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
        var request = new StringContent(projectProperties, Encoding.UTF32, "application/json");
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