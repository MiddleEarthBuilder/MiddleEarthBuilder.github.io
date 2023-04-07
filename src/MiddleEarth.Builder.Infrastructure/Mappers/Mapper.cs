using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class Mapper
{
    private readonly BuilderContext _context;

    public Mapper(BuilderContext context)
    {
        _context = context;
    }

    public void Map(ArmyRaw storageValue, Army value)
    {
        value.Warbands = storageValue.Warbands.Select(Map).ToList();
        value.Leader = value.PotentialLeaders.FirstOrDefault(hero => hero.Name == storageValue.Name);
    }

    public Warband Map(WarbandRaw storageValue)
    {
        return new Warband
        {
            Hero = storageValue.Hero == null ? null : Map(storageValue.Hero),
            Followers = storageValue.Followers.Select(Map).ToList()
        };
    }

    private Warrior? Map(HeroRaw storageValue)
    {
        var armyList = _context.ArmyLists.GetOrCreate(storageValue.ArmyList);
        var hero = armyList.Heroes.FirstOrDefault(hero => hero.Name == storageValue.Name);
        return hero == null ?
            null :
            new Warrior(hero);
    }

    private Warrior? Map(WarriorRaw storageValue)
    {
        var armyList = _context.ArmyLists.GetOrCreate(storageValue.ArmyList);
        var warrior = armyList.Warriors.FirstOrDefault(warrior => warrior.Name == storageValue.Name);
        if (warrior != null)
            return new Warrior(warrior);
        var hero = armyList.Heroes.FirstOrDefault(hero => hero.Name == storageValue.Name);
        return hero == null ?
            null :
            new Warrior(hero);
    }

    public void Map(ArmyListRaw storageValue, ArmyList value)
    {
        value.ArmyBonuses = storageValue.ArmyBonuses.Select(Map).ToList();
        value.Heroes = storageValue.Heroes.Select(Map).ToList();
        value.Warriors = storageValue.Warriors.Select(Map).ToList();
        value.Alliances = storageValue.Alliances.Select(Map).ToList();
    }

    public ArmyListRaw Map(ArmyList value) => new(
        value.Name,
        value.Side,
        value.Heroes.Select(MapHero).ToArray(),
        value.Warriors.Select(MapWarrior).ToArray(),
        value.ArmyBonuses.Select(Map).ToArray(),
        value.Alliances.Select(Map).ToArray());

    private HeroProfileRaw MapHero(Warrior value)
    {
        throw new NotImplementedException();
    }

    private WarriorProfileRaw MapWarrior(Warrior value) => new(
        value.ArmyList.Name,
        value.Name,
        Map(value.Characteristics),
        value.Equipment.Select(MapProfile).ToArray(),
        value.BaseCost,
        value.Note);

    private ProfileEquipmentRaw MapProfile(Equipment value) => new(
        value.Name,
        value.IsDefault,
        value.Cost);

    private AllianceRaw Map(Alliance value) => new(
        value.ArmyList.Name,
        value.Level);

    public Warrior Map(HeroProfileRaw storageValue) => new(
        _context.ArmyLists.GetOrCreate(storageValue.ArmyList),
        storageValue.Name,
        Tier.GetTier(storageValue.Tier))
    {
        Characteristics = Map(storageValue.Characteristics),
        Equipment = storageValue.Equipment.Select(Map).ToList(),
        BaseCost = storageValue.Cost,
        Note = storageValue.Note
    };

    public Warrior Map(WarriorProfileRaw storageValue) => new(
        _context.ArmyLists.GetOrCreate(storageValue.ArmyList),
        storageValue.Name,
        Tier.Warrior)
    {
        Characteristics = Map(storageValue.Characteristics),
        Equipment = storageValue.Equipment.Select(Map).ToList(),
        BaseCost = storageValue.Cost,
        Note = storageValue.Note
    };

    public Equipment Map(ProfileEquipmentRaw storageValue)
    {
        var equipment = _context.Equipments.GetOrCreate(storageValue.Name);

        return new Equipment(equipment.Name)
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

    public Alliance Map(AllianceRaw storageValue) => new(
        _context.ArmyLists.GetOrCreate(storageValue.ArmyList),
        storageValue.Level);

    public Equipment Map(EquipmentRaw storageValue) => new(storageValue.Name)
    {
        Description = storageValue.Description,
        CharacteristicsBonus = Map(storageValue.CharacteristicsBonus),
        IsBow = storageValue.IsBow,
        IsAllowedOnce = storageValue.IsAllowedOnce,
        DeniedEquipment = storageValue.DeniedEquipment.ToList(),
        ReplacedEquipment = storageValue.ReplacedEquipment.ToList()
    };

    public EquipmentRaw Map(Equipment value) => new(
        value.Name,
        value.Description,
        value.CharacteristicsBonus == null ? null : Map(value.CharacteristicsBonus),
        value.IsBow,
        value.IsAllowedOnce,
        value.DeniedEquipment.ToArray(),
        value.ReplacedEquipment.ToArray());

    private CharacteristicsRaw Map(Characteristics value) => new(
        value.Move,
        value.Fight,
        value.Shoot,
        value.Strength,
        value.Defense,
        value.Attacks,
        value.Wounds,
        value.Courage,
        value.Might,
        value.Will,
        value.Fate,
        value.SpecialRules.Select(rule => rule.Name).ToArray());

    public SpecialRule Map(SpecialRuleRaw storageValue) => new(storageValue.Name)
    {
        Description = storageValue.Description,
        Target = storageValue.Target
    };

    public Characteristics Map(CharacteristicsRaw storageValue) => new()
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
            .OfType<SpecialRule>()
            .ToList()
    };

    public SpecialRuleRaw Map(SpecialRule value) => new(
        value.Name,
        value.Target,
        value.Description);
}