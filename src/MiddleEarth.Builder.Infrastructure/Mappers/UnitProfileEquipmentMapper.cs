using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class UnitProfileEquipmentMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public UnitProfileEquipmentMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public UnitProfileEquipmentRaw Map(UnitProfileEquipment value) => new(
        value.Profile.Name,
        value.DefaultCount,
        value.Cost,
        value.IsAllowedOnce,
        value.ReplacedEquipment.ToArray());

    public UnitProfileEquipment Map(UnitProfileEquipmentRaw raw)
    {
        var equipment = _context.Equipments.GetOrCreate(raw.Name);

        return new UnitProfileEquipment(equipment)
        {
            DefaultCount = raw.DefaultCount,
            Cost = raw.Cost,
            IsAllowedOnce = raw.IsAllowedOnce,
            ReplacedEquipment = raw.ReplacedEquipment.ToList()
        };
    }
}