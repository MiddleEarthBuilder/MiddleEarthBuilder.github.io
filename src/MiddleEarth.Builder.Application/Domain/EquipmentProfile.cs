namespace MiddleEarth.Builder.Application.Domain;

public class EquipmentProfile
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public Characteristics? CharacteristicsBonus { get; set; }
    public bool IsBow { get; set; }
    public List<string> DeniedEquipment { get; set; } = new();

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