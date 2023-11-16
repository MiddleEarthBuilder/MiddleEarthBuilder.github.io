using MiddleEarth.Builder.Application.Domain;
using MiddleEarth.Builder.Application.Entities;

namespace MiddleEarth.Builder.Application.Mappers;

public class ProfileSpecialRuleMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public ProfileSpecialRuleMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ProfileSpecialRuleRaw Map(ProfileSpecialRule value) => new(
        value.Rule.Name!,
        value.Target,
        value.Rule.Description!);

    public ProfileSpecialRule Map(ProfileSpecialRuleRaw raw) => new(_context.GetOrCreateSpecialRule(raw.Name))
    {
        Target = raw.Target
    };
}