using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MiddleEarth.Builder.Application.Domain;

public class ProfileSpecialRule
{
    [Required]
    public SpecialRule Rule { get; set; } = new();
    public string? Target { get; set; }

    public string FullName => string.IsNullOrEmpty(Target) ? Rule.Name! : $"{Rule.Name} ({Target})";

    public ProfileSpecialRule() { }

    public ProfileSpecialRule(SpecialRule rule)
    {
        Rule = rule;
    }

    public ProfileSpecialRule(string nameWithTarget, string description)
    {
        var match = Regex.Match(nameWithTarget, @"(?<name>.*)( \((?<target>.*\)))?");
        if (match.Success)
        {
            var name = match.Groups["name"].Value;
            var target = match.Groups["target"].Value;
            Rule = new SpecialRule(name, description);
            Target = target;
        }
        else
        {
            Rule = new SpecialRule(nameWithTarget, description);
        }
    }
}