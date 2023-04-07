namespace MiddleEarth.Models;

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