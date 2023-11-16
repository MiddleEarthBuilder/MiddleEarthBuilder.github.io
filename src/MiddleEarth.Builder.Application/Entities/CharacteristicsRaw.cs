namespace MiddleEarth.Builder.Application.Entities;

public record CharacteristicsRaw(
    int Move,
    int Fight,
    int? Shoot,
    int Strength,
    int Defense,
    int Attacks,
    int Wounds,
    int Courage,
    int? Might = null,
    int? Will = null,
    int? Fate = null)
{
    private string FightString => Shoot == null ? $"{Fight}/-" : $"{Fight}/{Shoot}+";

    public override string ToString() =>
        $"Mv {Move}\", F{FightString}, S{Strength}, D{Defense}, A{Attacks}, W{Wounds}, C{Courage}, M{Might}, W{Will}, F{Fate}";
}