namespace MiddleEarth.Builder.Infrastructure.Entities;

public record HeroRaw(
    string ArmyList,
    string Name,
    EquipmentRaw[] Equipment);