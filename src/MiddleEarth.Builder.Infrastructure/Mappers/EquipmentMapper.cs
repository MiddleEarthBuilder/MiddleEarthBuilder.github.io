using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class EquipmentMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public EquipmentMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public EquipmentRaw Map(Equipment value) => 
        new(value.ProfileEquipment.Profile.Name, value.Count);
}