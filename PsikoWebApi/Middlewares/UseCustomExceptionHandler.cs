using Core.DTOs;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Service.Exceptions;
using System.Text.Json;

namespace PsikoWebApi.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config => 
            {
                config.Run(async contex =>
                {
                    contex.Response.ContentType = "application/json";

                    var exceptionFeature = contex.Features.Get<IExceptionHandlerFeature>();

                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };

                    contex.Response.StatusCode = statusCode;

                    var response = CustomResponseDTO<NoContent>.Fail(statusCode,exceptionFeature.Error.Message);
                    await contex.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
