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

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Side
{
    Undefined, Good, Evil
}

public record ArmyListRaw(
    string Name,
    [property: JsonConverter(typeof(JsonStringEnumConverter))] Side Side,
    HeroProfileRaw[]? Heroes,
    WarriorProfileRaw[]? Warriors,
    ProfileSpecialRuleRaw[]? ArmyBonuses,
    AllianceRaw[]? Alliances);

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
        value.Heroes.Any() ? value.Heroes.Select(_mapper.HeroProfileMapper.Map).ToArray() : null,
        value.Warriors.Any() ? value.Warriors.Select(_mapper.WarriorProfileMapper.Map).ToArray() : null,
        value.ArmyBonuses.Any() ? value.ArmyBonuses.Select(_mapper.ProfileSpecialRuleMapper.Map).ToArray() : null,
        value.Alliances.Any() ? value.Alliances.Select(_mapper.AllianceMapper.Map).ToArray() : null);

    public ArmyList Map(ArmyListRaw raw) => new(raw.Name)
    {
        Side = raw.Side,
        ArmyBonuses = raw.ArmyBonuses?.Select(_mapper.ProfileSpecialRuleMapper.Map).ToList() ?? new List<ProfileSpecialRule>(),
        Heroes = raw.Heroes?.Select(_mapper.HeroProfileMapper.Map).ToList() ?? new List<HeroProfile>(),
        Warriors = raw.Warriors?.Select(_mapper.WarriorProfileMapper.Map).ToList() ?? new List<WarriorProfile>(),
        Alliances = raw.Alliances?.Select(_mapper.AllianceMapper.Map).ToList() ?? new List<Alliance>() // TODO: Skip until all army lists are loaded
    };
}