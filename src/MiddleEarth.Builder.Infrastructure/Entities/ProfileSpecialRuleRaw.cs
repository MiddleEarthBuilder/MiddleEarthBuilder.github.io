namespace MiddleEarth.Builder.Infrastructure.Entities;

public record ProfileSpecialRuleRaw(
    string Name,
    string? Target)
{
    public override string ToString() =>
        string.IsNullOrEmpty(Target) ? Name : $"{Name} ({Target})";
}