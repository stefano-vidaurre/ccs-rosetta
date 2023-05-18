namespace CCS.Rosetta.Api.Projects;

public class Name
{
    public string Value { get; private set; }
 
    public Name(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(name));
        }

        Value = name;
    }
}