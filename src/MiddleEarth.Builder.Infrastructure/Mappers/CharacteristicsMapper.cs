using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class CharacteristicsMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public CharacteristicsMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public CharacteristicsRaw Map(Characteristics value) => new(
        value.Move,
        value.Fight,
        value.Shoot,
        value.Strength,
        value.Defense,
        value.Attacks,
        value.Wounds,
        value.Courage,
        value.Might,
        value.Will,
        value.Fate,
        value.SpecialRules.Select(rule => rule.Name).ToArray());

    public Characteristics Map(CharacteristicsRaw raw) => new()
    {
        Move = raw.Move,
        Fight = raw.Fight,
        Shoot = raw.Shoot,
        Strength = raw.Strength,
        Defense = raw.Defense,
        Attacks = raw.Attacks,
        Wounds = raw.Wounds,
        Courage = raw.Courage,
        Might = raw.Might,
        Will = raw.Will,
        Fate = raw.Fate,
        SpecialRules = raw.SpecialRules
            .Select(_context.SpecialRules.GetOrCreate)
            .ToList()
    };
}