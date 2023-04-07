namespace MiddleEarth.Builder.Infrastructure.Entities;

public record CombinedWarriorProfileRaw(
    string ArmyList,
    string Name,
    CharacteristicsRaw Characteristics,
    UnitProfileEquipmentRaw[] Equipment,
    int Cost,
    string? Note,
    WarriorProfileRaw[] AdditionalUnits,
    bool CountsAsOne) :
    WarriorProfileRaw(ArmyList, Name, Characteristics,
        Equipment, Cost, Note);