namespace MiddleEarth.Builder.Application.Domain;

public class ProfileEquipment
{
    public EquipmentProfile Profile { get; set; }
    public int DefaultCount { get; set; }
    public int Cost { get; set; }
    public List<string> ReplacedEquipment { get; set; } = new();
    public string ReplacedEquipmentString
    {
        get => string.Join(", ", ReplacedEquipment);
        set => ReplacedEquipment = value.Split(",")
            .Select(s => s.Trim()).ToList();
    }

    public ProfileEquipment(EquipmentProfile profile)
    {
        Profile = profile;
    }
}