using Microsoft.AspNetCore.Builder;

namespace DivitOtoyol.Modules.Servers.Shared.Extensions.ApplicationBuilderExtensions;

public static partial class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        return app;
    }
}
