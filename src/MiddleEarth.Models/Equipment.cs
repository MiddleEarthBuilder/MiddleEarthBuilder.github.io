namespace MiddleEarth.Models;

public record Equipment(
    string Name,
    string Description,
    Characteristics? CharacteristicsBonus,
    bool IsBow = false,
    bool IsAllowedOnce = true,
    string[]? DeniedEquipment = null,
    string[]? ReplacedEquipment = null) : IComparable<Equipment>
{
    public string[] DeniedEquipment { get; set; } = DeniedEquipment ??
                                                    Array.Empty<string>();
    public string[] ReplacedEquipment { get; set; } = ReplacedEquipment ??
                                                      Array.Empty<string>();

    public int CompareTo(Equipment? other) =>
        string.Compare(Name, other?.Name, StringComparison.Ordinal);
}

public class EquipmentDto
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public CharacteristicsDto? CharacteristicsBonus { get; set; }
    public int Cost { get; set; }
    public bool IsBow { get; set; }
    public bool IsAllowedOnce { get; set; } = true;
    public List<string> DeniedEquipment { get; set; } = new();
    public List<string> ReplacedEquipment { get; set; } = new();
    public bool IsEquipped { get; set; }

    public EquipmentDto(string name)
    {
        Name = name;
    }

    public EquipmentDto(EquipmentDto source)
    {
        Name = source.Name;
        Description = source.Description;
        CharacteristicsBonus = source.CharacteristicsBonus == null ? null : new CharacteristicsDto(source.CharacteristicsBonus);
        Cost = source.Cost;
        IsBow = source.IsBow;
        IsAllowedOnce = source.IsAllowedOnce;
        DeniedEquipment = new List<string>(source.DeniedEquipment);
        ReplacedEquipment = new List<string>(source.ReplacedEquipment);
        IsEquipped = source.IsEquipped;
    }
}