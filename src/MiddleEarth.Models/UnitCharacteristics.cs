namespace MiddleEarth.Models
{
    public record HeroCharacteristics(
        int Move,
        FightCharacteristic Fight,
        int Strength,
        int Defense,
        int Attacks,
        int Wounds,
        int Courage,
        int Might,
        int Will,
        int Fate) :
        UnitCharacteristics(Move, Fight, Strength, Defense, Attacks, Wounds, Courage)
    {
        public override string ToString() => $"{base.ToString()}, M{Might}, W{Will}, F{Fate}";
    }

    public record UnitCharacteristics(
        int Move,
        FightCharacteristic Fight,
        int Strength,
        int Defense,
        int Attacks,
        int Wounds,
        int Courage)
    {
        public override string ToString() => $"Mv {Move}\", F{Fight}, S{Strength}, D{Defense}, A{Attacks}, W{Wounds}, C{Courage}";
    }

    public record FightCharacteristic(
        int Fight,
        int? Shoot)
    {
        public override string ToString() => Shoot == null ? $"{Fight}/-" : $"{Fight}/{Shoot}+";
    }
}