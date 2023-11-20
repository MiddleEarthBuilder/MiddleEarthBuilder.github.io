namespace MiddleEarth.Builder.Application.Domain;

public class HeroProfile
{
    public ArmyList ArmyList { get; set; }
    public string Name { get; set; }
    public Tier Tier { get; set; }
    public TierEnum TierEnum
    {
        get => Tier.Id;
        set => Tier = Tier.GetTier(value);
    }

    public List<string> Keywords { get; set; } = new();
    public string KeywordsString
    {
        get => string.Join(", ", Keywords);
        set => Keywords = value.Split(",")
            .Select(s => s.Trim()).ToList();
    }

    public Characteristics Characteristics { get; set; } = new();

    public List<ProfileEquipment> Equipment { get; set; } = new();
    public IEnumerable<ProfileEquipment> DefaultEquipment => Equipment
        .Where(equipment => equipment.DefaultCount > 0);
    public IEnumerable<ProfileEquipment> OptionalEquipment => Equipment
        .Where(equipment => equipment.DefaultCount == 0);

    public List<ProfileSpecialRule> SpecialRules { get; set; } = new();
    public List<HeroicAction> HeroicActions { get; set; } = new();
    public List<ProfileMagicalPower> MagicalPowers { get; set; } = new();
    public int Cost { get; set; }
    public string? Note { get; set; }

    public HeroProfile(ArmyList armyList, string name, Tier tier)
    {
        ArmyList = armyList;
        Name = name;
        Tier = tier;
    }
}

public record HeroProfileRaw(
    string ArmyList,
    string Name,
    string Tier,
    string[] Keywords,
    CharacteristicsRaw Characteristics,
    ProfileEquipmentRaw[]? Equipment,
    ProfileSpecialRuleRaw[]? SpecialRules,
    HeroicActionRaw[]? HeroicActions,
    ProfileMagicalPowerRaw[]? MagicalPowers,
    int Cost,
    string? Note);

public class HeroProfileMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public HeroProfileMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public HeroProfileRaw Map(HeroProfile value) => new(
        value.ArmyList.Name,
        value.Name,
        value.Tier.Name,
        value.Keywords.ToArray(),
        _mapper.CharacteristicsMapper.Map(value.Characteristics),
        value.Equipment.Select(_mapper.ProfileEquipmentMapper.Map).ToArray(),
        value.SpecialRules.Select(_mapper.ProfileSpecialRuleMapper.Map).ToArray(),
        value.HeroicActions.Select(_mapper.HeroicActionMapper.Map).ToArray(),
        value.MagicalPowers.Select(_mapper.ProfileMagicalPowerMapper.Map).ToArray(),
        value.Cost,
        value.Note);

    public HeroProfile Map(HeroProfileRaw raw) => new(
        _context.ArmyLists.GetOrCreate(raw.ArmyList),
        raw.Name,
        Tier.GetTier(raw.Tier))
    {
        Keywords = raw.Keywords.ToList(),
        Characteristics = _mapper.CharacteristicsMapper.Map(raw.Characteristics),
        Equipment = raw.Equipment?.Select(_mapper.ProfileEquipmentMapper.Map).ToList() ?? new List<ProfileEquipment>(),
        SpecialRules = raw.SpecialRules?.Select(_mapper.ProfileSpecialRuleMapper.Map).ToList() ?? new List<ProfileSpecialRule>(),
        HeroicActions = raw.HeroicActions?.Select(_mapper.HeroicActionMapper.Map).ToList() ?? new List<HeroicAction>(),
        MagicalPowers = raw.MagicalPowers?.Select(_mapper.ProfileMagicalPowerMapper.Map).ToList() ?? new List<ProfileMagicalPower>(),
        Cost = raw.Cost,
        Note = raw.Note
    };
}