using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Persistence.EfCore.Postgres;
using DivitOtoyol.Modules.Locations.Shared.Contracts;
using DivitOtoyol.Modules.Locations.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Locations.Shared.Extensions.ServiceCollectionExtensions;

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
                $"{LocationModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}:UseInMemory"))
        {
            services.AddDbContext<LocationDbContext>(options =>
                options.UseInMemoryDatabase("DivitOtoyol.Modules.Locations"));

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<LocationDbContext>()!);
        }
        else
        {
            services.AddPostgresDbContext<LocationDbContext>(
                configuration,
                $"{LocationModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}");
        }

        services.AddScoped<ILocationDbContext>(provider => provider.GetRequiredService<LocationDbContext>());
    }
}
