using MiddleEarth.Models;
using System.Text.Json.Serialization;

namespace MiddleEarth.Builder.Infrastructure.Entities;

public record ArmyListRaw(
    string Name,
    [property: JsonConverter(typeof(JsonStringEnumConverter))] Side Side,
    HeroProfileRaw[] Heroes,
    WarriorProfileRaw[] Warriors,
    ProfileSpecialRuleRaw[] ArmyBonuses,
    AllianceRaw[] Alliances);