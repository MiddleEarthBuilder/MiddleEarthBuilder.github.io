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

    public Characteristics Map(CharacteristicsRaw storageValue) => new()
    {
        Move = storageValue.Move,
        Fight = storageValue.Fight,
        Shoot = storageValue.Shoot,
        Strength = storageValue.Strength,
        Defense = storageValue.Defense,
        Attacks = storageValue.Attacks,
        Wounds = storageValue.Wounds,
        Courage = storageValue.Courage,
        Might = storageValue.Might,
        Will = storageValue.Will,
        Fate = storageValue.Fate,
        SpecialRules = storageValue.SpecialRules
            .Select(_context.SpecialRules.GetOrCreate)
            .ToList()
    };
}