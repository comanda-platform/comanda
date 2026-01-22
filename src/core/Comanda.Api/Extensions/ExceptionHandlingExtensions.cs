using Microsoft.AspNetCore.Diagnostics;
using Comanda.Domain;

namespace Comanda.Api.Extensions;

public static class ExceptionHandlingExtensions
{
    extension(IApplicationBuilder app)
    {
        public IApplicationBuilder UseGlobalExceptionHandling()
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exception = context.Features
                        .Get<IExceptionHandlerFeature>()?
                        .Error;

                    if (context.Response.HasStarted)
                        return;

                    context.Response.Clear();
                    context.Response.ContentType = "application/json";

                    switch (exception)
                    {
                        case NotFoundException ex:
                            context.Response.StatusCode = StatusCodes.Status404NotFound;
                            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
                            break;

                        case ConflictException ex:
                            context.Response.StatusCode = StatusCodes.Status409Conflict;
                            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
                            break;

                        case ArgumentException ex:
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
                            break;

                        case InvalidOperationException ex:
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
                            break;

                        case UnauthorizedAccessException:
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            break;

                        default:
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            await context.Response.WriteAsJsonAsync(new
                            {
                                error = "An unexpected error occurred"
                            });
                            break;
                    }
                });
            });

            return app;
        }
    }
}







