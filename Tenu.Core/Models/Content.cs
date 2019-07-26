using System;
using System.Collections.Generic;

namespace Tenu.Core.Models
{
    public class Content
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? ParentId { get; set; }
        public string ContentTypeAlias { get; set; }
        public string Name { get; set; }
        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
        public IDictionary<string, ContentProperty> Properties { get; set; } = new Dictionary<string,ContentProperty>();
        public IList<string> Urls { get; set; } = new List<string>();

        public T GetMetadata<T>(string key)
        {
            if (!Metadata.ContainsKey(key)) return default;

            var value = Metadata[key];
            if (value is T t) return t;

            return (T) Convert.ChangeType(value, typeof(T));
        }

        public void SetMetadata<T>(string key, T value)
        {
            if (Metadata.ContainsKey(key))
                Metadata[key] = value;
            else
                Metadata.Add(key, value);
        }


        public class ContentProperty
        {
            public string PropertyTypeAlias { get; set; }
            public string RawValue { get; set; }
        }
    }
}
