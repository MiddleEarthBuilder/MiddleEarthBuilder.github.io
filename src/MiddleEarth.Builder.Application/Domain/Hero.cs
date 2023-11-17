namespace MiddleEarth.Builder.Application.Domain;

public class Hero
{
    public HeroProfile Profile { get; set; }
    public List<Equipment> Equipment { get; set; }

    public int Cost => Profile.Cost +
                       Equipment.Sum(equipment => equipment.Count * equipment.ProfileEquipment.Cost);

    public Hero(HeroProfile profile)
    {
        Profile = profile;
        Equipment = profile.Equipment
            .Select(equipment => new Equipment(equipment))
            .ToList();
    }
}

public record HeroRaw(
    string ArmyList,
    string Name,
    EquipmentRaw[] Equipment);

public class HeroMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public HeroMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public HeroRaw Map(Hero value) => new(
        value.Profile.ArmyList.Name,
        value.Profile.Name,
        value.Equipment.Select(_mapper.EquipmentMapper.Map).ToArray());

    public Hero? Map(HeroRaw raw)
    {
        var armyList = _context.GetOrCreateArmyList(raw.ArmyList);
        var hero = armyList.Heroes.FirstOrDefault(hero => hero.Name == raw.Name);
        return hero == null ?
            null :
            new Hero(hero);
    }
}