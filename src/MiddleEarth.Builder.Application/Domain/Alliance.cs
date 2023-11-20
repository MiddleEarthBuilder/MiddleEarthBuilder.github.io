using System.Text.Json.Serialization;

namespace MiddleEarth.Builder.Application.Domain;

public class Alliance
{
    public ArmyList ArmyList { get; set; }
    public AllianceLevel Level { get; set; }

    public Alliance(ArmyList armyList, AllianceLevel level)
    {
        ArmyList = armyList;
        Level = level;
    }
}

public record AllianceRaw(
    string ArmyList,
    [property: JsonConverter(typeof(JsonStringEnumConverter))] AllianceLevel Level);

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AllianceLevel
{
    Impossible, Convenient, Historical
}

public class AllianceMapper
{
    private readonly Context _context;

    public AllianceMapper(Context context)
    {
        _context = context;
    }

    public AllianceRaw Map(Alliance value) => new(
        value.ArmyList.Name,
        value.Level);

    public Alliance Map(AllianceRaw raw) => new(
        _context.ArmyLists.GetOrCreate(raw.ArmyList),
        raw.Level);
}