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

    public HeroProfileRaw MapHero(Warrior value)
    {
        throw new NotImplementedException();
    }

    public Warrior Map(HeroProfileRaw storageValue) => new(
        _context.ArmyLists.GetOrCreate(storageValue.ArmyList),
        storageValue.Name,
        Tier.GetTier(storageValue.Tier))
    {
        Characteristics = _mapper.CharacteristicsMapper.Map(storageValue.Characteristics),
        Equipment = storageValue.Equipment.Select(_mapper.ProfileEquipmentMapper.Map).ToList(),
        BaseCost = storageValue.Cost,
        Note = storageValue.Note
    };
}