namespace MiddleEarth.Builder.Application.Domain;

public record CombinedHeroProfileRaw(
    string ArmyList,
    string Name,
    string Tier,
    string[] Keywords,
    CharacteristicsRaw Characteristics,
    ProfileEquipmentRaw[] Equipment,
    ProfileSpecialRuleRaw[] SpecialRules,
    int Cost,
    string? Note,
    WarriorProfileRaw[] AdditionalUnits,
    bool CountsAsOne) :
    HeroProfileRaw(ArmyList, Name, Tier, Keywords, Characteristics,
        Equipment, SpecialRules, Cost, Note);