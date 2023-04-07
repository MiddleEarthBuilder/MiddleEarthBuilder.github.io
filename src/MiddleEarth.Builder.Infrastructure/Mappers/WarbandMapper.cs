﻿using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Mappers;

public class WarbandMapper
{
    private readonly BuilderContext _context;
    private readonly Mapper _mapper;

    public WarbandMapper(BuilderContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public WarbandRaw Map(Warband value) => new(
        _mapper.HeroMapper.Map(value.Hero),
        value.Followers.Select(_mapper.WarriorMapper.Map).ToArray());

    public Warband Map(WarbandRaw raw) => new()
    {
        Hero = raw.Hero == null ? null :
            _mapper.HeroMapper.Map(raw.Hero),
        Followers = raw.Followers.Select(_mapper.WarriorMapper.Map).OfType<Warrior>().ToList()
    };
}