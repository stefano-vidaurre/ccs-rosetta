namespace CCS.Rosetta.Api.Projects;

public class Project
{
    private readonly Name _name;

    public Project(Name name, string? description)
    {
        _name = name;
        Description = description;
    }

    public string Name => _name.Value;
    public string? Description { get; }
}