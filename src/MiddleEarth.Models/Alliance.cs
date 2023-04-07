using System.Text.Json.Serialization;

namespace MiddleEarth.Models;

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