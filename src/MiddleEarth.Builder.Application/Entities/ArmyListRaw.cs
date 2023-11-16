using MiddleEarth.Builder.Application.Domain;
using System.Text.Json.Serialization;

namespace MiddleEarth.Builder.Application.Entities;

public record ArmyListRaw(
    string Name,
    [property: JsonConverter(typeof(JsonStringEnumConverter))] Side Side,
    HeroProfileRaw[] Heroes,
    WarriorProfileRaw[] Warriors,
    ProfileSpecialRuleRaw[] ArmyBonuses,
    AllianceRaw[] Alliances);