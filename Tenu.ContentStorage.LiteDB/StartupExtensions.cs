using System;
using Microsoft.Extensions.DependencyInjection;
using Tenu.Core.Interfaces;

namespace Tenu.ContentStorage.LiteDB
{
    public static class StartupExtensions
    {
        public static ITenuCoreBuilder WithLiteDbContentStorage(this ITenuCoreBuilder builder, Action<LiteDbContentOptions> configure = null)
        {
            if (configure != null) builder.Services.Configure(configure);

            builder.Services.AddSingleton<IContentRepository, LiteDbContentRepository>();

            return builder;
        }
    }
}