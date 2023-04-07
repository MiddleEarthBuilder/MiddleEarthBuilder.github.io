namespace MiddleEarth.Builder.Infrastructure.Entities;

public record CombinedWarriorProfile(
    string ArmyList,
    string Name,
    Characteristics Characteristics,
    ProfileEquipment[] Equipment,
    int Cost,
    string? Note,
    WarriorProfile[] AdditionalUnits,
    bool CountsAsOne) :
    WarriorProfile(ArmyList, Name, Characteristics,
        Equipment, Cost, Note);