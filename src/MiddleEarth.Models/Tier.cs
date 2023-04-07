namespace MiddleEarth.Models;

public record Tier(int Id, string Name, int FollowersLimit) : IComparable<Tier>
{
    public static readonly Tier HeroOfLegend = new(1, "Hero of legend", 18);
    public static readonly Tier HeroOfValour = new(2, "Hero of valour", 15);
    public static readonly Tier HeroOfFortitude = new(3, "Hero of fortitude", 12);
    public static readonly Tier MinorHero = new(4, "Minor hero", 6);
    public static readonly Tier IndependentHero = new(5, "Minor hero", 0);
    public static readonly Tier Warrior = new(6, "Warrior", 0);
    private static readonly IReadOnlyCollection<Tier> Tiers = new[] { HeroOfLegend, HeroOfValour, HeroOfFortitude, MinorHero, IndependentHero, Warrior };

    public static Tier GetTier(string name) =>
        Tiers.FirstOrDefault(tier => tier.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ??
        throw new KeyNotFoundException($"Tier with name \"{name}\" not exists");

    public static Tier GetTier(int id) =>
        Tiers.FirstOrDefault(tier => tier.Id == id) ??
        throw new KeyNotFoundException($"Tier with id \"{id}\" not exists");

    public int CompareTo(Tier? other)
    {
        if (other == null)
            return 1;

        var compare = FollowersLimit.CompareTo(other.FollowersLimit);
        if (compare != 0)
            return compare;

        return other.Id.CompareTo(Id);
    }
}