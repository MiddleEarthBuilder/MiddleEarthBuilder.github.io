namespace MiddleEarth.Builder.Infrastructure.Entities;

public record ProfileEquipmentRaw(
    string Name,
    int DefaultCount,
    int Cost,
    bool IsAllowedOnce,
    string[]? ReplacedEquipment = null)
{
    public string[] ReplacedEquipment { get; set; } = ReplacedEquipment ??
                                                      Array.Empty<string>();
}