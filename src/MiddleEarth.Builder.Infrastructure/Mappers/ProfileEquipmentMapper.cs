using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class ProfileEquipmentMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public ProfileEquipmentMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ProfileEquipmentRaw MapProfile(Equipment value) => new(
        value.Name,
        value.IsDefault,
        value.Cost);

    public Equipment Map(ProfileEquipmentRaw storageValue)
    {
        var equipment = _context.Equipments.GetOrCreate(storageValue.Name);

        return new Equipment(equipment.Name)
        {
            Description = equipment.Description,
            CharacteristicsBonus = equipment.CharacteristicsBonus,
            Cost = storageValue.Cost,
            IsBow = equipment.IsBow,
            IsAllowedOnce = equipment.IsAllowedOnce,
            DeniedEquipment = equipment.DeniedEquipment,
            ReplacedEquipment = equipment.ReplacedEquipment,
            IsEquipped = storageValue.IsDefault
        };
    }
}