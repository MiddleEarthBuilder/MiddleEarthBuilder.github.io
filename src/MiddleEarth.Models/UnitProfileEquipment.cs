namespace MiddleEarth.Models;

public class UnitProfileEquipment
{
    public EquipmentProfile Profile { get; set; }
    public int DefaultCount { get; set; }
    public int Cost { get; set; }
    public bool IsAllowedOnce { get; set; } = true;
    public List<string> ReplacedEquipment { get; set; } = new();

    public UnitProfileEquipment(EquipmentProfile profile)
    {
        Profile = profile;
    }
}