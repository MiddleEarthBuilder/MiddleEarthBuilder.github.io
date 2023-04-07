using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class ProfileEquipmentMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public ProfileEquipmentMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ProfileEquipmentRaw Map(ProfileEquipment value) => new(
        value.Profile.Name,
        value.DefaultCount,
        value.Cost,
        value.IsAllowedOnce,
        value.ReplacedEquipment.ToArray());

    public ProfileEquipment Map(ProfileEquipmentRaw raw)
    {
        var equipment = _context.Equipments.GetOrCreate(raw.Name);

        return new ProfileEquipment(equipment)
        {
            DefaultCount = raw.DefaultCount,
            Cost = raw.Cost,
            IsAllowedOnce = raw.IsAllowedOnce,
            ReplacedEquipment = raw.ReplacedEquipment.ToList()
        };
    }
}