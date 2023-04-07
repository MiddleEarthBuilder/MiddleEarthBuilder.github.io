using System.Text.Json.Serialization;

namespace MiddleEarth.Models;

public record Alliance(
    string ArmyList,
    AllianceLevel Level);

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AllianceLevel
{
    Impossible, Convenient, Historical
}

public class AllianceDto
{
    public ArmyListDto ArmyList { get; set; }
    public AllianceLevel Level { get; set; }

    public AllianceDto(ArmyListDto armyList, AllianceLevel level)
    {
        ArmyList = armyList;
        Level = level;
    }
}