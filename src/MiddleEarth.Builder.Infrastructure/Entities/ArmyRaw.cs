namespace MiddleEarth.Builder.Infrastructure.Entities;

/// <summary>
/// An army with its leader and warbands
/// </summary>
/// <param name="Name">A name of the army</param>
/// <param name="ArmyList">A name of the army list</param>
/// <param name="Leader">A name of the hero with highest tier in the army</param>
/// <param name="Warbands">Warbands' details</param>
public record ArmyRaw(
    string Name,
    string ArmyList,
    string? Leader,
    WarbandRaw[] Warbands);