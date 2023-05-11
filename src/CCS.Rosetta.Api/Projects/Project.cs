namespace CCS.Rosetta.Api.Projects;

public class Project
{
    public Project(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; }
    public string? Description { get; }
}