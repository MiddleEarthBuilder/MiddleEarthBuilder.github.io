namespace MiddleEarth.Builder.Infrastructure.Entities;

/// <summary>
/// An army with its leader and warbands
/// </summary>
/// <param name="Leader">A name of the hero with highest tier in the army</param>
/// <param name="Warbands">Warbands' details</param>
public record Army(
    string Name,
    string Leader,
    Warband[] Warbands);