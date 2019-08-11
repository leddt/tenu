using System;
using Tenu.Core.Models;

namespace Tenu.FrontEnd
{
    public class ModelFactory
    {
        private readonly PropertyConverterService _converterService;

        public ModelFactory(PropertyConverterService converterService)
        {
            _converterService = converterService;
        }

        public object CreateContentModel(Content content, Type modelType)
        {
            if (modelType == typeof(Content))
                return content;
            if (modelType == typeof(object))
                return new DynamicContentModel(content, _converterService);

            return null;
        }
    }
}