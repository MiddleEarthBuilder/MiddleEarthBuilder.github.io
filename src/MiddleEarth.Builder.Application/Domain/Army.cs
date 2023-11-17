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
    WarbandRaw[]? Warbands);

public class ArmyMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public ArmyMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ArmyRaw Map(Army value) => new(
        value.Name,
        value.List.Name,
        value.Leader?.Profile.Name,
        value.Warbands.Any() ? value.Warbands.Select(_mapper.WarbandMapper.Map).ToArray() : null);

    public Army Map(ArmyRaw raw)
    {
        var army = new Army(raw.Name, _context.GetOrCreateArmyList(raw.ArmyList))
        {
            Warbands = raw.Warbands?.Select(_mapper.WarbandMapper.Map).ToList() ?? new List<Warband>()
        };
        army.Leader = army.PotentialLeaders.FirstOrDefault(hero => hero.Profile.Name == raw.Leader);
        return army;
    }
}