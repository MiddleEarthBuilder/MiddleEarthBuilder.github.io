namespace MiddleEarth.Models;

public class WarriorProfile
{
    public ArmyList ArmyList { get; set; }
    public string Name { get; set; }
    public string KeywordsString
    {
        get => string.Join(", ", Keywords);
        set => Keywords = value.Split(",")
            .Select(s => s.Trim()).ToList();
    }
    public List<string> Keywords { get; set; } = new();
    public Characteristics Characteristics { get; set; } = new();
    public List<ProfileEquipment> Equipment { get; set; } = new();
    public List<ProfileSpecialRule> SpecialRules { get; set; } = new();
    public int Cost { get; set; }
    public string? Note { get; set; }

    public WarriorProfile(ArmyList armyList, string name)
    {
        ArmyList = armyList;
        Name = name;
    }
}