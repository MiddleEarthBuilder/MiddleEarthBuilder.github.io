﻿using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class WarriorProfileMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public WarriorProfileMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public WarriorProfileRaw Map(WarriorProfile value) => new(
        value.ArmyList.Name,
        value.Name,
        value.Keywords.ToArray(),
        _mapper.CharacteristicsMapper.Map(value.Characteristics),
        value.Equipment.Select(_mapper.ProfileEquipmentMapper.Map).ToArray(),
        value.SpecialRules.Select(_mapper.ProfileSpecialRuleMapper.Map).ToArray(),
        value.Cost,
        value.Note);

    public WarriorProfile Map(WarriorProfileRaw raw) => new(
        _context.ArmyLists.GetOrCreate(raw.ArmyList),
        raw.Name)
    {
        Keywords = raw.Keywords.ToList(),
        Characteristics = _mapper.CharacteristicsMapper.Map(raw.Characteristics),
        Equipment = raw.Equipment.Select(_mapper.ProfileEquipmentMapper.Map).ToList(),
        SpecialRules = raw.SpecialRules.Select(_mapper.ProfileSpecialRuleMapper.Map).ToList(),
        Cost = raw.Cost,
        Note = raw.Note
    };
}