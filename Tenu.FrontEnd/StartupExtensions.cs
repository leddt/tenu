﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;

namespace Tenu.FrontEnd
{
    public static class StartupExtensions
    {
        public static void AddTenuFrontEnd(this IServiceCollection services)
        {
            services.AddMvcCore()
                .AddRazorViewEngine()
                .AddCookieTempDataProvider();

            services.AddScoped<TenuRouter>();
            services.AddScoped<TenuRenderer>();
        }

        public static void UseTenuFrontEnd(this IApplicationBuilder app)
        {
            // Enforce URLs ending with /
            app.UseRewriter(new RewriteOptions()
                .AddRedirect("(.*[^/])$", "$1/", 301));

            app.UseMiddleware<TenuFrontEndMiddleware>();
            app.Use((ctx, next) => ctx.Response.WriteAsync("Page not found"));
        }
    }
}
