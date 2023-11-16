using MiddleEarth.Builder.Application.Domain;
using MiddleEarth.Builder.Application.Entities;

namespace MiddleEarth.Builder.Application.Mappers;

public class EquipmentProfileMapper
{
    private readonly Mapper _mapper;

    public EquipmentProfileMapper(Mapper mapper)
    {
        _mapper = mapper;
    }

    public EquipmentProfileRaw Map(EquipmentProfile value) => new(
        value.Name,
        value.Description,
        value.CharacteristicsBonus == null ? null :
            _mapper.CharacteristicsMapper.Map(value.CharacteristicsBonus),
        value.IsBow,
        value.DeniedEquipment.ToArray());

    public EquipmentProfile Map(EquipmentProfileRaw raw) => new(raw.Name)
    {
        Description = raw.Description,
        CharacteristicsBonus = raw.CharacteristicsBonus == null ? null :
            _mapper.CharacteristicsMapper.Map(raw.CharacteristicsBonus),
        IsBow = raw.IsBow,
        DeniedEquipment = raw.DeniedEquipment.ToList()
    };
}