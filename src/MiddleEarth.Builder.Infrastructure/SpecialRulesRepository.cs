using Microsoft.Extensions.Logging;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure;

internal class SpecialRulesRepository : SingleFileRepositoryBase<string, SpecialRule>
{
    protected override string DataFilePath => "/data/special-rules.json";
    protected override string GetKey(SpecialRule entity) => entity.Name;

    public SpecialRulesRepository(HttpClient httpClient, ILogger<SingleFileRepositoryBase<string, SpecialRule>> logger) :
        base(httpClient, logger)
    { }
}