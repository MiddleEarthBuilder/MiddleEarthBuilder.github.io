namespace MiddleEarth.Builder.Infrastructure.Entities;

public record WarriorProfileRaw(
    string ArmyList,
    string Name,
    CharacteristicsRaw Characteristics,
    UnitProfileEquipmentRaw[] Equipment,
    string[] SpecialRules,
    int Cost,
    string? Note);