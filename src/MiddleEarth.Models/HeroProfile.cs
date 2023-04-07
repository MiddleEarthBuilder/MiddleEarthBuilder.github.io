namespace MiddleEarth.Models;

public class HeroProfile
{
    public ArmyList ArmyList { get; set; }
    public string Name { get; set; }
    public Tier Tier { get; set; }
    public Characteristics Characteristics { get; set; } = new();
    public List<UnitProfileEquipment> Equipment { get; set; } = new();
    public int Cost { get; set; }
    public string? Note { get; set; }

    public HeroProfile(ArmyList armyList, string name, Tier tier)
    {
        ArmyList = armyList;
        Name = name;
        Tier = tier;
    }
}