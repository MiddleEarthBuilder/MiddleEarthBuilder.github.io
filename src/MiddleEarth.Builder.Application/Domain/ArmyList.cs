using System.Text.Json.Serialization;

namespace MiddleEarth.Builder.Application.Domain;

public class ArmyList
{
    public string Name { get; set; }
    public Side Side { get; set; }
    public List<HeroProfile> Heroes { get; set; } = new();
    public List<WarriorProfile> Warriors { get; set; } = new();
    public List<ProfileSpecialRule> ArmyBonuses { get; set; } = new();
    public List<Alliance> Alliances { get; set; } = new();

    public ArmyList(string name)
    {
        Name = name;
    }

    public void Update(ArmyList value)
    {
        Side = value.Side;
        Heroes = value.Heroes;
        Warriors = value.Warriors;
        ArmyBonuses = value.ArmyBonuses;
        Alliances = value.Alliances;
    }
}

public record ArmyListRaw(
    string Name,
    [property: JsonConverter(typeof(JsonStringEnumConverter))] Side Side,
    HeroProfileRaw[] Heroes,
    WarriorProfileRaw[] Warriors,
    ProfileSpecialRuleRaw[] ArmyBonuses,
    AllianceRaw[] Alliances);

public class ArmyListMapper
{
    private readonly Mapper _mapper;

    public ArmyListMapper(Mapper mapper)
    {
        _mapper = mapper;
    }

    public ArmyListRaw Map(ArmyList value) => new(
        value.Name,
        value.Side,
        value.Heroes.Select(_mapper.HeroProfileMapper.Map).ToArray(),
        value.Warriors.Select(_mapper.WarriorProfileMapper.Map).ToArray(),
        value.ArmyBonuses.Select(_mapper.ProfileSpecialRuleMapper.Map).ToArray(),
        value.Alliances.Select(_mapper.AllianceMapper.Map).ToArray()); // TODO: Skip until all army lists are loaded

    public ArmyList Map(ArmyListRaw raw) => new(raw.Name)
    {
        Side = raw.Side,
        ArmyBonuses = raw.ArmyBonuses.Select(_mapper.ProfileSpecialRuleMapper.Map).ToList(),
        Heroes = raw.Heroes.Select(_mapper.HeroProfileMapper.Map).ToList(),
        Warriors = raw.Warriors.Select(_mapper.WarriorProfileMapper.Map).ToList(),
        Alliances = raw.Alliances.Select(_mapper.AllianceMapper.Map).ToList()
    };
}