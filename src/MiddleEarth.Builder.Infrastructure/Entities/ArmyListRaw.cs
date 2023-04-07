using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Entities;

public record ArmyListRaw(
    string Name,
    Side Side,
    HeroProfileRaw[] Heroes,
    WarriorProfileRaw[] Warriors,
    ProfileSpecialRuleRaw[] ArmyBonuses,
    AllianceRaw[] Alliances);