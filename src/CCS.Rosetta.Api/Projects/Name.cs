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

        if (HasValidFormat(name))
        {
            throw new FormatException("Name format is not valid");
        }

        Value = name;
    }

    private static bool HasValidFormat(string name)
    {
        return name.Length < 4 || name.Contains(' ') || name.Contains('*');
    }
}