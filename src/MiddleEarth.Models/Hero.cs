namespace MiddleEarth.Models;

public class Hero
{
    public HeroProfile Profile { get; set; }
    public List<Equipment> Equipment { get; set; }

    public int Cost => Profile.Cost +
                       Equipment.Sum(equipment => equipment.Count * equipment.ProfileEquipment.Cost);

    public Hero(HeroProfile profile)
    {
        Profile = profile;
        Equipment = profile.Equipment
            .Select(equipment => new Equipment(equipment))
            .ToList();
    }
}