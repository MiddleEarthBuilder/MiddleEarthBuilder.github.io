namespace MiddleEarth.Builder.Application.Domain;

public record CombinedWarriorProfileRaw(
    string ArmyList,
    string Name,
    string[]? Keywords,
    CharacteristicsRaw Characteristics,
    ProfileEquipmentRaw[]? Equipment,
    ProfileSpecialRuleRaw[]? SpecialRules,
    int Cost,
    string? Note,
    WarriorProfileRaw[]? AdditionalUnits,
    bool CountsAsOne) :
    WarriorProfileRaw(ArmyList, Name, Keywords, Characteristics,
        Equipment, SpecialRules, Cost, Note);