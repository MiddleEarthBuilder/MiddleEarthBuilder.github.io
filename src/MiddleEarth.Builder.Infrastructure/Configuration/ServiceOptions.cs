namespace MiddleEarth.Builder.Infrastructure.Configuration;
public class ServiceOptions
{
    public const string SectionKey = "Service";

    public long MaxFileUploadSize { get; set; } = 1024 * 1024;
}
