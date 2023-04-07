namespace MiddleEarth.Models;

/// <summary>
/// An army with its leader and warbands
/// </summary>
/// <param name="Leader">A name of the hero with highest tier in the army</param>
/// <param name="Warbands">Warbands' details</param>
public record Army(
    string Name,
    string Leader,
    Warband[] Warbands);

public class ArmyDto
{
    public ArmyListDto List { get; set; }
    public ArmyUnitDto? Leader { get; set; }
    public List<WarbandDto> Warbands { get; set; } = new();

    public int PointsCount => Warbands.Count > 0 ? Warbands.Sum(warband => warband.Points) : 0;
    public int UnitsCount => Warbands.Count > 0 ? Warbands.Sum(warband => warband.UnitsCount) : 0;
    public int BrokenCount => UnitsCount / 2;
    public int QuarterCount => UnitsCount / 4;
    public int BowsCount => Warbands.Count > 0 ? Warbands.Sum(warband => warband.BowsCount) : 0;
    public List<ArmyUnitDto> PotentialLeaders
    {
        get
        {
            var maxTier = Warbands.Max(dto => dto.Hero.Tier);
            return Warbands.Select(warband => warband.Hero).Where(hero => hero.Tier == maxTier).ToList();
        }
    }

    public ArmyDto(ArmyListDto list)
    {
        List = list;
    }
}