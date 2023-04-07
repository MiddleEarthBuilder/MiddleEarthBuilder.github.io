namespace MiddleEarth.Builder.Infrastructure.Entities;

public record WarriorProfileRaw(
    string ArmyList,
    string Name,
    CharacteristicsRaw Characteristics,
    ProfileEquipmentRaw[] Equipment,
    int Cost,
    string? Note);