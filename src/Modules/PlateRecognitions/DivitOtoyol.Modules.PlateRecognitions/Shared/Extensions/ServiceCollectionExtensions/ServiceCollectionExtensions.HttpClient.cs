using BuildingBlocks.Resiliency.Extensions;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Camera;
using DivitOtoyol.Modules.PlateRecognitions.Shared.System.Option;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Vehicle.Color;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Vehicle.Make;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Vehicle.Model;

namespace DivitOtoyol.Modules.PlateRecognitions.Shared.Extensions.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomHttpClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<CamerasApiClientOptions>().Bind(
                configuration.GetSection(
                    $"{PlateRecognitionModuleConfiguration.ModuleName}:{nameof(CamerasApiClientOptions)}"))
            .ValidateDataAnnotations();

        services.AddOptions<MakesApiClientOptions>().Bind(
                configuration.GetSection(
                    $"{PlateRecognitionModuleConfiguration.ModuleName}:{nameof(MakesApiClientOptions)}"))
            .ValidateDataAnnotations();

        services.AddOptions<ModelsApiClientOptions>().Bind(
                configuration.GetSection(
                    $"{PlateRecognitionModuleConfiguration.ModuleName}:{nameof(ModelsApiClientOptions)}"))
            .ValidateDataAnnotations();

        services.AddOptions<ColorsApiClientOptions>().Bind(
                configuration.GetSection(
                    $"{PlateRecognitionModuleConfiguration.ModuleName}:{nameof(ColorsApiClientOptions)}"))
            .ValidateDataAnnotations();

        services.AddOptions<OptionsApiClientOptions>().Bind(
                configuration.GetSection(
                    $"{PlateRecognitionModuleConfiguration.ModuleName}:{nameof(OptionsApiClientOptions)}"))
            .ValidateDataAnnotations(); 

        services.AddHttpApiClient<ICameraApiClient, CameraApiClient>();
        services.AddHttpApiClient<IMakeApiClient, MakeApiClient>();
        services.AddHttpApiClient<IModelApiClient, ModelApiClient>();
        services.AddHttpApiClient<IColorApiClient, ColorApiClient>();
        services.AddHttpApiClient<IOptionApiClient, OptionApiClient>();

        return services;
    }
}
