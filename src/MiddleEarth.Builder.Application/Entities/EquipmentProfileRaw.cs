namespace MiddleEarth.Builder.Application.Entities;

public record EquipmentProfileRaw(
    string Name,
    string Description,
    CharacteristicsRaw? CharacteristicsBonus,
    bool IsBow = false,
    string[]? DeniedEquipment = null)
{
    public string[] DeniedEquipment { get; set; } = DeniedEquipment ??
                                                    Array.Empty<string>();
}