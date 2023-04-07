namespace MiddleEarth.Models;

public class ProfileEquipment
{
    public EquipmentProfile Profile { get; set; }
    public int DefaultCount { get; set; }
    public int Cost { get; set; }
    public bool IsAllowedOnce { get; set; } = true;
    public List<string> ReplacedEquipment { get; set; } = new();

    public ProfileEquipment(EquipmentProfile profile)
    {
        Profile = profile;
    }
}