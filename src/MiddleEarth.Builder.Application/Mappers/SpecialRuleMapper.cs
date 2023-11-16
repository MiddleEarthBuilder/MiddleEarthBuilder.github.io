using MiddleEarth.Builder.Application.Domain;
using MiddleEarth.Builder.Application.Entities;

namespace MiddleEarth.Builder.Application.Mappers;

public class SpecialRuleMapper
{
    public SpecialRuleRaw Map(SpecialRule value) => new(
        value.Name!,
        value.Description!);

    public SpecialRule Map(SpecialRuleRaw raw) => new(raw.Name)
    {
        Description = raw.Description
    };
}