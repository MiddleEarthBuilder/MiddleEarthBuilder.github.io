using System.Text.Json;
using System.Text.Json.Serialization;

namespace MiddleEarth.Builder.Infrastructure.Files;

public class ContextExporter
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

    private readonly Context _context;

    public ContextExporter(Context context)
    {
        _context = context;
    }

    public Task<Stream> GetStream()
    {
        var raw = _context.GetRaw();
        var binaryData = JsonSerializer.SerializeToUtf8Bytes(raw, JsonSerializerOptions);
        return Task.FromResult((Stream)new MemoryStream(binaryData));
    }
}