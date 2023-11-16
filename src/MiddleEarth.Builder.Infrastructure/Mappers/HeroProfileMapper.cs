using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class HeroProfileMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public HeroProfileMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public HeroProfileRaw Map(HeroProfile value) => new(
        value.ArmyList.Name,
        value.Name,
        value.Tier.Name,
        value.Keywords.ToArray(),
        _mapper.CharacteristicsMapper.Map(value.Characteristics),
        value.Equipment.Select(_mapper.ProfileEquipmentMapper.Map).ToArray(),
        value.SpecialRules.Select(_mapper.ProfileSpecialRuleMapper.Map).ToArray(),
        value.Cost,
        value.Note);

    public HeroProfile Map(HeroProfileRaw raw) => new(
        _context.GetOrCreateArmyList(raw.ArmyList),
        raw.Name,
        Tier.GetTier(raw.Tier))
    {
        Keywords = raw.Keywords.ToList(),
        Characteristics = _mapper.CharacteristicsMapper.Map(raw.Characteristics),
        Equipment = raw.Equipment.Select(_mapper.ProfileEquipmentMapper.Map).ToList(),
        SpecialRules = raw.SpecialRules.Select(_mapper.ProfileSpecialRuleMapper.Map).ToList(),
        Cost = raw.Cost,
        Note = raw.Note
    };
}