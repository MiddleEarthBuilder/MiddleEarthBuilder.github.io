namespace MiddleEarth.Models;

public class ArmyUnitDto
{
    public ArmyListDto ArmyList { get; set; }
    public string Name { get; set; }
    public Tier Tier { get; set; }
    public CharacteristicsDto Characteristics { get; set; } = new();
    public List<EquipmentDto> Equipment { get; set; } = new();
    public int BaseCost { get; set; }
    public int Count { get; set; } = 1;
    public bool IsHero { get; set; }
    public string? Note { get; set; }
    public int UnitCost => BaseCost + Equipment.Sum(equipment => equipment.IsEquipped ? equipment.Cost : 0);
    public int TotalCost => Count * UnitCost;
    public bool HasBow => Equipment.Any(equipment => equipment is { IsEquipped: true, IsBow: true });

    public ArmyUnitDto(ArmyListDto armyList, string name, Tier tier)
    {
        ArmyList = armyList;
        Name = name;
        Tier = tier;
    }

    public ArmyUnitDto(ArmyUnitDto source)
    {
        ArmyList = source.ArmyList;
        Name = source.Name;
        Tier = source.Tier;
        Characteristics = new CharacteristicsDto(source.Characteristics);
        Equipment = source.Equipment.Select(equipment => new EquipmentDto(equipment)).ToList();
        BaseCost = source.BaseCost;
        Note = source.Note;
    }
}