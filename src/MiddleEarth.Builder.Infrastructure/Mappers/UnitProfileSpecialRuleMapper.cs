using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class ProfileSpecialRuleMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public ProfileSpecialRuleMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ProfileSpecialRuleRaw Map(ProfileSpecialRule value) => new(
        value.Rule.Name,
        value.Target);

    public ProfileSpecialRule Map(ProfileSpecialRuleRaw raw) => new(_context.SpecialRules.GetOrCreate(raw.Name))
    {
        Target = raw.Target
    };
}