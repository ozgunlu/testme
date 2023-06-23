namespace DivitOtoyol.Api.Extensions.ApplicationBuilderExtensions;

public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    ///     Register CORS.
    /// </summary>
    public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
    {
        app.UseCors(p =>
        {
            p.AllowAnyOrigin();
            p.WithMethods("GET");
            p.AllowAnyHeader();
        });

        return app;
    }
}
