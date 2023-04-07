using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class ArmyMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public ArmyMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ArmyRaw Map(Army value) => new(
        value.Leader?.Profile.Name,
        value.Warbands.Select(_mapper.WarbandMapper.Map).ToArray());

    public void Map(ArmyRaw raw, Army value)
    {
        value.Warbands = raw.Warbands.Select(_mapper.WarbandMapper.Map).ToList();
        value.Leader = raw.Leader == null ? null :
            value.PotentialLeaders.FirstOrDefault(hero => hero.Profile.Name == raw.Leader);
    }
}