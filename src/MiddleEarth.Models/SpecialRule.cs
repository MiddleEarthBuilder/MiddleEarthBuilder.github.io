namespace MiddleEarth.Models;

public record SpecialRule(
    string Name,
    string? Target,
    string Description) : IComparable<SpecialRule>
{
    public int CompareTo(SpecialRule? other) => 
        string.Compare(Name, other?.Name, StringComparison.Ordinal);

    public override string ToString() => 
        string.IsNullOrEmpty(Target) ? Name : $"{Name} ({Target})";
}