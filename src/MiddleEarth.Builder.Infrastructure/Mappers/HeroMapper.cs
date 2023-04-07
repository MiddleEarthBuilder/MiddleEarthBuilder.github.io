﻿using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class HeroMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public HeroMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public HeroRaw Map(Hero value) => new(
        value.Profile.ArmyList.Name,
        value.Profile.Name,
        value.Equipment.Select(_mapper.EquipmentMapper.Map).ToArray());

    public Hero? Map(HeroRaw raw)
    {
        var armyList = _context.ArmyLists.GetOrCreate(raw.ArmyList);
        var hero = armyList.Heroes.FirstOrDefault(hero => hero.Name == raw.Name);
        return hero == null ?
            null :
            new Hero(hero);
    }
}