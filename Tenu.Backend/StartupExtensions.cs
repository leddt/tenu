using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tenu.Backend.AspNetServices;

namespace Tenu.Backend
{
    public class TenuBackendOptions
    {
        // No setter for now as app bundle assumes /admin
        public string BackendPath { get; } = "/admin";
    }

    public static class StartupExtensions
    {
        public static void AddTenuBackend(this IServiceCollection services)
        {
            services.AddMvcCore()
                .AddJsonFormatters();

            services.AddSingleton<ISpaStaticFileProvider, BackendSpaStaticFileProvider>();
        }

        public static IApplicationBuilder UseTenuBackend(this IApplicationBuilder app, Action<TenuBackendOptions> configure = null)
        {
            var options = app.ApplicationServices.GetService<IOptions<TenuBackendOptions>>().Value;
            configure?.Invoke(options);

            return app.Map(options.BackendPath, x =>
            {
                x.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new BackendFileProvider()
                });
                x.UseSpaStaticFiles();

                x.UseMvc();
                x.UseSpa(spa => { spa.Options.DefaultPage = "/index.html"; });
            });
        }
    }
}
