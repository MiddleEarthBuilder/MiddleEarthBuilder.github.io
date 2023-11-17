using System.Text.Json.Serialization;

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
    [JsonIgnore]
    public string KeywordsString
    {
        get => string.Join(", ", Keywords);
        set => Keywords = value.Split(",")
            .Select(s => s.Trim()).ToList();
    }

    public Characteristics Characteristics { get; set; } = new();

    public List<ProfileEquipment> Equipment { get; set; } = new();
    [JsonIgnore]
    public IEnumerable<ProfileEquipment> DefaultEquipment => Equipment
        .Where(equipment => equipment.DefaultCount > 0);
    [JsonIgnore]
    public IEnumerable<ProfileEquipment> OptionalEquipment => Equipment
        .Where(equipment => equipment.DefaultCount == 0);

    public List<ProfileSpecialRule> SpecialRules { get; set; } = new();
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
    ProfileEquipmentRaw[] Equipment,
    ProfileSpecialRuleRaw[] SpecialRules,
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
        value.Cost,
        value.Note);

    public HeroProfile Map(HeroProfileRaw raw) => new(
        _context.GetOrCreateArmyList(raw.ArmyList),
        raw.Name,
        Tier.GetTier(raw.Tier))
    {
        Keywords = raw.Keywords.ToList(),
        Characteristics = _mapper.CharacteristicsMapper.Map(raw.Characteristics),
        Equipment = raw.Equipment.Select(_mapper.ProfileEquipmentMapper.Map).ToList(),
        SpecialRules = raw.SpecialRules.Select(_mapper.ProfileSpecialRuleMapper.Map).ToList(),
        Cost = raw.Cost,
        Note = raw.Note
    };
}