using Microsoft.Extensions.Logging;
using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Repositories;

internal class EquipmentRepository : SingleFileRepositoryBase<string, EquipmentProfile, EquipmentProfileRaw>
{
    protected override string DataFilePath => "/data/equipments.json";

    public EquipmentRepository(BuilderContext context, HttpClient httpClient, ILogger<EquipmentRepository> logger) :
        base(context, httpClient, logger)
    { }

    protected override string GetKey(EquipmentProfile entity) => entity.Name;
    protected override EquipmentProfile CreateEmpty(string key) => new(key);

    protected override EquipmentProfile Map(EquipmentProfileRaw raw) => 
        Context.Mapper.EquipmentProfileMapper.Map(raw);
}