namespace CCS.Rosetta.Api.Projects;

public class Name
{
    public Name(string name)
    {
        Value = name;
    }

    public string Value { get; private set; }
}