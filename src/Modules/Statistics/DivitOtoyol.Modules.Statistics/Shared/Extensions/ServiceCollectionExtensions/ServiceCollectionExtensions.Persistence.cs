using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Persistence.EfCore.Postgres;
using DivitOtoyol.Modules.Statistics.Shared.Contracts;
using DivitOtoyol.Modules.Statistics.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Statistics.Shared.Extensions.ServiceCollectionExtensions;

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
                $"{StatisticModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}:UseInMemory"))
        {
            services.AddDbContext<StatisticDbContext>(options =>
                options.UseInMemoryDatabase("DivitOtoyol.Modules.Statistics"));

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<StatisticDbContext>()!);
        }
        else
        {
            services.AddPostgresDbContext<StatisticDbContext>(
                configuration,
                $"{StatisticModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}");
        }

        services.AddScoped<IStatisticDbContext>(provider => provider.GetRequiredService<StatisticDbContext>());
    }
}
