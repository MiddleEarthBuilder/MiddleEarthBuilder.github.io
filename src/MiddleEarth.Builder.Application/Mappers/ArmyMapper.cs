using MiddleEarth.Builder.Application.Domain;
using MiddleEarth.Builder.Application.Entities;

namespace MiddleEarth.Builder.Application.Mappers;

public class ArmyMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public ArmyMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ArmyRaw Map(Army value) => new(
        value.Name,
        value.List.Name,
        value.Leader?.Profile.Name,
        value.Warbands.Select(_mapper.WarbandMapper.Map).ToArray());

    public Army Map(ArmyRaw raw)
    {
        var army = new Army(raw.Name, _context.GetOrCreateArmyList(raw.Name))
        {
            Warbands = raw.Warbands.Select(_mapper.WarbandMapper.Map).ToList()
        };
        army.Leader = army.PotentialLeaders.FirstOrDefault(hero => hero.Profile.Name == raw.Leader);
        return army;
    }
}