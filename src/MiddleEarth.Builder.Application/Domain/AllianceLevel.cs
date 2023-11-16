using System.Text.Json.Serialization;

namespace MiddleEarth.Builder.Application.Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AllianceLevel
{
    Impossible, Convenient, Historical
}