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