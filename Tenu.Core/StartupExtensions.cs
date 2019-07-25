using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Tenu.Core.Interfaces;
using Tenu.Core.Services;

namespace Tenu.Core
{
    public class TenuOptions
    {
        public string ConfigRoot { get; set; } = "TenuConfig";
    }

    internal class TenuCoreBuilder : ITenuCoreBuilder
    {
        public TenuCoreBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }

    public static class StartupExtensions
    {
        public static ITenuCoreBuilder AddTenu(this IServiceCollection services, Action<TenuOptions> configure = null)
        {
            if (configure != null)
            {
                services.Configure(configure);
            }

            services.AddSingleton<ITenuConfigProvider>(x =>
            {
                var options = x.GetService<IOptions<TenuOptions>>().Value;
                var configRootPath = $"{x.GetRequiredService<IHostingEnvironment>().ContentRootPath}/{options.ConfigRoot}";
                return new TenuConfigProvider(new PhysicalFileProvider(configRootPath));
            });

            services.AddSingleton<IContentTypeRepository, ContentTypeRepository>();

            return new TenuCoreBuilder(services);
        }
    }
}
