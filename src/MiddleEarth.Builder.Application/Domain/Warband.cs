namespace MiddleEarth.Builder.Application.Domain;

public class Warband
{
    public Hero? Hero { get; set; }
    /// <summary>
    /// Dictionary of units and their count in a warband
    /// </summary>
    public List<Warrior> Followers { get; set; } = new();

    public ArmyList? ArmyList => Hero?.Profile.ArmyList;
    public int Points => Hero?.Cost ?? 0 + Followers.Sum(warriors => warriors.TotalCost);
    public int UnitsCount => (Hero == null ? 0 : 1) + Followers.Sum(warrior => warrior.Count);
    public int BowsCount => Followers.Sum(warrior => warrior.HasBow ? warrior.Count : 0);
}

/// <summary>
/// Warbands details
/// </summary>
/// <param name="ArmyList">Name of a hero's army list</param>
/// <param name="Hero">Warband's hero details</param>
/// <param name="Followers">Hero's followers.</param>
public record WarbandRaw(
    HeroRaw? Hero,
    WarriorRaw[] Followers);

public class WarbandMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public WarbandMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public WarbandRaw Map(Warband value) => new(
        _mapper.HeroMapper.Map(value.Hero),
        value.Followers.Select(_mapper.WarriorMapper.Map).ToArray());

    public Warband Map(WarbandRaw raw) => new()
    {
        Hero = raw.Hero == null ? null :
            _mapper.HeroMapper.Map(raw.Hero),
        Followers = raw.Followers.Select(_mapper.WarriorMapper.Map).OfType<Warrior>().ToList()
    };
}