namespace MiddleEarth.Models;

public class UnitProfileSpecialRule
{
    public SpecialRule Rule { get; set; }
    public string? Target { get; set; }

    public UnitProfileSpecialRule(SpecialRule rule)
    {
        Rule = rule;
    }
}