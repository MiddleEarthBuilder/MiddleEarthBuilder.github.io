using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class EquipmentMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public EquipmentMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Equipment Map(EquipmentRaw storageValue) => new(storageValue.Name)
    {
        Description = storageValue.Description,
        CharacteristicsBonus = storageValue.CharacteristicsBonus == null ? null :
            _mapper.CharacteristicsMapper.Map(storageValue.CharacteristicsBonus),
        IsBow = storageValue.IsBow,
        IsAllowedOnce = storageValue.IsAllowedOnce,
        DeniedEquipment = storageValue.DeniedEquipment.ToList(),
        ReplacedEquipment = storageValue.ReplacedEquipment.ToList()
    };

    public EquipmentRaw Map(Equipment value) => new(
        value.Name,
        value.Description,
        value.CharacteristicsBonus == null ? null :
            _mapper.CharacteristicsMapper.Map(value.CharacteristicsBonus),
        value.IsBow,
        value.IsAllowedOnce,
        value.DeniedEquipment.ToArray(),
        value.ReplacedEquipment.ToArray());
}