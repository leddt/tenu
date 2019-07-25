using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Tenu.Core.Extensions;

namespace Tenu.Core.Models
{
    public class ContentType
    {
        public ContentType(string alias, JObject jObject)
        {
            Alias = alias;
            
            Name = jObject.GetRequiredString("name");
            Properties = jObject
                .GetRequiredObject("properties")
                .Properties()
                .Select(prop => new PropertyDefinition(prop))
                .ToList()
                .AsReadOnly();
        }

        public string Alias { get; }
        public string Name { get; }
        public IReadOnlyCollection<PropertyDefinition> Properties { get; }

        public class PropertyDefinition
        {
            public PropertyDefinition(JProperty jProperty)
            {
                Alias = jProperty.Name;

                var jObject = jProperty.GetObjectValue();

                PropertyTypeAlias = jObject.GetRequiredString("type");
                Name = jObject.GetOptionalString("name") ?? Alias;
                Hint = jObject.GetOptionalString("hint");
                Required = jObject.GetOptionalBoolean("required");
            }

            public string Alias { get; }
            public string PropertyTypeAlias { get; }
            public string Name { get; }
            public string Hint { get; }
            public bool Required { get; }
        }
    }
}