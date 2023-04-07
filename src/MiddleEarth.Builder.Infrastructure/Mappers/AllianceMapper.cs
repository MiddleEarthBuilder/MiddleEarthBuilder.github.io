using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class AllianceMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public AllianceMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public AllianceRaw Map(Alliance value) => new(
        value.ArmyList.Name,
        value.Level);

    public Alliance Map(AllianceRaw storageValue) => new(
        _context.ArmyLists.GetOrCreate(storageValue.ArmyList),
        storageValue.Level);
}