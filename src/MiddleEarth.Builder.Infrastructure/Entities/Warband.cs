namespace MiddleEarth.Builder.Infrastructure.Entities;

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