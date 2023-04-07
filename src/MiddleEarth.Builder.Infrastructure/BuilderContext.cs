using Microsoft.Extensions.Logging;
using MiddleEarth.Builder.Application;
using MiddleEarth.Builder.Infrastructure.Mappers;
using MiddleEarth.Builder.Infrastructure.Repositories;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure;

public class BuilderContext
{
    public IRepository<string, ArmyListDto> ArmyLists { get; }
    public IRepository<string, SpecialRuleDto> SpecialRules { get; }
    public IRepository<string, EquipmentDto> Equipments { get; }
    public Mapper Mapper { get; }

    public BuilderContext(HttpClient httpClient, ILoggerFactory loggerFactory)
    {
        ArmyLists = new ArmyListsRepository(this, httpClient, loggerFactory.CreateLogger<ArmyListsRepository>());
        SpecialRules = new SpecialRulesRepository(this, httpClient, loggerFactory.CreateLogger<SpecialRulesRepository>());
        Equipments = new EquipmentRepository(this, httpClient, loggerFactory.CreateLogger<EquipmentRepository>());
        Mapper = new Mapper(this);
    }

    public async Task<ArmyDto> CreateArmy(string armyListName) => new(
        await ArmyLists.GetOrCreateAsync(armyListName, CancellationToken.None));
}