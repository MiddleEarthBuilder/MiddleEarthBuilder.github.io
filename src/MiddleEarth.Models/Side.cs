using System.Text.Json.Serialization;

namespace MiddleEarth.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Side
{
    Undefined, Good, Evil
}