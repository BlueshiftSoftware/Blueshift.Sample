namespace Blueshift.Sample.WebAPI;

public class Routes
{
    public const string Applications = "applications";
    public const string ApplicationById = $"{Applications}/{{applicationId}}";
    public const string Models = $"{ApplicationById}/models";
    public const string ModelById = $"{Models}/{{modelId}}";
}
