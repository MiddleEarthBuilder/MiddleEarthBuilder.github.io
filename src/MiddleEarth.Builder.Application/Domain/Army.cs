namespace MiddleEarth.Builder.Application.Domain;

public class Army
{
    public string Name { get; set; }
    public ArmyList List { get; set; }
    public Hero? Leader { get; set; }
    public List<Warband> Warbands { get; set; } = new();

    public int PointsCount => Warbands.Count > 0 ? Warbands.Sum(warband => warband.Points) : 0;
    public int UnitsCount => Warbands.Count > 0 ? Warbands.Sum(warband => warband.UnitsCount) : 0;
    public int BrokenCount => UnitsCount / 2;
    public int QuarterCount => UnitsCount / 4;
    public int BowsCount => Warbands.Count > 0 ? Warbands.Sum(warband => warband.BowsCount) : 0;
    public IEnumerable<Hero> Heroes => Warbands.Select(warband => warband.Hero).OfType<Hero>();
    public IEnumerable<Hero> PotentialLeaders
    {
        get
        {
            if (!Heroes.Any())
                return new List<Hero>();

            var maxTier = Heroes.Max(hero => hero.Profile.Tier);
            return Heroes.Where(hero => hero.Profile.Tier == maxTier);
        }
    }

    public Army(string name, ArmyList list)
    {
        Name = name;
        List = list;
    }
}