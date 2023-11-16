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