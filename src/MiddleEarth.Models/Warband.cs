namespace MiddleEarth.Models;

/// <summary>
/// Warbands details
/// </summary>
/// <param name="ArmyList">Name of a hero's army list</param>
/// <param name="Hero">Warband's hero details</param>
/// <param name="Followers">Hero's followers.</param>
public record Warband(
    string ArmyList,
    Hero? Hero,
    Warrior[] Followers);

public class WarbandDto
{
    public ArmyUnitDto? Hero { get; set; }
    /// <summary>
    /// Dictionary of units and their count in a warband
    /// </summary>
    public List<ArmyUnitDto> Followers { get; set; } = new();

    public int Points => Hero?.TotalCost ?? 0 + (Followers.Count > 0 ? Followers.Sum(warrior => warrior.TotalCost) : 0);
    public int UnitsCount => (Hero == null ? 0 : 1) + Followers.Sum(warrior => warrior.Count);
    public int BowsCount => Followers.Sum(warrior => warrior.HasBow ? warrior.Count : 0);
}