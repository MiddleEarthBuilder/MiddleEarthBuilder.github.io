using Microsoft.Extensions.Logging;
using MiddleEarth.Builder.Infrastructure.Entities;
using MiddleEarth.Models;

namespace MiddleEarth.Builder.Infrastructure.Repositories;

internal class SpecialRulesRepository : SingleFileRepositoryBase<string, SpecialRule, SpecialRuleRaw>
{
    protected override string DataFilePath => "/data/special-rules.json";

    public SpecialRulesRepository(BuilderContext context, HttpClient httpClient, ILogger<SpecialRulesRepository> logger) :
        base(context, httpClient, logger)
    { }

    protected override string GetKey(SpecialRule entity) => entity.Name;
    protected override SpecialRule CreateEmpty(string key) => new(key);

    protected override SpecialRule Map(SpecialRuleRaw raw) =>
        Context.Mapper.SpecialRuleMapper.Map(raw);
}