using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Entities;

public record ArmyListRaw(
    string Name,
    Side Side,
    HeroProfileRaw[] Heroes,
    WarriorProfileRaw[] Warriors,
    SpecialRuleRaw[] ArmyBonuses,
    AllianceRaw[] Alliances);