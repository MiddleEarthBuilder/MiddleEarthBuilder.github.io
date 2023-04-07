namespace MiddleEarth.Builder.Infrastructure.Entities;

public record EquipmentRaw(
    string Name,
    string Description,
    CharacteristicsRaw? CharacteristicsBonus,
    bool IsBow = false,
    bool IsAllowedOnce = true,
    string[]? DeniedEquipment = null,
    string[]? ReplacedEquipment = null) : IComparable<EquipmentRaw>
{
    public string[] DeniedEquipment { get; set; } = DeniedEquipment ??
                                                    Array.Empty<string>();
    public string[] ReplacedEquipment { get; set; } = ReplacedEquipment ??
                                                      Array.Empty<string>();

    public int CompareTo(EquipmentRaw? other) =>
        string.Compare(Name, other?.Name, StringComparison.Ordinal);
}