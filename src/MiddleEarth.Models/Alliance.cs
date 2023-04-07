using System.Text.Json.Serialization;

namespace MiddleEarth.Models;

public record Alliance(
    string ArmyList,
    AllianceLevel Level);

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AllianceLevel
{
    Impossible, Convenient, Historical
}