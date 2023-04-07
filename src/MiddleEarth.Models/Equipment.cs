namespace MiddleEarth.Models;

public class Equipment
{
    public ProfileEquipment ProfileEquipment { get; set; }
    public int Count { get; set; } = 1;

    public Equipment(ProfileEquipment equipment)
    {
        ProfileEquipment = equipment;
    }
}