namespace MiddleEarth.Builder.Infrastructure.Entities;

public record ProfileSpecialRuleRaw(
    string Name,
    string? Target,
    string Description)
{
    public override string ToString() =>
        string.IsNullOrEmpty(Target) ? Name : $"{Name} ({Target})";
}