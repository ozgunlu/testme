using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Persistence.EfCore.Postgres;
using DivitOtoyol.Modules.Servers.Shared.Contracts;
using DivitOtoyol.Modules.Servers.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Servers.Shared.Extensions.ServiceCollectionExtensions;

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
                $"{ServerModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}:UseInMemory"))
        {
            services.AddDbContext<ServerDbContext>(options =>
                options.UseInMemoryDatabase("DivitOtoyol.Modules.Servers"));

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<ServerDbContext>()!);
        }
        else
        {
            services.AddPostgresDbContext<ServerDbContext>(
                configuration,
                $"{ServerModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}");
        }

        services.AddScoped<IServerDbContext>(provider => provider.GetRequiredService<ServerDbContext>());
    }
}
