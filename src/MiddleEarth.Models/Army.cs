namespace MiddleEarth.Models;

/// <summary>
/// An army with its leader and warbands
/// </summary>
/// <param name="Leader">A name of the hero with highest tier in the army</param>
/// <param name="Warbands">Warbands' details</param>
public record Army(
    string Leader,
    Warband[] Warbands);

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

public record Hero(
    string ArmyList,
    string Name,
    string[] Equipment);

/// <summary>
/// Army unit details
/// </summary>
/// <param name="Count">Followers count</param>
/// <param name="ArmyList">Name of a unit's army list</param>
/// <param name="Name">A unit name</param>
/// <param name="Equipment">A single unit equipment</param>
public record Warrior(
    int Count,
    string ArmyList,
    string Name,
    string[] Equipment);