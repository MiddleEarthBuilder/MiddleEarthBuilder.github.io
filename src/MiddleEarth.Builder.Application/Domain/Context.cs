using System.Collections.ObjectModel;

namespace MiddleEarth.Builder.Application.Domain;

public class Context
{
    private readonly Mapper _mapper;

    public Context()
    {
        _mapper = new Mapper(this);
    }

    private Dictionary<string, ArmyList> ArmyLists { get; set; } = new();
    private Dictionary<string, SpecialRule> SpecialRules { get; set; } = new();
    private Dictionary<string, EquipmentProfile> Equipments { get; set; } = new();
    private Dictionary<string, HeroicAction> HeroicActions { get; set; } = new();
    private Dictionary<string, MagicalPower> MagicalPowers { get; set; } = new();
    private Dictionary<string, Army> Armies { get; set; } = new();

    public async Task Load(ContextRaw contextRaw)
    {
        Equipments = contextRaw.Equipments?.Select(_mapper.EquipmentProfileMapper.Map)
            .ToDictionary(rule => rule.Name!, rule => rule) ?? new Dictionary<string, EquipmentProfile>();
        SpecialRules = contextRaw.SpecialRules?.Select(_mapper.SpecialRuleMapper.Map)
            .ToDictionary(rule => rule.Name!, rule => rule) ?? new Dictionary<string, SpecialRule>();
        HeroicActions = contextRaw.HeroicActions?.Select(_mapper.HeroicActionMapper.Map)
            .ToDictionary(rule => rule.Name!, rule => rule) ?? new Dictionary<string, HeroicAction>();
        MagicalPowers = contextRaw.MagicalPowers?.Select(_mapper.MagicalPowerMapper.Map)
            .ToDictionary(rule => rule.Name!, rule => rule) ?? new Dictionary<string, MagicalPower>();
        ArmyLists = contextRaw.ArmyLists?.Select(_mapper.ArmyListMapper.Map)
            .ToDictionary(rule => rule.Name!, rule => rule) ?? new Dictionary<string, ArmyList>();
        Armies = contextRaw.Armies?.Select(_mapper.ArmyMapper.Map)
            .ToDictionary(rule => rule.Name!, rule => rule) ?? new Dictionary<string, Army>();
    }

    public async Task<Army> CreateArmy(string name, string armyListName)
    {
        var army = new Army(name, ArmyLists[armyListName]);
        Armies.Add(name, army);
        return army;
    }

    public ContextRaw GetRaw() => new(
        Equipments.Any() ?
            Equipments.Values
                .OrderBy(profile => profile.Name)
                .Select(_mapper.EquipmentProfileMapper.Map)
                .ToArray() : null,
        SpecialRules.Any() ?
            SpecialRules.Values
                .OrderBy(rule => rule.Name)
                .Select(_mapper.SpecialRuleMapper.Map)
                .ToArray() : null,
        HeroicActions.Any() ?
            HeroicActions.Values
                .OrderBy(rule => rule.Name)
                .Select(_mapper.HeroicActionMapper.Map)
                .ToArray() : null,
        MagicalPowers.Any() ?
            MagicalPowers.Values
                .OrderBy(rule => rule.Name)
                .Select(_mapper.MagicalPowerMapper.Map)
                .ToArray() : null,
        ArmyLists.Any() ?
            ArmyLists.Values
                .OrderBy(list => list.Side)
                .ThenBy(list => list.Name)
                .Select(_mapper.ArmyListMapper.Map)
                .ToArray() : null,
        Armies.Any() ?
            Armies.Values
                .OrderBy(army => army.Name)
                .Select(_mapper.ArmyMapper.Map)
                .ToArray() : null);

    public IReadOnlyCollection<ArmyList> GetArmyLists(Side? side = null)
    {
        var values = ArmyLists.Values;
        if (side is null or Side.Undefined)
            return new ReadOnlyCollection<ArmyList>(values.ToList());

        return new ReadOnlyCollection<ArmyList>(values
            .Where(list => list.Side == side).ToList());
    }

    public ArmyList GetOrCreateArmyList(string name)
    {
        if (ArmyLists.TryGetValue(name, out var value))
            return value;

        value = new ArmyList(name);
        ArmyLists.Add(name, value);
        return value;
    }

    public IReadOnlyCollection<SpecialRule> GetSpecialRules() =>
        new ReadOnlyCollection<SpecialRule>(SpecialRules.Values.ToList());

    public SpecialRule GetOrCreateSpecialRule(string name)
    {
        if (SpecialRules.TryGetValue(name, out var value))
            return value;

        value = new SpecialRule(name);
        SpecialRules.Add(name, value);
        return value;
    }

    public IReadOnlyCollection<EquipmentProfile> GetEquipments() =>
        new ReadOnlyCollection<EquipmentProfile>(Equipments.Values.ToList());

    public EquipmentProfile GetOrCreateEquipment(string name)
    {
        if (Equipments.TryGetValue(name, out var value))
            return value;

        value = new EquipmentProfile(name);
        Equipments.Add(name, value);
        return value;
    }

    public IReadOnlyCollection<Army> GetArmies() =>
        new ReadOnlyCollection<Army>(Armies.Values.ToList());

    public Army GetOrCreateArmy(string name, ArmyList armyList)
    {
        if (Armies.TryGetValue(name, out var value))
            return value;

        value = new Army(name, armyList);
        Armies.Add(name, value);
        return value;
    }

    public IReadOnlyCollection<HeroicAction> GetHeroicActions() =>
        new ReadOnlyCollection<HeroicAction>(HeroicActions.Values.ToList());

    public HeroicAction GetOrCreateHeroicAction(string name)
    {
        if (HeroicActions.TryGetValue(name, out var value))
            return value;

        value = new HeroicAction(name);
        HeroicActions.Add(name, value);
        return value;
    }

    public IReadOnlyCollection<MagicalPower> GetMagicalPowers() =>
        new ReadOnlyCollection<MagicalPower>(MagicalPowers.Values.ToList());

    public MagicalPower GetOrCreateMagicalPower(string name)
    {
        if (MagicalPowers.TryGetValue(name, out var value))
            return value;

        value = new MagicalPower(name);
        MagicalPowers.Add(name, value);
        return value;
    }
}

public record ContextRaw(
    EquipmentProfileRaw[]? Equipments,
    SpecialRuleRaw[]? SpecialRules,
    HeroicActionRaw[]? HeroicActions,
    MagicalPowerRaw[]? MagicalPowers,
    ArmyListRaw[]? ArmyLists,
    ArmyRaw[]? Armies);

public class Mapper
{
    public readonly AllianceMapper AllianceMapper;
    public readonly ArmyListMapper ArmyListMapper;
    public readonly ArmyMapper ArmyMapper;
    public readonly CharacteristicsMapper CharacteristicsMapper;
    public readonly EquipmentMapper EquipmentMapper;
    public readonly EquipmentProfileMapper EquipmentProfileMapper;
    public readonly HeroicActionMapper HeroicActionMapper;
    public readonly HeroMapper HeroMapper;
    public readonly HeroProfileMapper HeroProfileMapper;
    public readonly MagicalPowerMapper MagicalPowerMapper;
    public readonly ProfileEquipmentMapper ProfileEquipmentMapper;
    public readonly ProfileMagicalPowerMapper ProfileMagicalPowerMapper;
    public readonly ProfileSpecialRuleMapper ProfileSpecialRuleMapper;
    public readonly SpecialRuleMapper SpecialRuleMapper;
    public readonly WarbandMapper WarbandMapper;
    public readonly WarriorMapper WarriorMapper;
    public readonly WarriorProfileMapper WarriorProfileMapper;

    public Mapper(Context context)
    {
        AllianceMapper = new AllianceMapper(context);
        ArmyListMapper = new ArmyListMapper(this);
        ArmyMapper = new ArmyMapper(context, this);
        CharacteristicsMapper = new CharacteristicsMapper(context, this);
        EquipmentMapper = new EquipmentMapper(context);
        EquipmentProfileMapper = new EquipmentProfileMapper(this);
        HeroicActionMapper = new HeroicActionMapper();
        HeroMapper = new HeroMapper(context, this);
        HeroProfileMapper = new HeroProfileMapper(context, this);
        MagicalPowerMapper = new MagicalPowerMapper();
        ProfileEquipmentMapper = new ProfileEquipmentMapper(context, this);
        ProfileMagicalPowerMapper = new ProfileMagicalPowerMapper(context, this);
        ProfileSpecialRuleMapper = new ProfileSpecialRuleMapper(context, this);
        SpecialRuleMapper = new SpecialRuleMapper();
        WarbandMapper = new WarbandMapper(context, this);
        WarriorMapper = new WarriorMapper(context, this);
        WarriorProfileMapper = new WarriorProfileMapper(context, this);
    }
}