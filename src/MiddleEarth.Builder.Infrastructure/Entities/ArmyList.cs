using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Entities;

public record ArmyList(
    string Name,
    Side Side,
    HeroProfile[] Heroes,
    WarriorProfile[] Warriors,
    SpecialRule[] ArmyBonuses,
    Alliance[] Alliances);