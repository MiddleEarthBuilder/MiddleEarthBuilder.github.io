namespace MiddleEarth.Builder.Application.Domain;
public class MagicalPower
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public MagicalPower(string name)
    {
        Name = name;
    }

    public void Update(MagicalPower value)
    {
        value.Description = Description;
    }
}

public record MagicalPowerRaw(string Name, string? Description);

public class MagicalPowerMapper
{
    public MagicalPowerRaw Map(MagicalPower value) => new(value.Name, value.Description);

    public MagicalPower Map(MagicalPowerRaw raw) => new(raw.Name)
    {
        Description = raw.Description
    };
}