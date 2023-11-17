namespace MiddleEarth.Builder.Application.Entities;

public record EquipmentProfileRaw(
    string Name,
    string Description,
    CharacteristicsRaw? CharacteristicsBonus,
    bool IsBow = false,
    bool IsAllowedOnce = true,
    string[]? DeniedEquipment = null)
{
    public string[] DeniedEquipment { get; set; } = DeniedEquipment ??
                                                    Array.Empty<string>();
}