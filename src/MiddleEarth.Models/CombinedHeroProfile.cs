namespace MiddleEarth.Models;

public record CombinedHeroProfile(
    string ArmyList,
    string Name,
    string Tier,
    Characteristics Characteristics,
    ProfileEquipment[] Equipment,
    int Cost,
    string? Note,
    WarriorProfile[] AdditionalUnits,
    bool CountsAsOne) :
    HeroProfile(ArmyList, Name, Tier, Characteristics,
        Equipment, Cost, Note);