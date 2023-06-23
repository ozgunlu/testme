using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Persistence.EfCore.Postgres;
using DivitOtoyol.Modules.Vehicles.Shared.Contracts;
using DivitOtoyol.Modules.Vehicles.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Vehicles.Shared.Extensions.ServiceCollectionExtensions;

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

        return services;
    }

    private static void AddPostgresWriteStorage(IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>(
                $"{VehicleModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}:UseInMemory"))
        {
            services.AddDbContext<VehicleDbContext>(options =>
                options.UseInMemoryDatabase("DivitOtoyol.Modules.Vehicles"));

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<VehicleDbContext>()!);
        }
        else
        {
            services.AddPostgresDbContext<VehicleDbContext>(
                configuration,
                $"{VehicleModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}");
        }

        services.AddScoped<IVehicleDbContext>(provider => provider.GetRequiredService<VehicleDbContext>());
    }
}
