namespace MiddleEarth.Builder.Application.Domain;

public class Warrior
{
    public WarriorProfile Profile { get; set; }
    public List<Equipment> Equipment { get; set; }
    public int Count { get; set; } = 1;

    public int UnitCost => Profile.Cost +
                           Equipment.Sum(equipment => equipment.Count * equipment.ProfileEquipment.Cost);
    public int TotalCost => Count * UnitCost;
    public bool HasBow => Equipment.Any(equipment => equipment.Count > 0 && equipment.ProfileEquipment.Profile.IsBow);

    public Warrior(WarriorProfile profile)
    {
        Profile = profile;
        Equipment = profile.Equipment
            .Select(equipment => new Equipment(equipment))
            .ToList();
    }
}