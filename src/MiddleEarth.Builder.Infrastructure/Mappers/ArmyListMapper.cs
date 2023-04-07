using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class ArmyListMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public ArmyListMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Map(ArmyListRaw storageValue, ArmyList value)
    {
        value.ArmyBonuses = storageValue.ArmyBonuses.Select(_mapper.SpecialRuleMapper.Map).ToList();
        value.Heroes = storageValue.Heroes.Select(_mapper.HeroProfileMapper.Map).ToList();
        value.Warriors = storageValue.Warriors.Select(_mapper.WarriorProfileMapper.Map).ToList();
        value.Alliances = storageValue.Alliances.Select(_mapper.AllianceMapper.Map).ToList();
    }

    public ArmyListRaw Map(ArmyList value) => new(
        value.Name,
        value.Side,
        value.Heroes.Select(_mapper.HeroProfileMapper.MapHero).ToArray(),
        value.Warriors.Select(_mapper.WarriorProfileMapper.MapWarrior).ToArray(),
        value.ArmyBonuses.Select(_mapper.SpecialRuleMapper.Map).ToArray(),
        value.Alliances.Select(_mapper.AllianceMapper.Map).ToArray());
}