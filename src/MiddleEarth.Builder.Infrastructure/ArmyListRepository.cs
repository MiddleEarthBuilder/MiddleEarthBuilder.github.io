using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure;

internal class ArmyListRepository : RepositoryBase<string, ArmyList>
{
    protected override string DataDirectoryRoute => "/data/army-list";

    public ArmyListRepository(HttpClient httpClient) :
        base(httpClient)
    { }

    protected override string GetKey(ArmyList entity) => entity.Name;
    protected override ArmyList CreateNew(string name) => new(
        name, Side.Undefined,
        Array.Empty<HeroProfile>(), 
        Array.Empty<WarriorProfile>(), 
        Array.Empty<SpecialRule>(), 
        Array.Empty<Alliance>()); // TODO: Change to Dto
}