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

    public Warrior? Map(WarriorRaw storageValue)
    {
        var armyList = _context.ArmyLists.GetOrCreate(storageValue.ArmyList);
        var warrior = armyList.Warriors.FirstOrDefault(warrior => warrior.Name == storageValue.Name);
        if (warrior != null)
            return new Warrior(warrior);
        var hero = armyList.Heroes.FirstOrDefault(hero => hero.Name == storageValue.Name);
        return hero == null ?
            null :
            new Warrior(hero);
    }
}