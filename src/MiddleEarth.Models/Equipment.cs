namespace MiddleEarth.Models;

public class Equipment
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public Characteristics? CharacteristicsBonus { get; set; }
    public int Cost { get; set; }
    public bool IsBow { get; set; }
    public bool IsAllowedOnce { get; set; } = true;
    public List<string> DeniedEquipment { get; set; } = new();
    public List<string> ReplacedEquipment { get; set; } = new();
    public bool IsEquipped { get; set; }

    public Equipment(string name)
    {
        Name = name;
    }

    public Equipment(Equipment source)
    {
        Name = source.Name;
        Description = source.Description;
        CharacteristicsBonus = source.CharacteristicsBonus == null ? null : new Characteristics(source.CharacteristicsBonus);
        Cost = source.Cost;
        IsBow = source.IsBow;
        IsAllowedOnce = source.IsAllowedOnce;
        DeniedEquipment = new List<string>(source.DeniedEquipment);
        ReplacedEquipment = new List<string>(source.ReplacedEquipment);
        IsEquipped = source.IsEquipped;
    }
}