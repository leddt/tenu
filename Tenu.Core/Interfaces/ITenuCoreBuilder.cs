using Microsoft.Extensions.DependencyInjection;

namespace Tenu.Core.Interfaces
{
    public interface ITenuCoreBuilder
    {
        IServiceCollection Services { get; }
    }
}