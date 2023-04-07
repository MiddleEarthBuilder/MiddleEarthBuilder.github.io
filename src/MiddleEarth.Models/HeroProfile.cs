namespace MiddleEarth.Models;

public class HeroProfile
{
    public ArmyList ArmyList { get; set; }
    public string Name { get; set; }
    public Tier Tier { get; set; }
    public List<string> Keywords { get; set; }
    public Characteristics Characteristics { get; set; } = new();
    public List<ProfileEquipment> Equipment { get; set; } = new();
    public List<ProfileSpecialRule> SpecialRules { get; set; } = new();
    public int Cost { get; set; }
    public string? Note { get; set; }

    public HeroProfile(ArmyList armyList, string name, Tier tier)
    {
        ArmyList = armyList;
        Name = name;
        Tier = tier;
    }
}