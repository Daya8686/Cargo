
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

namespace CargoManagerSystem
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project

    //This CORS Middle ware is used for the arrangin the angular with web Api

    public class CorsMiddleware 
        {
            private readonly RequestDelegate _next;

            public CorsMiddleware(RequestDelegate next) 
            {
                _next = next;
            }

            public Task Invoke(HttpContext httpContext)
            {
                httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*"); //any origin
                httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                httpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept");
                httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");// all this methods are allowed from Web Api
                return _next(httpContext);
            }
        }
                //without this we can not access angular

        // Extension method used to add the middleware to the HTTP request pipeline.
        public static class CORSMiddlewareExtensions
        {
            public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<CorsMiddleware>();
            }
        }
    }

