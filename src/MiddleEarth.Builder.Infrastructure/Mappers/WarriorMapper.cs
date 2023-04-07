using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class WarriorMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public WarriorMapper(BuilderContext context, Mapper mapper)
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
        var armyList = _context.ArmyLists.GetOrCreate(raw.ArmyList);
        var warrior = armyList.Warriors.FirstOrDefault(warrior => warrior.Name == raw.Name);
        return warrior == null ?
            null :
            new Warrior(warrior);
    }
}