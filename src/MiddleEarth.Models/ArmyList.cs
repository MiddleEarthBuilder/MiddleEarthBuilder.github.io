using System.Text.Json.Serialization;

namespace MiddleEarth.Models;

public record ArmyList(string Name, Side Side, HeroProfile[] Heroes, WarriorProfile[] Warriors, SpecialRule[] ArmyBonuses, Alliance[] Alliances);

public record Alliance(string ArmyList, AllianceLevel Level);

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AllianceLevel
{
    Impossible, Convenient, Historical
}

public record Tier(int Id, string Name, int FollowersLimit)
{
    public static readonly Tier HeroOfLegend = new(1, "Hero of legend", 18);
    public static readonly Tier HeroOfValour = new(2, "Hero of valour", 15);
    public static readonly Tier HeroOfFortitude = new(3, "Hero of fortitude", 12);
    public static readonly Tier MinorHero = new(4, "Minor hero", 6);
    public static readonly Tier IndependentHero = new(5, "Minor hero", 0);
    private static readonly IReadOnlyCollection<Tier> Tiers = new[] { HeroOfLegend, HeroOfValour, HeroOfFortitude, MinorHero, IndependentHero };

    public static Tier GetTier(string name) =>
        Tiers.FirstOrDefault(tier => tier.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ??
        throw new KeyNotFoundException($"Tier with name \"{name}\" not exists");

    public static Tier GetTier(int id) =>
        Tiers.FirstOrDefault(tier => tier.Id == id) ??
        throw new KeyNotFoundException($"Tier with id \"{id}\" not exists");
}

public enum Side
{
    Undefined, Good, Evil
}