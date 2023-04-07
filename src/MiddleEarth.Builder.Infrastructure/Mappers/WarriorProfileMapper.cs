using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class WarriorProfileMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public WarriorProfileMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public WarriorProfileRaw Map(Warrior value) => new(
        value.ArmyList.Name,
        value.Name,
        _mapper.CharacteristicsMapper.Map(value.Characteristics),
        value.Equipment.Select(_mapper.ProfileEquipmentMapper.Map).ToArray(),
        value.BaseCost,
        value.Note);

    public Warrior Map(WarriorProfileRaw storageValue) => new(
        _context.ArmyLists.GetOrCreate(storageValue.ArmyList),
        storageValue.Name,
        Tier.Warrior)
    {
        Characteristics = _mapper.CharacteristicsMapper.Map(storageValue.Characteristics),
        Equipment = storageValue.Equipment.Select(_mapper.ProfileEquipmentMapper.Map).ToList(),
        BaseCost = storageValue.Cost,
        Note = storageValue.Note
    };
}