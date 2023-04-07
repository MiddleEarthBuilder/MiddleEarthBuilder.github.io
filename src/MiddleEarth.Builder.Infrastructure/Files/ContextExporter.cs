using Microsoft.Extensions.Logging;
using MiddleEarth.Builder.Application;
using MiddleEarth.Models;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MiddleEarth.Builder.Infrastructure.Files;

public class ContextExporter
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

    private readonly BuilderContext _context;
    private readonly ILogger<ContextExporter> _logger;
    public ContextExporter(BuilderContext context, ILogger<ContextExporter> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Stream> GetDataStream(CancellationToken cancellationToken)
    {
        var armyListFiles = await GetFiles(_context.ArmyLists, cancellationToken);
        var equipmentsFile = await GetFile(_context.Equipments, cancellationToken);
        var specialRulesFile = await GetFile(_context.SpecialRules, cancellationToken);
        armyListFiles[equipmentsFile.Key] = equipmentsFile.Value;
        armyListFiles[specialRulesFile.Key] = specialRulesFile.Value;
        var binaryData = GetZipArchive(armyListFiles);
        var fileStream = new MemoryStream(binaryData);

        return fileStream;
    }

    private async Task<Dictionary<string, byte[]>> GetFiles(IRepository<string, ArmyList> armyListsRepository, CancellationToken cancellationToken)
    {
        var armyListDtos = await armyListsRepository.GetAllAsync(cancellationToken);

        var dictionary = armyListDtos
            .Select(_context.Mapper.ArmyListMapper.Map)
            .ToDictionary(
                armyList => $"army-lists/{armyList.Name}.json",
                armyList => JsonSerializer.SerializeToUtf8Bytes(armyList, JsonSerializerOptions));

        var index = armyListDtos.Select(list => list.Name);
        dictionary["army-lists/_index.json"] = JsonSerializer.SerializeToUtf8Bytes(index, JsonSerializerOptions);

        return dictionary;
    }

    private async Task<KeyValuePair<string, byte[]>> GetFile(IRepository<string, EquipmentProfile> equipmentsRepository, CancellationToken cancellationToken)
    {
        var equipmentDtos = await equipmentsRepository.GetAllAsync(cancellationToken);
        var equipments = equipmentDtos
            .Select(_context.Mapper.EquipmentProfileMapper.Map)
            .OrderBy(equipment => equipment.Name);
        var bytes = JsonSerializer.SerializeToUtf8Bytes(equipments, JsonSerializerOptions);
        return new KeyValuePair<string, byte[]>("equipments.json", bytes);
    }

    private async Task<KeyValuePair<string, byte[]>> GetFile(IRepository<string, SpecialRule> specialRulesRepository, CancellationToken cancellationToken)
    {
        var specialRuleDtos = await specialRulesRepository.GetAllAsync(cancellationToken);
        var specialRules = specialRuleDtos
            .Select(_context.Mapper.SpecialRuleMapper.Map)
            .OrderBy(rule => rule.Name);
        var bytes = JsonSerializer.SerializeToUtf8Bytes(specialRules, JsonSerializerOptions);
        return new KeyValuePair<string, byte[]>("special-rules.json", bytes);
    }

    private static byte[] GetZipArchive(IEnumerable<KeyValuePair<string, byte[]>> files)
    {
        using var archiveStream = new MemoryStream();
        using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
        {
            foreach (var file in files)
            {
                var zipArchiveEntry = archive.CreateEntry(file.Key, CompressionLevel.Fastest);
                using var zipStream = zipArchiveEntry.Open();
                zipStream.Write(file.Value, 0, file.Value.Length);
            }
        }

        return archiveStream.ToArray();
    }
}