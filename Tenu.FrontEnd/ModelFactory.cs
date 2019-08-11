using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Tenu.Core.Models;

namespace Tenu.FrontEnd
{
    public class ModelFactory
    {
        private static readonly object ServiceNotFound = new object();

        private readonly PropertyConverterService _converterService;
        private readonly IServiceProvider _services;

        public ModelFactory(PropertyConverterService converterService, IServiceProvider services)
        {
            _converterService = converterService;
            _services = services;
        }

        public object CreateContentModel(Content content, Type modelType)
        {
            if (modelType == typeof(Content))
                return content;
            if (modelType == typeof(object))
                return new DynamicContentModel(content, _converterService);
            if (TryBuildModel(content, modelType, out var model))
                return model;
                
            return null;
        }

        private bool TryBuildModel(Content content, Type modelType, out object model)
        {
            var ctors = modelType.GetConstructors().OrderByDescending(ctor => ctor.GetParameters().Length);
            foreach (var ctor in ctors)
            {
                var args = ctor.GetParameters().Select(param => GetConstructorArgument(param, content)).ToArray();
                if (args.Any(x => x == ServiceNotFound)) continue;

                try
                {
                    model = Activator.CreateInstance(modelType, args);
                    return true;
                }
                catch { }
            }

            model = null;
            return false;
        }

        private object GetConstructorArgument(ParameterInfo parameterInfo, Content content)
        {
            if (parameterInfo.ParameterType == typeof(Content)) return content;

            var service = _services.GetService(parameterInfo.ParameterType);
            if (service != null)
                return service;

            if (parameterInfo.HasDefaultValue)
                return parameterInfo.DefaultValue;

            return ServiceNotFound;
        }
    }
}