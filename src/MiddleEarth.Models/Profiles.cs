namespace MiddleEarth.Models;

public record HeroProfile(
    string ArmyList,
    string Name,
    string Tier,
    HeroCharacteristics Characteristics,
    CommonEquipment CommonEquipment,
    CustomEquipment CustomEquipment,
    int Cost,
    string? Note);

public record WarriorProfile(
    string ArmyList,
    string Name,
    WarriorCharacteristics Characteristics,
    CommonEquipment CommonEquipment,
    CustomEquipment CustomEquipment,
    int Cost,
    string? Note);

public record CommonEquipment(
    string Name,
    bool IsDefault,
    int Cost);

public record CustomEquipment(
    Equipment Profile,
    bool IsDefault,
    int Cost);

public record Equipment(
    string Name,
    string Description,
    WarriorCharacteristics CharacteristicsBonus,
    bool IsBow = false,
    bool IsAllowedOnce = true,
    string[]? DeniedEquipment = null,
    string[]? ReplacedEquipment = null)
{
    public string[] DeniedEquipment { get; set; } = DeniedEquipment ??
                                                    Array.Empty<string>();
    public string[] ReplacedEquipment { get; set; } = ReplacedEquipment ??
                                                      Array.Empty<string>();
}

public record CombinedHeroProfile(
    string ArmyList,
    string Name,
    string Tier,
    HeroCharacteristics Characteristics,
    CommonEquipment CommonEquipment,
    CustomEquipment CustomEquipment,
    int Cost,
    string? Note,
    WarriorProfile[] AdditionalUnits,
    bool CountsAsOne) :
    HeroProfile(ArmyList, Name, Tier, Characteristics,
        CommonEquipment, CustomEquipment, Cost, Note);

public record CombinedWarriorProfile(
    string ArmyList,
    string Name,
    WarriorCharacteristics Characteristics,
    CommonEquipment CommonEquipment,
    CustomEquipment CustomEquipment,
    int Cost,
    string? Note,
    WarriorProfile[] AdditionalUnits,
    bool CountsAsOne) :
    WarriorProfile(ArmyList, Name, Characteristics,
        CommonEquipment, CustomEquipment, Cost, Note);