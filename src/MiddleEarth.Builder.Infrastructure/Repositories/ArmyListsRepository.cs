﻿using Microsoft.Extensions.Logging;
using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Repositories;

internal class ArmyListsRepository : MultipleFilesRepositoryBase<string, ArmyList, ArmyListRaw>
{
    protected override string DataDirectoryPath => "/data/army-list";

    public ArmyListsRepository(BuilderContext context, HttpClient httpClient, ILogger<ArmyListsRepository> logger) :
        base(context, httpClient, logger)
    { }

    protected override string GetKey(ArmyList entity) => entity.Name;

    protected override ArmyList CreateEmpty(string name) => new(name);

    protected override void Map(ArmyListRaw storeValue, ArmyList value) =>
        Context.Mapper.Map(storeValue, value);
}