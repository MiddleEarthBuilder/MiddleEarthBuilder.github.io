using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Entities;

public record AllianceRaw(
    string ArmyList,
    AllianceLevel Level);