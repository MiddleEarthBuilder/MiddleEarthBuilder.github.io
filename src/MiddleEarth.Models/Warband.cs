namespace MiddleEarth.Models;

public class Warband
{
    public Warrior? Hero { get; set; }
    /// <summary>
    /// Dictionary of units and their count in a warband
    /// </summary>
    public List<Warrior> Followers { get; set; } = new();

    public int Points => Hero?.TotalCost ?? 0 + (Followers.Count > 0 ? Followers.Sum(warrior => warrior.TotalCost) : 0);
    public int UnitsCount => (Hero == null ? 0 : 1) + Followers.Sum(warrior => warrior.Count);
    public int BowsCount => Followers.Sum(warrior => warrior.HasBow ? warrior.Count : 0);
}