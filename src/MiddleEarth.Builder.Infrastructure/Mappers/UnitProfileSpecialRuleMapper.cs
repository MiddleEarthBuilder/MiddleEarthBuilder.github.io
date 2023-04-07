using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class UnitProfileSpecialRuleMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public UnitProfileSpecialRuleMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public UnitProfileSpecialRuleRaw Map(UnitProfileSpecialRule value) => new(
        value.Rule.Name,
        value.Target);

    public UnitProfileSpecialRule Map(UnitProfileSpecialRuleRaw raw) => new(_context.SpecialRules.GetOrCreate(raw.Name))
    {
        Target = raw.Target
    };
}