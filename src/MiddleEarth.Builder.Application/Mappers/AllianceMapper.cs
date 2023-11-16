using MiddleEarth.Builder.Application.Domain;
using MiddleEarth.Builder.Application.Entities;

namespace MiddleEarth.Builder.Application.Mappers;

public class AllianceMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public AllianceMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public AllianceRaw Map(Alliance value) => new(
        value.ArmyList.Name,
        value.Level);

    public Alliance Map(AllianceRaw raw) => new(
        _context.GetOrCreateArmyList(raw.ArmyList),
        raw.Level);
}