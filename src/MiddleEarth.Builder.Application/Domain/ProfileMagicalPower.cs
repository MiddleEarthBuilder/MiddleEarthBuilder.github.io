namespace MiddleEarth.Builder.Application.Domain;
public class ProfileMagicalPower
{
    public MagicalPower Power { get; set; }
    public int Casting { get; set; }
    public int Range { get; set; }

    public ProfileMagicalPower(MagicalPower power)
    {
        Power = power;
    }
}

public record ProfileMagicalPowerRaw(string Name, int Casting, int Range);

public class ProfileMagicalPowerMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public ProfileMagicalPowerMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ProfileMagicalPowerRaw Map(ProfileMagicalPower value) => new(value.Power.Name, value.Casting, value.Range);

    public ProfileMagicalPower Map(ProfileMagicalPowerRaw raw) => new(_context.GetOrCreateMagicalPower(raw.Name))
    {
        Casting = raw.Casting,
        Range = raw.Range
    };
}