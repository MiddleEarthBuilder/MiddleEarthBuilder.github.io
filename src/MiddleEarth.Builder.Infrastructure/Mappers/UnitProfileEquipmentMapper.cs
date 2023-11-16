using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class ProfileEquipmentMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public ProfileEquipmentMapper(Context context, Mapper mapper)
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

    public ProfileEquipment Map(ProfileEquipmentRaw raw) => new(_context.GetOrCreateEquipment(raw.Name))
    {
        DefaultCount = raw.DefaultCount,
        Cost = raw.Cost,
        IsAllowedOnce = raw.IsAllowedOnce,
        ReplacedEquipment = raw.ReplacedEquipment.ToList()
    };
}