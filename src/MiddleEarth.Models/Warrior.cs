namespace MiddleEarth.Models;

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