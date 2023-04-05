namespace MiddleEarth.Models;

public class ArmyDto
{
    private string Leader { get; set; }
    public List<WarbandDto> Warbands { get; set; } = new();

    public int PointsCount => Warbands.Count > 0 ? Warbands.Sum(warband => warband.Points) : 0;
    public int UnitsCount => Warbands.Count > 0 ? Warbands.Sum(warband => warband.UnitsCount) : 0;
    public int BrokenCount => UnitsCount / 2;
    public int QuarterCount => UnitsCount / 4;
    public int BowsCount => Warbands.Count > 0 ? Warbands.Sum(warband => warband.BowsCount) : 0;
}

public class WarbandDto
{
    public ArmyHeroDto? Hero { get; set; }
    /// <summary>
    /// Dictionary of units and their count in a warband
    /// </summary>
    public Dictionary<ArmyWarriorDto, int> Followers { get; set; } = new();

    public int Points => Hero?.Cost ?? 0 + (Followers.Count > 0 ? Followers.Sum(kv => kv.Value * kv.Key.Cost) : 0);
    public int UnitsCount => (Hero == null ? 0 : 1) + Followers.Sum(kv => kv.Value);
    public int BowsCount => Followers.Sum(kv => kv.Key.HasBow ? kv.Value : 0);
}

public record ArmyHeroDto(
    ArmyListDto ArmyList,
    string Name,
    Tier Tier,
    HeroCharacteristicsDto Characteristics,
    List<EquipmentDto> Equipment,
    int BaseCost)
{
    public int Cost => BaseCost + Equipment.Sum(equipment => equipment.IsEquipped ? equipment.Cost : 0);
}

public record ArmyWarriorDto(
    ArmyListDto ArmyList,
    string Name,
    WarriorCharacteristics Characteristics,
    List<EquipmentDto> Equipment,
    int BaseCost)
{
    public int Cost => BaseCost + Equipment.Sum(equipment => equipment.IsEquipped ? equipment.Cost : 0);
    public bool HasBow => Equipment.Any(equipment => equipment is { IsEquipped: true, IsBow: true });
}

public record ArmyListDto(
    string Name,
    ArmyHeroDto[] Heroes,
    ArmyWarriorDto[] Warriors,
    SpecialRule[] ArmyBonuses,
    AllianceDto[] Alliances);

public record AllianceDto(
    ArmyListDto ArmyList,
    AllianceLevel Level);

public class HeroCharacteristicsDto : WarriorCharacteristicsDto
{
    public int Might { get; set; }
    public int Will { get; set; }
    public int Fate { get; set; }

    public override string ToString() =>
        $"{base.ToString()}, M{Might}, W{Will}, F{Fate}";
}

public class WarriorCharacteristicsDto
{
    public int Move { get; set; }
    public FightCharacteristic Fight { get; set; }
    public int Strength { get; set; }
    public int Defense { get; set; }
    public int Attacks { get; set; }
    public int Wounds { get; set; }
    public int Courage { get; set; }
    public SpecialRule[] SpecialRules { get; set; }

    public override string ToString() =>
        $"Mv {Move}\", F{Fight}, S{Strength}, D{Defense}, A{Attacks}, W{Wounds}, C{Courage}";
}

public record EquipmentDto(
    string Name,
    string Description,
    WarriorCharacteristics CharacteristicsBonus,
    int Cost,
    bool IsBow = false,
    bool IsAllowedOnce = true,
    string[]? DeniedEquipment = null,
    string[]? ReplacedEquipment = null)
{
    public bool IsEquipped { get; set; }
}