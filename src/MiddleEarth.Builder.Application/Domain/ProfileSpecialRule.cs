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

public record ProfileSpecialRuleRaw(
    string Name,
    string? Target)
{
    public override string ToString() =>
        string.IsNullOrEmpty(Target) ? Name : $"{Name} ({Target})";
}

public class ProfileSpecialRuleMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public ProfileSpecialRuleMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ProfileSpecialRuleRaw Map(ProfileSpecialRule value)
    {
        _context.SpecialRules.GetOrCreate(value.Rule.Name!).Update(value.Rule);
        return new ProfileSpecialRuleRaw(
            value.Rule.Name!,
            value.Target);
    }

    public ProfileSpecialRule Map(ProfileSpecialRuleRaw raw) => new(_context.SpecialRules.GetOrCreate(raw.Name))
    {
        Target = raw.Target
    };
}