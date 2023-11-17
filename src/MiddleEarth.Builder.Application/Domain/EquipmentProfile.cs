namespace MiddleEarth.Builder.Application.Domain;

public class EquipmentProfile
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public Characteristics? CharacteristicsBonus { get; set; }
    public bool IsBow { get; set; }
    public bool IsAllowedOnce { get; set; } = true;
    public List<string> DeniedEquipment { get; set; } = new();
    public string DeniedEquipmentString
    {
        get => string.Join(", ", DeniedEquipment);
        set => DeniedEquipment = value.Split(",")
            .Select(s => s.Trim()).ToList();
    }

    public EquipmentProfile(string name)
    {
        Name = name;
    }

    public void Update(EquipmentProfile value)
    {
        CharacteristicsBonus = value.CharacteristicsBonus;
        DeniedEquipment = value.DeniedEquipment;
        Description = value.Description;
        IsBow = value.IsBow;
    }
}