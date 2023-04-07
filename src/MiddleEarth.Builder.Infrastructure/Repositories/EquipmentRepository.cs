using Microsoft.Extensions.Logging;
using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Repositories;

internal class EquipmentRepository : SingleFileRepositoryBase<string, Equipment, EquipmentRaw>
{
    protected override string DataFilePath => "/data/equipments.json";

    public EquipmentRepository(BuilderContext context, HttpClient httpClient, ILogger<EquipmentRepository> logger) :
        base(context, httpClient, logger)
    { }

    protected override string GetKey(Equipment entity) => entity.Name;
    protected override Equipment CreateEmpty(string key) => new(key);

    protected override Equipment Map(EquipmentRaw storageValue) => Context.Mapper.Map(storageValue);
}