using System.Text.Json.Serialization;

namespace MiddleEarth.Builder.Application.Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Side
{
    Undefined, Good, Evil
}