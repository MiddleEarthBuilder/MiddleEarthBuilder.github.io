namespace MiddleEarth.Models;

public class ArmyDto
{
    public ArmyListDto List { get; set; }
    public ArmyUnitDto? Leader { get; set; }
    public List<WarbandDto> Warbands { get; set; } = new();

    public int PointsCount => Warbands.Count > 0 ? Warbands.Sum(warband => warband.Points) : 0;
    public int UnitsCount => Warbands.Count > 0 ? Warbands.Sum(warband => warband.UnitsCount) : 0;
    public int BrokenCount => UnitsCount / 2;
    public int QuarterCount => UnitsCount / 4;
    public int BowsCount => Warbands.Count > 0 ? Warbands.Sum(warband => warband.BowsCount) : 0;
    public List<ArmyUnitDto> PotentialLeaders
    {
        get
        {
            var maxTier = Warbands.Max(dto => dto.Hero.Tier);
            return Warbands.Select(warband => warband.Hero).Where(hero => hero.Tier == maxTier).ToList();
        }
    }

    public ArmyDto(ArmyListDto list)
    {
        List = list;
    }
}

public class WarbandDto
{
    public ArmyUnitDto? Hero { get; set; }
    /// <summary>
    /// Dictionary of units and their count in a warband
    /// </summary>
    public List<ArmyUnitDto> Followers { get; set; } = new();

    public int Points => Hero?.TotalCost ?? 0 + (Followers.Count > 0 ? Followers.Sum(warrior => warrior.TotalCost) : 0);
    public int UnitsCount => (Hero == null ? 0 : 1) + Followers.Sum(warrior => warrior.Count);
    public int BowsCount => Followers.Sum(warrior => warrior.HasBow ? warrior.Count : 0);
}

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

public class ArmyListDto
{
    public string Name { get; set; }
    public Side Side { get; set; }
    public List<ArmyUnitDto> Heroes { get; set; } = new();
    public List<ArmyUnitDto> Warriors { get; set; } = new();
    public List<SpecialRuleDto> ArmyBonuses { get; set; } = new();
    public List<AllianceDto> Alliances { get; set; } = new();

    public ArmyListDto(string name)
    {
        Name = name;
    }
}

public class AllianceDto
{
    public ArmyListDto ArmyList { get; set; }
    public AllianceLevel Level { get; set; }

    public AllianceDto(ArmyListDto armyList, AllianceLevel level)
    {
        ArmyList = armyList;
        Level = level;
    }
}

public class CharacteristicsDto
{
    public int Move { get; set; }
    public int Fight { get; set; }
    public int? Shoot { get; set; }
    public int Strength { get; set; }
    public int Defense { get; set; }
    public int Attacks { get; set; }
    public int Wounds { get; set; }
    public int Courage { get; set; }
    public int? Might { get; set; }
    public int? Will { get; set; }
    public int? Fate { get; set; }
    public List<SpecialRuleDto> SpecialRules { get; set; } = new();

    public CharacteristicsDto() { }

    public CharacteristicsDto(CharacteristicsDto source)
    {
        Move = source.Move;
        Fight = source.Fight;
        Shoot = source.Shoot;
        Strength = source.Strength;
        Defense = source.Defense;
        Attacks = source.Attacks;
        Wounds = source.Wounds;
        Courage = source.Courage;
        Might = source.Might;
        Will = source.Will;
        Fate = source.Fate;
    }

    private string FightString => Shoot == null ? $"{Fight}/-" : $"{Fight}/{Shoot}+";

    public override string ToString() =>
        $"Mv {Move}\", F{FightString}, S{Strength}, D{Defense}, A{Attacks}, W{Wounds}, C{Courage}, M{Might}, W{Will}, F{Fate}";
}

public class SpecialRuleDto
{
    public string Name { get; set; }
    public string? Target { get; set; }
    public string Description { get; set; } = string.Empty;

    public SpecialRuleDto(string name)
    {
        Name = name;
    }
}

public class EquipmentDto
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public CharacteristicsDto? CharacteristicsBonus { get; set; }
    public int Cost { get; set; }
    public bool IsBow { get; set; }
    public bool IsAllowedOnce { get; set; } = true;
    public List<string> DeniedEquipment { get; set; } = new();
    public List<string> ReplacedEquipment { get; set; } = new();
    public bool IsEquipped { get; set; }

    public EquipmentDto(string name)
    {
        Name = name;
    }

    public EquipmentDto(EquipmentDto source)
    {
        Name = source.Name;
        Description = source.Description;
        CharacteristicsBonus = source.CharacteristicsBonus == null ? null : new CharacteristicsDto(source.CharacteristicsBonus);
        Cost = source.Cost;
        IsBow = source.IsBow;
        IsAllowedOnce = source.IsAllowedOnce;
        DeniedEquipment = new List<string>(source.DeniedEquipment);
        ReplacedEquipment = new List<string>(source.ReplacedEquipment);
        IsEquipped = source.IsEquipped;
    }
}