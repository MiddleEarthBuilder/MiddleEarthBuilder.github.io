namespace MiddleEarth.Builder.Infrastructure.Entities;

public record SpecialRuleRaw(
    string Name,
    string? Target,
    string Description) : IComparable<SpecialRuleRaw>
{
    public int CompareTo(SpecialRuleRaw? other) =>
        string.Compare(Name, other?.Name, StringComparison.Ordinal);

    public override string ToString() =>
        string.IsNullOrEmpty(Target) ? Name : $"{Name} ({Target})";
}