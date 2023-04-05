using MiddleEarth.Models;

namespace MiddleEarth.Builder.Application;

public static class RouteProvider
{
    public static string GetArmiesRoute(Side side) => $"/armies/{side}";
    public static string GetArmyListDataRoute(string name) => $"/data/army-list/{name}";
    public static string GetArmyListRoute(Side side, string? name = null) => string.IsNullOrEmpty(name) ? $"/{side}/army-list" : $"/{side}/army-list/{name}";
}