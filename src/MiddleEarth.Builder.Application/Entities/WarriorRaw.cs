namespace MiddleEarth.Builder.Application.Entities;

public record WarriorRaw(
    string ArmyList,
    string Name,
    EquipmentRaw[] Equipment,
    int Count);