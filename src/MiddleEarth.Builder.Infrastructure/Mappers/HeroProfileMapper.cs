using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class HeroProfileMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public HeroProfileMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public HeroProfileRaw Map(HeroProfile value)
    {
        throw new NotImplementedException();
    }

    public HeroProfile Map(HeroProfileRaw raw) => new(
        _context.ArmyLists.GetOrCreate(raw.ArmyList),
        raw.Name,
        Tier.GetTier(raw.Tier))
    {
        Characteristics = _mapper.CharacteristicsMapper.Map(raw.Characteristics),
        Equipment = raw.Equipment.Select(_mapper.UnitProfileEquipmentMapper.Map).ToList(),
        Cost = raw.Cost,
        Note = raw.Note
    };
}