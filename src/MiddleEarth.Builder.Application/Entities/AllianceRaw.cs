using MiddleEarth.Builder.Application.Domain;

namespace MiddleEarth.Builder.Application.Entities;

public record AllianceRaw(
    string ArmyList,
    AllianceLevel Level);