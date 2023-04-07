namespace MiddleEarth.Models;

public class ArmyList
{
    public string Name { get; set; }
    public Side Side { get; set; }
    public List<Warrior> Heroes { get; set; } = new();
    public List<Warrior> Warriors { get; set; } = new();
    public List<SpecialRule> ArmyBonuses { get; set; } = new();
    public List<Alliance> Alliances { get; set; } = new();

    public ArmyList(string name)
    {
        Name = name;
    }
}