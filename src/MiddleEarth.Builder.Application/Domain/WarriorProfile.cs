namespace MiddleEarth.Builder.Application.Domain;

public class WarriorProfile
{
    public ArmyList ArmyList { get; set; }
    public string Name { get; set; }
    public string KeywordsString
    {
        get => string.Join(", ", Keywords);
        set => Keywords = value.Split(",")
            .Select(s => s.Trim()).ToList();
    }
    public List<string> Keywords { get; set; } = new();
    public Characteristics Characteristics { get; set; } = new();
    public List<ProfileEquipment> Equipment { get; set; } = new();
    public List<ProfileSpecialRule> SpecialRules { get; set; } = new();
    public int Cost { get; set; }
    public string? Note { get; set; }

    public WarriorProfile(ArmyList armyList, string name)
    {
        ArmyList = armyList;
        Name = name;
    }
}

public record WarriorProfileRaw(
    string ArmyList,
    string Name,
    string[] Keywords,
    CharacteristicsRaw Characteristics,
    ProfileEquipmentRaw[]? Equipment,
    ProfileSpecialRuleRaw[]? SpecialRules,
    int Cost,
    string? Note);

public class WarriorProfileMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public WarriorProfileMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public WarriorProfileRaw Map(WarriorProfile value) => new(
        value.ArmyList.Name,
        value.Name,
        value.Keywords.ToArray(),
        _mapper.CharacteristicsMapper.Map(value.Characteristics),
        value.Equipment.Any() ? value.Equipment.Select(_mapper.ProfileEquipmentMapper.Map).ToArray() : null,
        value.SpecialRules.Any() ? value.SpecialRules.Select(_mapper.ProfileSpecialRuleMapper.Map).ToArray() : null,
        value.Cost,
        value.Note);

    public WarriorProfile Map(WarriorProfileRaw raw) => new(
        _context.ArmyLists.GetOrCreate(raw.ArmyList),
        raw.Name)
    {
        Keywords = raw.Keywords.ToList(),
        Characteristics = _mapper.CharacteristicsMapper.Map(raw.Characteristics),
        Equipment = raw.Equipment?.Select(_mapper.ProfileEquipmentMapper.Map).ToList() ?? new List<ProfileEquipment>(),
        SpecialRules = raw.SpecialRules?.Select(_mapper.ProfileSpecialRuleMapper.Map).ToList() ?? new List<ProfileSpecialRule>(),
        Cost = raw.Cost,
        Note = raw.Note
    };
}