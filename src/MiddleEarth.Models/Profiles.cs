namespace MiddleEarth.Models;

public record HeroProfile(
    string ArmyList,
    string Name,
    string Tier,
    Characteristics Characteristics,
    ProfileEquipment[] Equipment,
    int Cost,
    string? Note);

public record WarriorProfile(
    string ArmyList,
    string Name,
    Characteristics Characteristics,
    ProfileEquipment[] Equipment,
    int Cost,
    string? Note);

public record ProfileEquipment(
    string Name,
    bool IsDefault,
    int Cost);

public record Equipment(
    string Name,
    string Description,
    Characteristics? CharacteristicsBonus,
    bool IsBow = false,
    bool IsAllowedOnce = true,
    string[]? DeniedEquipment = null,
    string[]? ReplacedEquipment = null) : IComparable<Equipment>
{
    public string[] DeniedEquipment { get; set; } = DeniedEquipment ??
                                                    Array.Empty<string>();
    public string[] ReplacedEquipment { get; set; } = ReplacedEquipment ??
                                                      Array.Empty<string>();

    public int CompareTo(Equipment? other) =>
        string.Compare(Name, other?.Name, StringComparison.Ordinal);
}

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

public record CombinedWarriorProfile(
    string ArmyList,
    string Name,
    Characteristics Characteristics,
    ProfileEquipment[] Equipment,
    int Cost,
    string? Note,
    WarriorProfile[] AdditionalUnits,
    bool CountsAsOne) :
    WarriorProfile(ArmyList, Name, Characteristics,
        Equipment, Cost, Note);