using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tenu.BackOffice.AspNetServices;

namespace Tenu.BackOffice
{
    public class TenuBackOfficeOptions
    {
        // No setter for now as app bundle assumes /admin
        public string BackOfficePath { get; } = "/admin";
    }

    public static class StartupExtensions
    {
        public static void AddTenuBackOffice(this IServiceCollection services)
        {
            services.AddMvcCore()
                .AddJsonFormatters();

            services.AddSingleton<ISpaStaticFileProvider, BackOfficeSpaStaticFileProvider>();
        }

        public static IApplicationBuilder UseTenuBackOffice(this IApplicationBuilder app, Action<TenuBackOfficeOptions> configure = null)
        {
            var options = app.ApplicationServices.GetService<IOptions<TenuBackOfficeOptions>>().Value;
            configure?.Invoke(options);

            return app.Map(options.BackOfficePath, x =>
            {
                x.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new BackOfficeFileProvider()
                });
                x.UseSpaStaticFiles();

                x.UseMvc();
                x.UseSpa(spa => { spa.Options.DefaultPage = "/index.html"; });
            });
        }
    }
}
