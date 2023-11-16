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

    public ProfileSpecialRuleRaw Map(ProfileSpecialRule value)
    {
        _context.GetOrCreateSpecialRule(value.Rule.Name!).Update(value.Rule);
        return new ProfileSpecialRuleRaw(
            value.Rule.Name!,
            value.Target);
    }

    public ProfileSpecialRule Map(ProfileSpecialRuleRaw raw) => new(_context.GetOrCreateSpecialRule(raw.Name))
    {
        Target = raw.Target
    };
}