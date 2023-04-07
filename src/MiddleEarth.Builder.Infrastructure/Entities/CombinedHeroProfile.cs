namespace MiddleEarth.Builder.Infrastructure.Entities;

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