namespace MiddleEarth.Models;

public class ProfileSpecialRule
{
    public SpecialRule Rule { get; set; }
    public string? Target { get; set; }

    public string FullName => string.IsNullOrEmpty(Target) ? Rule.Name : $"{Rule.Name} ({Target})";

    public ProfileSpecialRule(SpecialRule rule)
    {
        Rule = rule;
    }
}