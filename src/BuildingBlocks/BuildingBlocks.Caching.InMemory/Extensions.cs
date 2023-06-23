using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.Caching;
using BuildingBlocks.Core.Caching;
using BuildingBlocks.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace BuildingBlocks.Caching.InMemory;

// IServiceCollection'a InMemoryCacheProvider'ı kaydeder
public static class Extensions
{
    public static IServiceCollection AddCustomInMemoryCache(
        this IServiceCollection services,
        IConfiguration config,
        Action<InMemoryCacheOptions>? configureOptions = null)
    {
        Guard.Against.Null(services, nameof(services));

        var options = config.GetOptions<InMemoryCacheOptions>(nameof(InMemoryCacheOptions));

        if (configureOptions is { })
        {
            services.Configure(configureOptions);
        }
        else
        {
            services.AddOptions<InMemoryCacheOptions>().Bind(config.GetSection(nameof(InMemoryCacheOptions)))
                .ValidateDataAnnotations();
        }

        // Bellek önbelleği servisini ekler
        services.AddMemoryCache();

        // Cache manager ve cache provider'ı kaydeder
        services.AddTransient<ICacheManager, CacheManager>();
        services.AddSingleton<ICacheProvider, InMemoryCacheProvider>();

        return services;
    }
}
