using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace GEC.Presentation.Api.Middlewares
{
    public class GlobalExceptionHandlingMiddleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = ex is InvalidOperationException || ex is ArgumentException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                ProblemDetails problem = new()
                {
                    Status = context.Response.StatusCode,
                    Type = "Server Error",
                    Title = "Server Error",
                };

                var json = JsonSerializer.Serialize(problem);
                await context.Response.WriteAsync(json);
                context.Response.ContentType = "application/json";
            }
        }
    }
}