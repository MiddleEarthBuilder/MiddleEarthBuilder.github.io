using Microsoft.Extensions.Logging;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Repositories;

internal class EquipmentRepository : SingleFileRepositoryBase<string, EquipmentDto, Equipment>
{
    protected override string DataFilePath => "/data/equipments.json";

    public EquipmentRepository(BuilderContext context, HttpClient httpClient, ILogger<EquipmentRepository> logger) :
        base(context, httpClient, logger)
    { }

    protected override string GetKey(EquipmentDto entity) => entity.Name;
    protected override EquipmentDto CreateEmpty(string key) => new(key);

    protected override EquipmentDto Map(Equipment storageValue) => Context.Mapper.Map(storageValue);
}