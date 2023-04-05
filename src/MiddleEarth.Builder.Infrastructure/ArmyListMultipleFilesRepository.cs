using Microsoft.Extensions.Logging;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure;

internal class ArmyListMultipleFilesRepository : MultipleFilesRepositoryBase<string, ArmyList>
{
    protected override string DataDirectoryPath => "/data/army-list";

    public ArmyListMultipleFilesRepository(HttpClient httpClient, ILogger<MultipleFilesRepositoryBase<string, ArmyList>> logger) :
        base(httpClient, logger)
    { }

    protected override string GetKey(ArmyList entity) => entity.Name;
    protected override ArmyList CreateNew(string name) => new(
        name, Side.Undefined,
        Array.Empty<HeroProfile>(), 
        Array.Empty<WarriorProfile>(), 
        Array.Empty<SpecialRule>(), 
        Array.Empty<Alliance>()); // TODO: Change to Dto
}