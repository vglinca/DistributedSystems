﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AviaSalesApi.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = ctx =>
                    {
                        var problemDetails = new ValidationProblemDetails(ctx.ModelState)
                        {
                            Title = "One or more validation problems has occured.",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "See the 'errors' property for details.",
                            Instance = ctx.HttpContext.Request.Path
                        };
                        problemDetails.Extensions.Add("traceId", ctx.HttpContext.TraceIdentifier);

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = {"application/problem+json"}
                        };
                    };
                });
            
            return services;
        }
    }
}