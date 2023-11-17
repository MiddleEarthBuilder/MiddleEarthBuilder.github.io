namespace MiddleEarth.Builder.Application.Entities;

public record ProfileEquipmentRaw(
    string Name,
    int DefaultCount,
    int Cost,
    string[]? ReplacedEquipment = null)
{
    public string[] ReplacedEquipment { get; set; } = ReplacedEquipment ??
                                                      Array.Empty<string>();
}