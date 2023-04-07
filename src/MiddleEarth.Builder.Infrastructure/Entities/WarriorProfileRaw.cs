namespace MiddleEarth.Builder.Infrastructure.Entities;

public record WarriorProfileRaw(
    string ArmyList,
    string Name,
    string[] Keywords,
    CharacteristicsRaw Characteristics,
    ProfileEquipmentRaw[] Equipment,
    ProfileSpecialRuleRaw[] SpecialRules,
    int Cost,
    string? Note);