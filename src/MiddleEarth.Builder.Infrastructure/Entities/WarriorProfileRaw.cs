namespace MiddleEarth.Builder.Infrastructure.Entities;

public record WarriorProfileRaw(
    string ArmyList,
    string Name,
    string[] Keywords,
    CharacteristicsRaw Characteristics,
    UnitProfileEquipmentRaw[] Equipment,
    UnitProfileSpecialRuleRaw[] SpecialRules,
    int Cost,
    string? Note);