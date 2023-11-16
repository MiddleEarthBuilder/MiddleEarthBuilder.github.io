using MiddleEarth.Builder.Application.Domain;
using MiddleEarth.Builder.Application.Entities;

namespace MiddleEarth.Builder.Application.Mappers;

public class EquipmentMapper
{
    private readonly Context _context;

    public EquipmentMapper(Context context)
    {
        _context = context;
    }

    public EquipmentRaw Map(Equipment value) =>
        new(value.ProfileEquipment.Profile.Name, value.Count);

    public Equipment Map(EquipmentRaw value) => throw new NotImplementedException();
}