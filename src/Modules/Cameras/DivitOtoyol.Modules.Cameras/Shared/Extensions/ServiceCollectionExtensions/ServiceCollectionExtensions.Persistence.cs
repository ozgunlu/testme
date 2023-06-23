using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Persistence.EfCore.Postgres;
using BuildingBlocks.Persistence.Mongo;
using DivitOtoyol.Modules.Cameras.Shared.Contracts;
using DivitOtoyol.Modules.Cameras.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Cameras.Shared.Extensions.ServiceCollectionExtensions;

// Bu sınıf, uygulamanın postgreSQL ve MongoDB veritabanlarına erişimini sağlar.
public static partial class ServiceCollectionExtensions
{
    // Bu metot, uygulamanın WebApplicationBuilder nesnesi üzerinde kullanılabilir.
    // Bu metod, IServiceCollection öğelerine postgreSQL ve MongoDB veritabanı servislerini ekler.
    public static WebApplicationBuilder AddStorage(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        AddStorage(builder.Services, configuration);

        return builder;
    }

    // Bu metot, IServiceCollection öğelerine postgreSQL ve MongoDB veritabanı servislerini ekler.
    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        AddPostgresWriteStorage(services, configuration);

        return services;
    }

    // PostgreSQL veritabanı hizmetlerini IServiceCollection'a ekler.
    // InMemory kullanımı etkinleştirilirse, "DivitOtoyol.Modules.Cameras" adında bir in-memory veritabanı oluşturulur.
    private static void AddPostgresWriteStorage(IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>(
                $"{CameraModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}:UseInMemory"))
        {
            services.AddDbContext<CameraDbContext>(options =>
                options.UseInMemoryDatabase("DivitOtoyol.Modules.Cameras"));

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<CameraDbContext>()!);
        }
        else
        {
            services.AddPostgresDbContext<CameraDbContext>(
                configuration,
                $"{CameraModuleConfiguration.ModuleName}:{nameof(PostgresOptions)}");
        }

        services.AddScoped<ICameraDbContext>(provider => provider.GetRequiredService<CameraDbContext>());
    }
}
