namespace MiddleEarth.Builder.Infrastructure.Entities;

public record WarriorProfileRaw(
    string ArmyList,
    string Name,
    CharacteristicsRaw Characteristics,
    UnitProfileEquipmentRaw[] Equipment,
    int Cost,
    string? Note);