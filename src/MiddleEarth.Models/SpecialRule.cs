namespace MiddleEarth.Models;

public class SpecialRuleDto
{
    public string Name { get; set; }
    public string? Target { get; set; }
    public string Description { get; set; } = string.Empty;

    public SpecialRuleDto(string name)
    {
        Name = name;
    }
}