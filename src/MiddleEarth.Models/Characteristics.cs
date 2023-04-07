namespace MiddleEarth.Models;

public record Characteristics(
    int Move,
    int Fight,
    int? Shoot,
    int Strength,
    int Defense,
    int Attacks,
    int Wounds,
    int Courage,
    int? Might = null,
    int? Will = null,
    int? Fate = null,
    string[]? SpecialRules = null)
{
    public string[] SpecialRules { get; } = SpecialRules ?? Array.Empty<string>();
    private string FightString => Shoot == null ? $"{Fight}/-" : $"{Fight}/{Shoot}+";

    public override string ToString() =>
        $"Mv {Move}\", F{FightString}, S{Strength}, D{Defense}, A{Attacks}, W{Wounds}, C{Courage}, M{Might}, W{Will}, F{Fate}, Rules: {string.Join(", ", SpecialRules)}";
}

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