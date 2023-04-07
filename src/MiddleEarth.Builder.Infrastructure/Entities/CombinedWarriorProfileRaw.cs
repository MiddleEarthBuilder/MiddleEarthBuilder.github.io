namespace MiddleEarth.Builder.Infrastructure.Entities;

public record CombinedWarriorProfileRaw(
    string ArmyList,
    string Name,
    string[] Keywords,
    CharacteristicsRaw Characteristics,
    UnitProfileEquipmentRaw[] Equipment,
    UnitProfileSpecialRuleRaw[] SpecialRules,
    int Cost,
    string? Note,
    WarriorProfileRaw[] AdditionalUnits,
    bool CountsAsOne) :
    WarriorProfileRaw(ArmyList, Name, Keywords, Characteristics,
        Equipment, SpecialRules, Cost, Note);