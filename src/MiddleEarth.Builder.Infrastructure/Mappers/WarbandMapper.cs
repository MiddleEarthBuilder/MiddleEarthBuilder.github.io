using MiddleEarth.Builder.Infrastructure.Entities;
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

    public Warband Map(WarbandRaw storageValue) => new()
    {
        Hero = storageValue.Hero == null ? null : _mapper.HeroMapper.Map(storageValue.Hero),
        Followers = storageValue.Followers.Select(_mapper.WarriorMapper.Map).OfType<Warrior>().ToList()
    };
}