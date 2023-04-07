namespace MiddleEarth.Models;

public class WarriorProfile
{
    public ArmyList ArmyList { get; set; }
    public string Name { get; set; }
    public Characteristics Characteristics { get; set; } = new();
    public List<UnitProfileEquipment> Equipment { get; set; } = new();
    public List<UnitProfileSpecialRule> SpecialRules { get; set; } = new();
    public int Cost { get; set; }
    public string? Note { get; set; }

    public WarriorProfile(ArmyList armyList, string name)
    {
        ArmyList = armyList;
        Name = name;
    }
}