using MiddleEarth.Builder.Application.Domain;
using MiddleEarth.Builder.Application.Entities;

namespace MiddleEarth.Builder.Application.Mappers;

public class CharacteristicsMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public CharacteristicsMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public CharacteristicsRaw Map(Characteristics value) => new(
        value.Move!.Value,
        value.Fight!.Value,
        value.Shoot,
        value.Strength!.Value,
        value.Defense!.Value,
        value.Attacks!.Value,
        value.Wounds!.Value,
        value.Courage!.Value,
        value.Might,
        value.Will,
        value.Fate);

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
        Fate = raw.Fate
    };
}