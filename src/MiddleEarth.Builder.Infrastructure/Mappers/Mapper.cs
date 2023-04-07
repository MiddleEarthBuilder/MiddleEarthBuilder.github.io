using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class Mapper
{
    private readonly BuilderContext _context;

    public Mapper(BuilderContext context)
    {
        _context = context;
    }

    public void Map(Army storageValue, ArmyDto value)
    {
        value.Warbands = storageValue.Warbands.Select(Map).ToList();
        value.Leader = value.PotentialLeaders.FirstOrDefault(hero => hero.Name == storageValue.Name);
    }

    public WarbandDto Map(Warband storageValue)
    {
        return new WarbandDto
        {
            Hero = storageValue.Hero == null ? null : Map(storageValue.Hero),
            Followers = storageValue.Followers.Select(Map).ToList()
        };
    }

    private ArmyUnitDto? Map(Hero storageValue)
    {
        var armyList = _context.ArmyLists.GetOrCreate(storageValue.ArmyList);
        var hero = armyList.Heroes.FirstOrDefault(hero => hero.Name == storageValue.Name);
        return hero == null ?
            null :
            new ArmyUnitDto(hero);
    }

    private ArmyUnitDto? Map(Warrior storageValue)
    {
        var armyList = _context.ArmyLists.GetOrCreate(storageValue.ArmyList);
        var warrior = armyList.Warriors.FirstOrDefault(warrior => warrior.Name == storageValue.Name);
        if (warrior != null)
            return new ArmyUnitDto(warrior);
        var hero = armyList.Heroes.FirstOrDefault(hero => hero.Name == storageValue.Name);
        return hero == null ?
            null :
            new ArmyUnitDto(hero);
    }

    public void Map(ArmyList storageValue, ArmyListDto value)
    {
        value.ArmyBonuses = storageValue.ArmyBonuses.Select(Map).ToList();
        value.Heroes = storageValue.Heroes.Select(Map).ToList();
        value.Warriors = storageValue.Warriors.Select(Map).ToList();
        value.Alliances = storageValue.Alliances.Select(Map).ToList();
    }

    public ArmyUnitDto Map(HeroProfile storageValue) => new(
        _context.ArmyLists.GetOrCreate(storageValue.ArmyList),
        storageValue.Name,
        Tier.GetTier(storageValue.Tier))
    {
        Characteristics = Map(storageValue.Characteristics),
        Equipment = storageValue.Equipment.Select(Map).ToList(),
        BaseCost = storageValue.Cost
    };

    public ArmyUnitDto Map(WarriorProfile storageValue) => new(
        _context.ArmyLists.GetOrCreate(storageValue.ArmyList),
        storageValue.Name,
        Tier.Warrior)
    {
        Characteristics = Map(storageValue.Characteristics),
        Equipment = storageValue.Equipment.Select(Map).ToList(),
        BaseCost = storageValue.Cost
    };

    public EquipmentDto Map(ProfileEquipment storageValue)
    {
        var equipment = _context.Equipments.GetOrCreate(storageValue.Name);

        return new EquipmentDto(equipment.Name)
        {
            Description = equipment.Description,
            CharacteristicsBonus = equipment.CharacteristicsBonus,
            Cost = storageValue.Cost,
            IsBow = equipment.IsBow,
            IsAllowedOnce = equipment.IsAllowedOnce,
            DeniedEquipment = equipment.DeniedEquipment,
            ReplacedEquipment = equipment.ReplacedEquipment,
            IsEquipped = storageValue.IsDefault
        };
    }

    public AllianceDto Map(Alliance storageValue) => new(
        _context.ArmyLists.GetOrCreate(storageValue.ArmyList),
        storageValue.Level);

    public EquipmentDto Map(Equipment storageValue) => new(storageValue.Name)
    {
        Description = storageValue.Description,
        CharacteristicsBonus = Map(storageValue.CharacteristicsBonus),
        IsBow = storageValue.IsBow,
        IsAllowedOnce = storageValue.IsAllowedOnce,
        DeniedEquipment = storageValue.DeniedEquipment.ToList(),
        ReplacedEquipment = storageValue.ReplacedEquipment.ToList()
    };

    public SpecialRuleDto Map(SpecialRule storageValue) => new(storageValue.Name)
    {
        Description = storageValue.Description,
        Target = storageValue.Target
    };

    public CharacteristicsDto Map(Characteristics storageValue) => new()
    {
        Move = storageValue.Move,
        Fight = storageValue.Fight,
        Shoot = storageValue.Shoot,
        Strength = storageValue.Strength,
        Defense = storageValue.Defense,
        Attacks = storageValue.Attacks,
        Wounds = storageValue.Wounds,
        Courage = storageValue.Courage,
        Might = storageValue.Might,
        Will = storageValue.Will,
        Fate = storageValue.Fate,
        SpecialRules = storageValue.SpecialRules
            .Select(_context.SpecialRules.GetOrCreate)
            .OfType<SpecialRuleDto>()
            .ToList()
    };
}