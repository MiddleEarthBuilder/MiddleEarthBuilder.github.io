namespace MiddleEarth.Builder.Application.Domain;

public class Context
{
    private readonly Mapper _mapper;

    public Context()
    {
        _mapper = new Mapper(this);
    }

    public Repository<EquipmentProfile> Equipments { get; } = new(key => new EquipmentProfile(key), profile => profile.Name.Trim());
    public Repository<SpecialRule> SpecialRules { get; } = new(key => new SpecialRule(key), profile => profile.Name!.Trim());
    public Repository<HeroicAction> HeroicActions { get; } = new(key => new HeroicAction(key), profile => profile.Name.Trim());
    public Repository<MagicalPower> MagicalPowers { get; } = new(key => new MagicalPower(key), profile => profile.Name.Trim());
    public Repository<ArmyList> ArmyLists { get; } = new(key => new ArmyList(key), profile => profile.Name.Trim());
    public Repository<Army> Armies { get; } = new(key => new Army(key), profile => profile.Name.Trim());

    public Task LoadAsync(ContextRaw contextRaw)
    {
        Equipments.Load(contextRaw.Equipments?.Select(_mapper.EquipmentProfileMapper.Map));
        SpecialRules.Load(contextRaw.SpecialRules?.Select(_mapper.SpecialRuleMapper.Map));
        HeroicActions.Load(contextRaw.HeroicActions?.Select(_mapper.HeroicActionMapper.Map));
        MagicalPowers.Load(contextRaw.MagicalPowers?.Select(_mapper.MagicalPowerMapper.Map));
        ArmyLists.Load(contextRaw.ArmyLists?.Select(_mapper.ArmyListMapper.Map));
        Armies.Load(contextRaw.Armies?.Select(_mapper.ArmyMapper.Map));

        return Task.CompletedTask;
    }

    public ContextRaw GetRaw() => new(
        Equipments.IsEmpty ? null : Equipments.GetAll().Select(_mapper.EquipmentProfileMapper.Map).ToArray(),
        SpecialRules.IsEmpty ? null : SpecialRules.GetAll().Select(_mapper.SpecialRuleMapper.Map).ToArray(),
        HeroicActions.IsEmpty ? null : HeroicActions.GetAll().Select(_mapper.HeroicActionMapper.Map).ToArray(),
        MagicalPowers.IsEmpty ? null : MagicalPowers.GetAll().Select(_mapper.MagicalPowerMapper.Map).ToArray(),
        ArmyLists.IsEmpty ? null : ArmyLists.GetAll().Select(_mapper.ArmyListMapper.Map).ToArray(),
        Armies.IsEmpty ? null : Armies.GetAll().Select(_mapper.ArmyMapper.Map).ToArray());
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

public class Repository<T>
{
    public bool IsEmpty => _dictionary.Count == 0;
    public int Count => _dictionary.Count;

    private readonly Func<string, T> _create;
    private readonly Func<T, string> _getKey;
    private Dictionary<string, T> _dictionary;

    public Repository(Func<string, T> create, Func<T, string> getKey)
    {
        _create = create;
        _getKey = getKey;
        _dictionary = new Dictionary<string, T>(StringComparer.InvariantCultureIgnoreCase);
    }

    public Repository<T> Load(IEnumerable<T>? items)
    {
        _dictionary = items?.ToDictionary(
                          item => _getKey(item).ToLower().Trim(),
                          item => item, StringComparer.InvariantCultureIgnoreCase)
                      ?? new Dictionary<string, T>(StringComparer.InvariantCultureIgnoreCase);

        return this;
    }

    public IEnumerable<T> GetAll(string? searchText = null, IEnumerable<string>? except = null, bool addItemFromSearchText = false)
    {
        if (searchText == null)
            return _dictionary
                .OrderBy(kv => kv.Key)
                .Select(kv => kv.Value);

        var items = _dictionary
            .Where(kv => kv.Key.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
            .OrderBy(kv => !kv.Key.StartsWith(searchText, StringComparison.InvariantCultureIgnoreCase))
            .ThenBy(kv => kv.Key)
            .ToList();

        if (addItemFromSearchText && !items.Any(kv => string.Equals(kv.Key, searchText, StringComparison.InvariantCultureIgnoreCase)))
            items.Add(new KeyValuePair<string, T>(searchText.ToLower().Trim(), _create(searchText)));

        if (except != null)
        {
            var set = except.ToHashSet(StringComparer.InvariantCultureIgnoreCase);
            items = items.Where(kv => !set.Contains(kv.Key)).ToList();
        }

        return items
            .Select(kv => kv.Value);
    }

    public T GetOrCreate(string key)
    {
        if (_dictionary.TryGetValue(key.Trim(), out var value))
            return value;

        value = _create(key);
        _dictionary.Add(key.ToLower().Trim(), value);
        return value;
    }
}