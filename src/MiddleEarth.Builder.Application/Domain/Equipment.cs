namespace MiddleEarth.Builder.Application.Domain;

public class Equipment
{
    public ProfileEquipment ProfileEquipment { get; set; }
    public int Count { get; set; } = 1;

    public Equipment(ProfileEquipment equipment)
    {
        ProfileEquipment = equipment;
    }
}

public record EquipmentRaw(string Name, int Count);

public class EquipmentMapper
{
    private readonly Context _context;

    public EquipmentMapper(Context context)
    {
        _context = context;
    }

    public EquipmentRaw Map(Equipment value) =>
        new(value.ProfileEquipment.Profile.Name, value.Count);

    public Equipment Map(EquipmentRaw value) => throw new NotImplementedException();
}