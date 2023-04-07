using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Entities;

public record Alliance(
    string ArmyList,
    AllianceLevel Level);