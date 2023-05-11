namespace CCS.Rosetta.Api.Projects;

public class Project
{
    public string Name { get; }
    public string? Description { get; }

    public Project(string name, string? description)
    {
        Name = name;
        Description = description;
    }
}