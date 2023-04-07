namespace MiddleEarth.Models;

public record ArmyList(
    string Name,
    Side Side,
    HeroProfile[] Heroes,
    WarriorProfile[] Warriors,
    SpecialRule[] ArmyBonuses,
    Alliance[] Alliances);

public class ArmyListDto
{
    public string Name { get; set; }
    public Side Side { get; set; }
    public List<ArmyUnitDto> Heroes { get; set; } = new();
    public List<ArmyUnitDto> Warriors { get; set; } = new();
    public List<SpecialRuleDto> ArmyBonuses { get; set; } = new();
    public List<AllianceDto> Alliances { get; set; } = new();

    public ArmyListDto(string name)
    {
        Name = name;
    }
}