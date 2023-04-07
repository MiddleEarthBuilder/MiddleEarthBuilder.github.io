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

    public void Map(ArmyRaw storageValue, Army value)
    {
        value.Warbands = storageValue.Warbands.Select(_mapper.WarbandMapper.Map).ToList();
        value.Leader = value.PotentialLeaders.FirstOrDefault(hero => hero.Name == storageValue.Name);
    }
}