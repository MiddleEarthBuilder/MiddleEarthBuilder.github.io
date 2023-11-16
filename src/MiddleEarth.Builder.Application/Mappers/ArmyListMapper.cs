using MiddleEarth.Builder.Application.Domain;
using MiddleEarth.Builder.Application.Entities;

namespace MiddleEarth.Builder.Application.Mappers;

public class ArmyListMapper
{
    private readonly Mapper _mapper;

    public ArmyListMapper(Mapper mapper)
    {
        _mapper = mapper;
    }

    public ArmyListRaw Map(ArmyList value) => new(
        value.Name,
        value.Side,
        value.Heroes.Select(_mapper.HeroProfileMapper.Map).ToArray(),
        value.Warriors.Select(_mapper.WarriorProfileMapper.Map).ToArray(),
        value.ArmyBonuses.Select(_mapper.ProfileSpecialRuleMapper.Map).ToArray(),
        value.Alliances.Select(_mapper.AllianceMapper.Map).ToArray());

    public ArmyList Map(ArmyListRaw raw) => new(raw.Name)
    {
        Side = raw.Side,
        ArmyBonuses = raw.ArmyBonuses.Select(_mapper.ProfileSpecialRuleMapper.Map).ToList(),
        Heroes = raw.Heroes.Select(_mapper.HeroProfileMapper.Map).ToList(),
        Warriors = raw.Warriors.Select(_mapper.WarriorProfileMapper.Map).ToList(),
        Alliances = raw.Alliances.Select(_mapper.AllianceMapper.Map).ToList()
    };
}