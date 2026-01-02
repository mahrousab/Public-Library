namespace MyBook.Exceptions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        var errorResponse = new MyBook.ViewModels.ErrorVM
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error.",
                            Path = context.Request.Path
                        };
                        await context.Response.WriteAsync(errorResponse.ToString());
                    }
                });
            });
        }
    }
}
