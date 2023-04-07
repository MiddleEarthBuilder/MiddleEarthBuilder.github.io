namespace MiddleEarth.Builder.Infrastructure.Entities;

public record HeroProfileRaw(
    string ArmyList,
    string Name,
    string Tier,
    string[] Keywords,
    CharacteristicsRaw Characteristics,
    ProfileEquipmentRaw[] Equipment,
    ProfileSpecialRuleRaw[] SpecialRules,
    int Cost,
    string? Note);