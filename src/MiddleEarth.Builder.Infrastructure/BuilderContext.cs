using Microsoft.Extensions.Logging;
using MiddleEarth.Builder.Application;
using MiddleEarth.Builder.Infrastructure.Mappers;
using MiddleEarth.Builder.Infrastructure.Repositories;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure;

public class BuilderContext
{
    public IRepository<string, ArmyList> ArmyLists { get; }
    public IRepository<string, SpecialRule> SpecialRules { get; }
    public IRepository<string, EquipmentProfile> Equipments { get; }
    public Mapper Mapper { get; }

    public BuilderContext(HttpClient httpClient, ILoggerFactory loggerFactory)
    {
        ArmyLists = new ArmyListsRepository(this, httpClient, loggerFactory.CreateLogger<ArmyListsRepository>());
        SpecialRules = new SpecialRulesRepository(this, httpClient, loggerFactory.CreateLogger<SpecialRulesRepository>());
        Equipments = new EquipmentRepository(this, httpClient, loggerFactory.CreateLogger<EquipmentRepository>());
        Mapper = new Mapper(this);
    }

    public async Task<Army> CreateArmy(string armyListName) => new(
        await ArmyLists.GetOrCreateAsync(armyListName, CancellationToken.None));

    public async Task Update(CancellationToken cancellationToken)
    {
        var armyLists = await ArmyLists.GetAllAsync(cancellationToken);
        var specialRules = armyLists
            .SelectMany(list => list.Heroes
                .SelectMany(hero => hero.SpecialRules)
                .Concat(list.Warriors
                    .SelectMany(warrior => warrior.SpecialRules)));

        foreach (var specialRule in specialRules)
            await SpecialRules.UpdateAsync(specialRule, cancellationToken); // TODO: Choose the newest

        var equipmentProfiles = armyLists
            .SelectMany(list => list.Heroes
                .SelectMany(hero => hero.Equipment)
                .Concat(list.Warriors
                    .SelectMany(warrior => warrior.Equipment)))
            .Select(equipment => equipment.Profile);

        foreach (var equipment in equipmentProfiles)
            await Equipments.UpdateAsync(equipment, cancellationToken); // TODO: Choose the newest
    }
}