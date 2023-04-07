namespace MiddleEarth.Builder.Infrastructure.Entities;

public record WarriorProfile(
    string ArmyList,
    string Name,
    Characteristics Characteristics,
    ProfileEquipment[] Equipment,
    int Cost,
    string? Note);