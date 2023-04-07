namespace MiddleEarth.Models;

public class SpecialRule
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;

    public SpecialRule(string name)
    {
        Name = name;
    }
}