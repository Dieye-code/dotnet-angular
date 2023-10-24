namespace API.Application.middlewares;

public static class Startup
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionMiddleware>();
}
