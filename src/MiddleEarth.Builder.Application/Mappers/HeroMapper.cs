using MiddleEarth.Builder.Application.Domain;
using MiddleEarth.Builder.Application.Entities;

namespace MiddleEarth.Builder.Application.Mappers;

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