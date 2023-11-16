namespace MiddleEarth.Builder.Application.Entities;

public record HeroRaw(
    string ArmyList,
    string Name,
    EquipmentRaw[] Equipment);