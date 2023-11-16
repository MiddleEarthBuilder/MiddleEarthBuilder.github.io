﻿using MiddleEarth.Builder.Application.Domain;

namespace MiddleEarth.Builder.Application;

public static class RouteProvider
{
    public static string GetArmiesRoute(Side side) => $"/armies/{side}";
    public static string GetArmyBuilderRoute(string name) => $"/armies/{name}/builder";
    public static string GetArmyListRoute(Side side, string? name = null) => string.IsNullOrEmpty(name) ? $"/{side}/army-lists" : $"/{side}/army-lists/{name}";
}