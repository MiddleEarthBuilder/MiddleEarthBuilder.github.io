namespace MiddleEarth.Builder.Infrastructure.Entities;

public record CombinedHeroProfileRaw(
    string ArmyList,
    string Name,
    string Tier,
    string[] Keywords,
    CharacteristicsRaw Characteristics,
    UnitProfileEquipmentRaw[] Equipment,
    UnitProfileSpecialRuleRaw[] SpecialRules,
    int Cost,
    string? Note,
    WarriorProfileRaw[] AdditionalUnits,
    bool CountsAsOne) :
    HeroProfileRaw(ArmyList, Name, Tier, Keywords, Characteristics,
        Equipment, SpecialRules, Cost, Note);