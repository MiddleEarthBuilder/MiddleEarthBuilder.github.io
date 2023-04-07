namespace MiddleEarth.Models;

public record HeroProfile(
    string ArmyList,
    string Name,
    string Tier,
    Characteristics Characteristics,
    ProfileEquipment[] Equipment,
    int Cost,
    string? Note);