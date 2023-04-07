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

    public ArmyListRaw Map(ArmyList value) => new(
        value.Name,
        value.Side,
        value.Heroes.Select(_mapper.HeroProfileMapper.Map).ToArray(),
        value.Warriors.Select(_mapper.WarriorProfileMapper.Map).ToArray(),
        value.ArmyBonuses.Select(_mapper.SpecialRuleMapper.Map).ToArray(),
        value.Alliances.Select(_mapper.AllianceMapper.Map).ToArray());

    public void Map(ArmyListRaw raw, ArmyList value)
    {
        value.ArmyBonuses = raw.ArmyBonuses.Select(_mapper.SpecialRuleMapper.Map).ToList();
        value.Heroes = raw.Heroes.Select(_mapper.HeroProfileMapper.Map).ToList();
        value.Warriors = raw.Warriors.Select(_mapper.WarriorProfileMapper.Map).ToList();
        value.Alliances = raw.Alliances.Select(_mapper.AllianceMapper.Map).ToList();
    }
}