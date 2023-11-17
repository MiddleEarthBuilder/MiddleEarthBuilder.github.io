namespace MiddleEarth.Builder.Application.Domain;
public class HeroicAction
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public HeroicAction(string name)
    {
        Name = name;
    }

    public void Update(HeroicAction value)
    {
        value.Description = Description;
    }
}

public record HeroicActionRaw(string Name, string? Description);

public class HeroicActionMapper
{
    public HeroicActionRaw Map(HeroicAction value) => new(value.Name, value.Description);

    public HeroicAction Map(HeroicActionRaw raw) => new(raw.Name)
    {
        Description = raw.Description
    };
}