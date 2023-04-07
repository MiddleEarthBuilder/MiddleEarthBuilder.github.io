namespace MiddleEarth.Models;

public class Warrior
{
    public ArmyList ArmyList { get; set; }
    public string Name { get; set; }
    public Tier Tier { get; set; }
    public Characteristics Characteristics { get; set; } = new();
    public List<Equipment> Equipment { get; set; } = new();
    public int BaseCost { get; set; }
    public int Count { get; set; } = 1;
    public bool IsHero { get; set; }
    public string? Note { get; set; }
    public int UnitCost => BaseCost + Equipment.Sum(equipment => equipment.IsEquipped ? equipment.Cost : 0);
    public int TotalCost => Count * UnitCost;
    public bool HasBow => Equipment.Any(equipment => equipment is { IsEquipped: true, IsBow: true });

    public Warrior(ArmyList armyList, string name, Tier tier)
    {
        ArmyList = armyList;
        Name = name;
        Tier = tier;
    }

    public Warrior(Warrior source)
    {
        ArmyList = source.ArmyList;
        Name = source.Name;
        Tier = source.Tier;
        Characteristics = new Characteristics(source.Characteristics);
        Equipment = source.Equipment.Select(equipment => new Equipment(equipment)).ToList();
        BaseCost = source.BaseCost;
        Note = source.Note;
    }
}