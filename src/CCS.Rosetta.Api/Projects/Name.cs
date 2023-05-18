using System.Text.RegularExpressions;

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

        if (HasInvalidFormat(name))
        {
            throw new FormatException("Name format is not valid");
        }

        Value = name;
    }

    private static bool HasInvalidFormat(string name)
    {
        Regex regex = new(@"[\w\-]{4,}");
        return !regex.IsMatch(name);
    }
}