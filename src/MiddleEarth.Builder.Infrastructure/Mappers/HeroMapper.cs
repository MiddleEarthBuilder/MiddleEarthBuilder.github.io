using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class HeroMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public HeroMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Warrior? Map(HeroRaw storageValue)
    {
        var armyList = _context.ArmyLists.GetOrCreate(storageValue.ArmyList);
        var hero = armyList.Heroes.FirstOrDefault(hero => hero.Name == storageValue.Name);
        return hero == null ?
            null :
            new Warrior(hero);
    }
}