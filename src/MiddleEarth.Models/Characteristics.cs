namespace MiddleEarth.Models;

public class CharacteristicsDto
{
    public int Move { get; set; }
    public int Fight { get; set; }
    public int? Shoot { get; set; }
    public int Strength { get; set; }
    public int Defense { get; set; }
    public int Attacks { get; set; }
    public int Wounds { get; set; }
    public int Courage { get; set; }
    public int? Might { get; set; }
    public int? Will { get; set; }
    public int? Fate { get; set; }
    public List<SpecialRuleDto> SpecialRules { get; set; } = new();

    public CharacteristicsDto() { }

    public CharacteristicsDto(CharacteristicsDto source)
    {
        Move = source.Move;
        Fight = source.Fight;
        Shoot = source.Shoot;
        Strength = source.Strength;
        Defense = source.Defense;
        Attacks = source.Attacks;
        Wounds = source.Wounds;
        Courage = source.Courage;
        Might = source.Might;
        Will = source.Will;
        Fate = source.Fate;
    }

    private string FightString => Shoot == null ? $"{Fight}/-" : $"{Fight}/{Shoot}+";

    public override string ToString() =>
        $"Mv {Move}\", F{FightString}, S{Strength}, D{Defense}, A{Attacks}, W{Wounds}, C{Courage}, M{Might}, W{Will}, F{Fate}";
}