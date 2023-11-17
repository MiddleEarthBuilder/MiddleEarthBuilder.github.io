namespace MiddleEarth.Builder.Application.Domain;

public class Warrior
{
    public WarriorProfile Profile { get; set; }
    public List<Equipment> Equipment { get; set; }
    public int Count { get; set; } = 1;

    public int UnitCost => Profile.Cost +
                           Equipment.Sum(equipment => equipment.Count * equipment.ProfileEquipment.Cost);
    public int TotalCost => Count * UnitCost;
    public bool HasBow => Equipment.Any(equipment => equipment.Count > 0 && equipment.ProfileEquipment.Profile.IsBow);

    public Warrior(WarriorProfile profile)
    {
        Profile = profile;
        Equipment = profile.Equipment
            .Select(equipment => new Equipment(equipment))
            .ToList();
    }
}

public record WarriorRaw(
    string ArmyList,
    string Name,
    EquipmentRaw[] Equipment,
    int Count);

public class WarriorMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public WarriorMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public WarriorRaw Map(Warrior value) => new(
        value.Profile.ArmyList.Name,
        value.Profile.Name,
        value.Equipment.Select(_mapper.EquipmentMapper.Map).ToArray(),
        value.Count);

    public Warrior? Map(WarriorRaw raw)
    {
        var armyList = _context.GetOrCreateArmyList(raw.ArmyList);
        var warrior = armyList.Warriors.FirstOrDefault(warrior => warrior.Name == raw.Name);
        return warrior == null ?
            null :
            new Warrior(warrior);
    }
}