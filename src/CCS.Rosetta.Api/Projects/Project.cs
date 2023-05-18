namespace CCS.Rosetta.Api.Projects;

public class Project
{
    public Name Name { get; }
    public string? Description { get; }
    
    public Project(Name name, string? description)
    {
        Name = name;
        Description = description;
    }
}