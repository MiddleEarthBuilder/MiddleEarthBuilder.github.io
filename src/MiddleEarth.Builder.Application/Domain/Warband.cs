namespace MiddleEarth.Builder.Application.Domain;

public class Warband
{
    public Hero? Hero { get; set; }
    /// <summary>
    /// Dictionary of units and their count in a warband
    /// </summary>
    public List<Warrior> Followers { get; set; } = new();

    public ArmyList? ArmyList => Hero?.Profile.ArmyList;
    public int Points => Hero?.Cost ?? 0 + Followers.Sum(warriors => warriors.TotalCost);
    public int UnitsCount => (Hero == null ? 0 : 1) + Followers.Sum(warrior => warrior.Count);
    public int BowsCount => Followers.Sum(warrior => warrior.HasBow ? warrior.Count : 0);
}