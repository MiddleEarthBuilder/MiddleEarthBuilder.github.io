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

    public WarriorProfileRaw Map(WarriorProfile value) => new(
        value.ArmyList.Name,
        value.Name,
        _mapper.CharacteristicsMapper.Map(value.Characteristics),
        value.Equipment.Select(_mapper.UnitProfileEquipmentMapper.Map).ToArray(),
        value.Cost,
        value.Note);

    public WarriorProfile Map(WarriorProfileRaw raw) => new(
        _context.ArmyLists.GetOrCreate(raw.ArmyList),
        raw.Name)
    {
        Characteristics = _mapper.CharacteristicsMapper.Map(raw.Characteristics),
        Equipment = raw.Equipment.Select(_mapper.UnitProfileEquipmentMapper.Map).ToList(),
        Cost = raw.Cost,
        Note = raw.Note
    };
}