using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class SpecialRuleMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public SpecialRuleMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public SpecialRule Map(SpecialRuleRaw storageValue) => new(storageValue.Name)
    {
        Description = storageValue.Description,
        Target = storageValue.Target
    };

    public SpecialRuleRaw Map(SpecialRule value) => new(
        value.Name,
        value.Target,
        value.Description);
}