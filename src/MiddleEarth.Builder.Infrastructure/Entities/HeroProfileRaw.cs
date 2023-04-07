namespace MiddleEarth.Builder.Infrastructure.Entities;

public record HeroProfileRaw(
    string ArmyList,
    string Name,
    string Tier,
    CharacteristicsRaw Characteristics,
    UnitProfileEquipmentRaw[] Equipment,
    string[] SpecialRules,
    int Cost,
    string? Note);