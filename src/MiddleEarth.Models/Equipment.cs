namespace MiddleEarth.Models;

public class Equipment
{
    public UnitProfileEquipment ProfileEquipment { get; set; }
    public int Count { get; set; } = 1;

    public Equipment(UnitProfileEquipment equipment)
    {
        ProfileEquipment = equipment;
    }
}