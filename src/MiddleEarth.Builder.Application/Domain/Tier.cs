namespace MiddleEarth.Builder.Application.Domain;

//[TypeConverter(typeof(TierConverter))]
public record Tier(TierEnum Id, string Name, int FollowersLimit) : IComparable<Tier>
{
    public static readonly Tier HeroOfLegend = new(TierEnum.HeroOfLegend, "Hero of Legend", 18);
    public static readonly Tier HeroOfValour = new(TierEnum.HeroOfValour, "Hero of Valour", 15);
    public static readonly Tier HeroOfFortitude = new(TierEnum.HeroOfFortitude, "Hero of Fortitude", 12);
    public static readonly Tier MinorHero = new(TierEnum.MinorHero, "Minor Hero", 6);
    public static readonly Tier IndependentHero = new(TierEnum.IndependentHero, "Independent Hero", 0);
    public static readonly Tier Warrior = new(TierEnum.Warrior, "Warrior", 0);
    private static readonly IReadOnlyCollection<Tier> Tiers = new[] { HeroOfLegend, HeroOfValour, HeroOfFortitude, MinorHero, IndependentHero, Warrior };

    public static Tier GetTier(string name) =>
        Tiers.FirstOrDefault(tier => tier.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ??
        throw new KeyNotFoundException($"Tier with name \"{name}\" not exists");

    public static Tier GetTier(int id) =>
        GetTier((TierEnum)id);

    public static Tier GetTier(TierEnum id) =>
        Tiers.FirstOrDefault(tier => tier.Id == id) ??
        throw new KeyNotFoundException($"Tier with id \"{id}\" not exists");

    public int CompareTo(Tier? other)
    {
        if (other == null)
            return 1;

        var compare = FollowersLimit.CompareTo(other.FollowersLimit);
        return compare != 0 ?
            compare :
            other.Id.CompareTo(Id);
    }
}

public enum TierEnum
{
    HeroOfLegend, HeroOfValour, HeroOfFortitude, MinorHero, IndependentHero, Warrior
}

//public class TierConverter : TypeConverter
//{
//    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) =>
//        sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

//    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
//    {
//        if (value is string name)
//            return Tier.GetTier(name);

//        return base.ConvertFrom(context, culture, value);
//    }
//}