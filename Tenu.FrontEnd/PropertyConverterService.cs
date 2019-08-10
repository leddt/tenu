using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Tenu.Core.Models;
using Tenu.FrontEnd.PropertyConverters;

namespace Tenu.FrontEnd
{
    public class PropertyConverterService
    {
        private readonly IServiceProvider _services;

        public PropertyConverterService(IServiceProvider services)
        {
            _services = services;
        }

        public T Convert<T>(Content.ContentProperty property)
        {
            var converters = _services.GetServices<IPropertyConverter<T>>();
            var converter = converters.FirstOrDefault(x => x.PropertyTypeAlias == property.PropertyTypeAlias);

            if (converter == null)
                throw new MissingConverterException(property.PropertyTypeAlias, typeof(T));

            return converter.Convert(property.RawValue);
        }

        public object ConvertDefault(Content.ContentProperty property)
        {
            var converters = _services.GetServices<IDefaultPropertyConverter>();
            var converter = converters.FirstOrDefault(x => x.PropertyTypeAlias == property.PropertyTypeAlias);

            if (converter == null)
                throw new MissingDefaultConverterException(property.PropertyTypeAlias);

            return converter.ConvertDefault(property.RawValue);
        }
    }

    public class MissingConverterException : Exception
    {
        public MissingConverterException(string propertyTypeAlias, Type wantedType) : base($"No converter from {propertyTypeAlias} to {wantedType}")
        {
        }
    }

    public class MissingDefaultConverterException : Exception
    {
        public MissingDefaultConverterException(string propertyTypeAlias) : base($"No default converter from {propertyTypeAlias}")
        {
        }
    }
}