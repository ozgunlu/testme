using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Persistence.EfCore.Postgres;
using BuildingBlocks.Persistence.Mongo;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Contracts;
using DivitOtoyol.Modules.PlateRecognitions.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.PlateRecognitions.Shared.Extensions.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddStorage(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        AddStorage(builder.Services, configuration);

        return builder;
    }

    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        AddPostgresWriteStorage(services, configuration);
        AddMongoReadStorage(services, configuration);

        return services;
    }

    private static void AddPostgresWriteStorage(IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>(
                $"{PlateRecognitionModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}:UseInMemory"))
        {
            services.AddDbContext<PlateRecognitionDbContext>(options =>
                options.UseInMemoryDatabase("DivitOtoyol.Modules.PlateRecognitions"));

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<PlateRecognitionDbContext>()!);
        }
        else
        {
            services.AddPostgresDbContext<PlateRecognitionDbContext>(
                configuration,
                $"{PlateRecognitionModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}");
        }

        services.AddScoped<IPlateRecognitionDbContext>(provider => provider.GetRequiredService<PlateRecognitionDbContext>());
    }

    private static void AddMongoReadStorage(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbContext<PlateRecognitionReadDbContext>(
            configuration,
            $"{PlateRecognitionModuleConfiguration.ModuleName}:{nameof(MongoOptions)}");
    }
}
