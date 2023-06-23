using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Persistence.EfCore.Postgres;
using DivitOtoyol.Modules.Systems.Shared.Contracts;
using DivitOtoyol.Modules.Systems.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Systems.Shared.Extensions.ServiceCollectionExtensions;

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
                $"{SystemModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}:UseInMemory"))
        {
            services.AddDbContext<SystemDbContext>(options =>
                options.UseInMemoryDatabase("DivitOtoyol.Modules.Systems"));

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<SystemDbContext>()!);
        }
        else
        {
            services.AddPostgresDbContext<SystemDbContext>(
                configuration,
                $"{SystemModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}");
        }

        services.AddScoped<ISystemDbContext>(provider => provider.GetRequiredService<SystemDbContext>());
    }
}
