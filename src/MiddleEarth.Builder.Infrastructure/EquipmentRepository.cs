using Microsoft.Extensions.Logging;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure;

internal class EquipmentRepository : SingleFileRepositoryBase<string, EquipmentDto>
{
    protected override string DataFilePath => "/data/equipments.json";
    protected override string GetKey(EquipmentDto entity) => entity.Name;

    public EquipmentRepository(HttpClient httpClient, ILogger<SingleFileRepositoryBase<string, EquipmentDto>> logger) :
        base(httpClient, logger)
    { }
}