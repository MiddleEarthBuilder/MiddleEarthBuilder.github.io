namespace MiddleEarth.Builder.Application.Domain;

public class ProfileEquipment
{
    public EquipmentProfile Profile { get; set; }
    public int DefaultCount { get; set; }
    public int Cost { get; set; }
    public List<string> ReplacedEquipment { get; set; } = new();
    public string ReplacedEquipmentString
    {
        get => string.Join(", ", ReplacedEquipment);
        set => ReplacedEquipment = value.Split(",")
            .Select(s => s.Trim()).ToList();
    }

    public ProfileEquipment(EquipmentProfile profile)
    {
        Profile = profile;
    }
}

public record ProfileEquipmentRaw(
    string Name,
    int DefaultCount,
    int Cost,
    string[]? ReplacedEquipment = null);

public class ProfileEquipmentMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public ProfileEquipmentMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ProfileEquipmentRaw Map(ProfileEquipment value)
    {
        _context.GetOrCreateEquipment(value.Profile.Name).Update(value.Profile);
        return new ProfileEquipmentRaw(
            value.Profile.Name,
            value.DefaultCount,
            value.Cost,
            value.ReplacedEquipment.Any() ? value.ReplacedEquipment.ToArray() : null);
    }

    public ProfileEquipment Map(ProfileEquipmentRaw raw) => new(_context.GetOrCreateEquipment(raw.Name))
    {
        DefaultCount = raw.DefaultCount,
        Cost = raw.Cost,
        ReplacedEquipment = raw.ReplacedEquipment?.ToList() ?? new List<string>()
    };
}