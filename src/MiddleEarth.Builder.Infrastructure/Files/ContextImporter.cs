using Microsoft.Extensions.Logging;

namespace MiddleEarth.Builder.Infrastructure.Files;

public class ContextImporter
{
    private readonly BuilderContext _context;
    private readonly ILogger<ContextImporter> _logger;
    public ContextImporter(BuilderContext context, ILogger<ContextImporter> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Stream> GetDataStream()
    {
        throw new NotImplementedException();
    }
}