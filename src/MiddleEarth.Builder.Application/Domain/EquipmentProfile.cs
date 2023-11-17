namespace MiddleEarth.Builder.Application.Domain;

public class EquipmentProfile
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public Characteristics? CharacteristicsBonus { get; set; }
    public bool IsBow { get; set; }
    public bool IsMultiple { get; set; }
    public List<string> DeniedEquipment { get; set; } = new();
    public string DeniedEquipmentString
    {
        get => string.Join(", ", DeniedEquipment);
        set => DeniedEquipment = value.Split(",")
            .Select(s => s.Trim()).ToList();
    }

    public EquipmentProfile(string name)
    {
        Name = name;
    }

    public void Update(EquipmentProfile value)
    {
        CharacteristicsBonus = value.CharacteristicsBonus;
        DeniedEquipment = value.DeniedEquipment;
        Description = value.Description;
        IsBow = value.IsBow;
    }
}

public record EquipmentProfileRaw(
    string Name,
    string Description,
    CharacteristicsRaw? CharacteristicsBonus,
    bool IsBow,
    bool IsMultiple,
    string[]? DeniedEquipment);

public class EquipmentProfileMapper
{
    private readonly Mapper _mapper;

    public EquipmentProfileMapper(Mapper mapper)
    {
        _mapper = mapper;
    }

    public EquipmentProfileRaw Map(EquipmentProfile value) => new(
        value.Name,
        value.Description,
        value.CharacteristicsBonus == null ? null :
            _mapper.CharacteristicsMapper.Map(value.CharacteristicsBonus),
        value.IsBow,
        value.IsMultiple,
        value.DeniedEquipment.Any() ? value.DeniedEquipment.ToArray() : null);

    public EquipmentProfile Map(EquipmentProfileRaw raw) => new(raw.Name)
    {
        Description = raw.Description,
        CharacteristicsBonus = raw.CharacteristicsBonus == null ? null :
            _mapper.CharacteristicsMapper.Map(raw.CharacteristicsBonus),
        IsBow = raw.IsBow,
        IsMultiple = raw.IsMultiple,
        DeniedEquipment = raw.DeniedEquipment?.ToList() ?? new List<string>()
    };
}