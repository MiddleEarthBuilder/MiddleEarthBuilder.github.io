using Microsoft.Extensions.Logging;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Repositories;

internal class SpecialRulesRepository : SingleFileRepositoryBase<string, SpecialRuleDto, SpecialRule>
{
    protected override string DataFilePath => "/data/special-rules.json";

    public SpecialRulesRepository(BuilderContext context, HttpClient httpClient, ILogger<SpecialRulesRepository> logger) :
        base(context, httpClient, logger)
    { }

    protected override string GetKey(SpecialRuleDto entity) => entity.Name;
    protected override SpecialRuleDto CreateEmpty(string key) => new(key);

    protected override SpecialRuleDto Map(SpecialRule storageValue) => Context.Mapper.Map(storageValue);
}