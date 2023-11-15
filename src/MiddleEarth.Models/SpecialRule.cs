using System.ComponentModel.DataAnnotations;

namespace MiddleEarth.Models;

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
}