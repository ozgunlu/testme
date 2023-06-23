using Asp.Versioning.Builder;

namespace DivitOtoyol.Modules.Cameras.Cameras;

internal static class CamerasConfigs
{
    public const string Tag = "Camera";
    public const string CamerasPrefixUri =
        $"{CameraModuleConfiguration.CameraModulePrefixUri}/cameras";
    public static ApiVersionSet VersionSet { get; private set; } = default!;

    internal static IServiceCollection AddCamerasServices(this IServiceCollection services)
    {
        return services;
    }

    internal static IEndpointRouteBuilder MapCamerasEndpoints(this IEndpointRouteBuilder endpoints)
    {
        VersionSet = endpoints.NewApiVersionSet(Tag).Build();
        return endpoints;
    }
}
