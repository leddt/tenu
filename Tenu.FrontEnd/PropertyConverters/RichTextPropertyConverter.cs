using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.DependencyInjection;

namespace Tenu.FrontEnd.PropertyConverters
{
    public class RichTextPropertyConverter : 
        IPropertyConverter<string>, 
        IPropertyConverter<HtmlString>,
        IDefaultPropertyConverter
    {
        public string PropertyTypeAlias => "richtext";

        string IPropertyConverter<string>.Convert(string rawValue) => rawValue;
        public HtmlString Convert(string rawValue) => new HtmlString(rawValue);
        public object ConvertDefault(string rawValue) => Convert(rawValue);

        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<IPropertyConverter<string>, RichTextPropertyConverter>();
            services.AddSingleton<IPropertyConverter<HtmlString>, RichTextPropertyConverter>();
            services.AddSingleton<IDefaultPropertyConverter, RichTextPropertyConverter>();
        }
    }
}