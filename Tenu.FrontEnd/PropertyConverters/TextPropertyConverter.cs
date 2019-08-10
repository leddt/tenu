using Microsoft.Extensions.DependencyInjection;

namespace Tenu.FrontEnd.PropertyConverters
{
    public class TextPropertyConverter : IPropertyConverter<string>, IDefaultPropertyConverter
    {
        public string PropertyTypeAlias => "text";

        public string Convert(string rawValue) => rawValue;
        public object ConvertDefault(string rawValue) => rawValue;

        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<IPropertyConverter<string>, TextPropertyConverter>();
            services.AddSingleton<IDefaultPropertyConverter, TextPropertyConverter>();
        }
    }
}