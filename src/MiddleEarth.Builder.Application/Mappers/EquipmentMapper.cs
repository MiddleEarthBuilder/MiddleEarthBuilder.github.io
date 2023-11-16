using MiddleEarth.Builder.Application.Domain;
using MiddleEarth.Builder.Application.Entities;

namespace MiddleEarth.Builder.Application.Mappers;

public class EquipmentMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public EquipmentMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public EquipmentRaw Map(Equipment value) =>
        new(value.ProfileEquipment.Profile.Name, value.Count);
}