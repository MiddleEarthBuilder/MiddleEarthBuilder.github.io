namespace MiddleEarth.Models;

public record ArmyList(
    string Name,
    Side Side,
    HeroProfile[] Heroes,
    WarriorProfile[] Warriors,
    SpecialRule[] ArmyBonuses,
    Alliance[] Alliances);