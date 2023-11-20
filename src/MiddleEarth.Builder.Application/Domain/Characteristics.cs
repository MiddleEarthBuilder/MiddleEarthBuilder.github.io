using System.ComponentModel.DataAnnotations;

namespace MiddleEarth.Builder.Application.Domain;

public class Characteristics
{
    [Required] public int? Move { get; set; } = 6;
    public string FightAndShoot
    {
        get => FightString;
        set
        {
            var values = value.Split('/');
            int.TryParse(values[0].Trim(), out var fight);
            Fight = fight;
            if (int.TryParse(values[0].Replace("+", "").Trim(), out var shoot))
                Shoot = shoot;
        }
    }

    [Required]
    public int? Fight { get; set; } = 3;
    public int? Shoot { get; set; } = 4;
    [Required]
    public int? Strength { get; set; } = 3;
    [Required]
    public int? Defense { get; set; } = 3;
    [Required]
    public int? Attacks { get; set; } = 1;
    [Required]
    public int? Wounds { get; set; } = 1;
    [Required]
    public int? Courage { get; set; } = 3;
    public int? Might { get; set; }
    public int? Will { get; set; }
    public int? Fate { get; set; }

    public Characteristics() { }

    public Characteristics(Characteristics source)
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

public class CharacteristicsMapper
{
    private readonly Context _context;
    private readonly Mapper _mapper;

    public CharacteristicsMapper(Context context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public CharacteristicsRaw Map(Characteristics value) => new(
        value.Move!.Value,
        value.Fight!.Value,
        value.Shoot,
        value.Strength!.Value,
        value.Defense!.Value,
        value.Attacks!.Value,
        value.Wounds!.Value,
        value.Courage!.Value,
        value.Might,
        value.Will,
        value.Fate);

    public Characteristics Map(CharacteristicsRaw raw) => new()
    {
        Move = raw.Move,
        Fight = raw.Fight,
        Shoot = raw.Shoot,
        Strength = raw.Strength,
        Defense = raw.Defense,
        Attacks = raw.Attacks,
        Wounds = raw.Wounds,
        Courage = raw.Courage,
        Might = raw.Might,
        Will = raw.Will,
        Fate = raw.Fate
    };
}