using System.ComponentModel.DataAnnotations;

namespace MiddleEarth.Builder.Application.Domain;

public class SpecialRule
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }

    public SpecialRule() { }

    public SpecialRule(string name)
    {
        Name = name;
    }

    public SpecialRule(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void Update(SpecialRule value)
    {
        Description = value.Description;
    }
}

public record SpecialRuleRaw(
    string Name,
    string Description);

public class SpecialRuleMapper
{
    public SpecialRuleRaw Map(SpecialRule value) => new(
        value.Name!,
        value.Description!);

    public SpecialRule Map(SpecialRuleRaw raw) => new(raw.Name)
    {
        Description = raw.Description
    };
}