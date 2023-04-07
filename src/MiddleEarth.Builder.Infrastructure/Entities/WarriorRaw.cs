namespace MiddleEarth.Builder.Infrastructure.Entities;

public record WarriorRaw(
    string ArmyList,
    string Name,
    EquipmentRaw[] Equipment,
    int Count);