namespace MiddleEarth.Models;

public class EquipmentDto
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public CharacteristicsDto? CharacteristicsBonus { get; set; }
    public int Cost { get; set; }
    public bool IsBow { get; set; }
    public bool IsAllowedOnce { get; set; } = true;
    public List<string> DeniedEquipment { get; set; } = new();
    public List<string> ReplacedEquipment { get; set; } = new();
    public bool IsEquipped { get; set; }

    public EquipmentDto(string name)
    {
        Name = name;
    }

    public EquipmentDto(EquipmentDto source)
    {
        Name = source.Name;
        Description = source.Description;
        CharacteristicsBonus = source.CharacteristicsBonus == null ? null : new CharacteristicsDto(source.CharacteristicsBonus);
        Cost = source.Cost;
        IsBow = source.IsBow;
        IsAllowedOnce = source.IsAllowedOnce;
        DeniedEquipment = new List<string>(source.DeniedEquipment);
        ReplacedEquipment = new List<string>(source.ReplacedEquipment);
        IsEquipped = source.IsEquipped;
    }
}