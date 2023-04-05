namespace MiddleEarth.Models;

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
    int Fate,
    string[] CommonSpecialRules,
    SpecialRule[] CustomSpecialRules) :
    WarriorCharacteristics(Move, Fight, Strength,
        Defense, Attacks, Wounds, Courage,
        CommonSpecialRules, CustomSpecialRules)
{
    public override string ToString() =>
        $"{base.ToString()}, M{Might}, W{Will}, F{Fate}";
}

public record WarriorCharacteristics(
    int Move,
    FightCharacteristic Fight,
    int Strength,
    int Defense,
    int Attacks,
    int Wounds,
    int Courage,
    string[] CommonSpecialRules,
    SpecialRule[] CustomSpecialRules)
{
    public override string ToString() =>
        $"Mv {Move}\", F{Fight}, S{Strength}, D{Defense}, A{Attacks}, W{Wounds}, C{Courage}";
}

public record FightCharacteristic(
    int Fight,
    int? Shoot)
{
    public override string ToString() => 
        Shoot == null ? $"{Fight}/-" : $"{Fight}/{Shoot}+";
}

public record SpecialRule(
    string Name,
    string Description,
    WarriorCharacteristics Bonus);