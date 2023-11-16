using MiddleEarth.Builder.Application.Domain;
using MiddleEarth.Builder.Application.Entities;

namespace MiddleEarth.Builder.Application.Mappers;

public class ProfileEquipmentMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public ProfileEquipmentMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ProfileEquipmentRaw Map(ProfileEquipment value)
    {
        _context.GetOrCreateEquipment(value.Profile.Name).Update(value.Profile);
        return new ProfileEquipmentRaw(
            value.Profile.Name,
            value.DefaultCount,
            value.Cost,
            value.IsAllowedOnce,
            value.ReplacedEquipment.ToArray());
    }

    public ProfileEquipment Map(ProfileEquipmentRaw raw) => new(_context.GetOrCreateEquipment(raw.Name))
    {
        DefaultCount = raw.DefaultCount,
        Cost = raw.Cost,
        IsAllowedOnce = raw.IsAllowedOnce,
        ReplacedEquipment = raw.ReplacedEquipment.ToList()
    };
}