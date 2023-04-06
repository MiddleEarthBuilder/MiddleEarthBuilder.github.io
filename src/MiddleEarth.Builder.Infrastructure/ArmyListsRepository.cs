using Microsoft.Extensions.Logging;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure;

internal class ArmyListsRepository : MultipleFilesRepositoryBase<string, ArmyListDto, ArmyList>
{
    protected override string DataDirectoryPath => "/data/army-list";

    public ArmyListsRepository(BuilderContext context, HttpClient httpClient, ILogger<ArmyListsRepository> logger) :
        base(context, httpClient, logger)
    { }

    protected override string GetKey(ArmyListDto entity) => entity.Name;

    protected override ArmyListDto CreateEmpty(string name) => new(name);

    protected override void Map(ArmyList storeValue, ArmyListDto value) => 
        Context.Mapper.Map(storeValue, value);
}