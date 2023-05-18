using System.Text;
using System.Text.Json;
using CCS.Rosetta.Api.Projects;

namespace CSS.Rosetta.Test.Projects;

[UsesVerify]
public class CreateProjectFeature : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly TestWebApplicationFactory<Program> _factory;

    public CreateProjectFeature(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task CreateProjectWithValidProperties()
    {
        ProjectCreateDto projectProperties = GivenASetOfValidProperties();

        await WhenCreateANewProject(projectProperties);

        await ThenRetrieveTheProjectsListWithTheNewProject();
    }

    private static ProjectCreateDto GivenASetOfValidProperties()
    {
        return new ProjectCreateDto
        {
            Name = "my-project",
            Description = "A description."
        };
    }

    private async Task WhenCreateANewProject(ProjectCreateDto projectProperties)
    {
        string json = JsonSerializer.Serialize(projectProperties);
        StringContent request = new(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("/", request);
        response.EnsureSuccessStatusCode();
    }

    private async Task ThenRetrieveTheProjectsListWithTheNewProject()
    {
        HttpResponseMessage response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        await Verify(json);
    }
}