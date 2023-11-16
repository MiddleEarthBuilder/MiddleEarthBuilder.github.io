namespace MiddleEarth.Builder.Application.Entities;

public record ProfileSpecialRuleRaw(
    string Name,
    string? Target,
    string Description)
{
    public override string ToString() =>
        string.IsNullOrEmpty(Target) ? Name : $"{Name} ({Target})";
}