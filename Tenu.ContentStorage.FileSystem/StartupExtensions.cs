using System;
using Microsoft.Extensions.DependencyInjection;
using Tenu.Core.Interfaces;

namespace Tenu.ContentStorage.FileSystem
{
    public static class StartupExtensions
    {
        public static ITenuCoreBuilder WithFileSystemContentStorage(this ITenuCoreBuilder builder, Action<FileSystemContentOptions> configure = null)
        {
            if (configure != null) builder.Services.Configure(configure);

            builder.Services.AddScoped<IContentRepository, FileSystemContentRepository>();

            return builder;
        }
    }
}