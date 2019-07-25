using System.Linq;
using Newtonsoft.Json.Linq;
using Tenu.Core.Interfaces;
using Tenu.Core.Models;

namespace Tenu.Core.Services
{
    public class ContentTypeRepository : IContentTypeRepository
    {
        private readonly ITenuConfigProvider _configProvider;

        public ContentTypeRepository(ITenuConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public ContentType[] GetAll()
        {
            var types = _configProvider.ListChildren("ContentTypes");
            return types.Select(GetByAlias).ToArray();
        }

        public ContentType GetByAlias(string contentTypeAlias)
        {
            contentTypeAlias = RemoveDotJson(contentTypeAlias);

            var json = _configProvider.ReadConfig($"ContentTypes/{contentTypeAlias}.json");
            if (string.IsNullOrWhiteSpace(json)) return null;

            return new ContentType(contentTypeAlias, JObject.Parse(json));
        }

        private static string RemoveDotJson(string value)
        {
            return value.EndsWith(".json") 
                ? value.Remove(value.Length - ".json".Length) 
                : value;
        }
    }
}
